using Confluent.Kafka;
using KitchenHell.Messaging.Consumers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;

namespace KitchenHell.Messaging.Kafka.Consumers;

public class KafkaMessageConsumeService<TKey, TValue> : IMessageConsumeService<TKey, TValue>
{
  private readonly IConsumer<TKey, TValue> _consumer;
  private readonly IMessageHandler<TKey, TValue> _handler;
  private readonly ILogger<KafkaMessageConsumeService<TKey, TValue>> _logger;
  private readonly IOptionsMonitor<KafkaConsumerOptions<TKey, TValue>> _optionsMonitor;

  private readonly SemaphoreSlim _semaphore = new(0);

  public KafkaMessageConsumeService(
    ILogger<KafkaMessageConsumeService<TKey, TValue>> logger,
    IMessageHandler<TKey, TValue> handler,
    IConsumer<TKey, TValue> consumer,
    IOptionsMonitor<KafkaConsumerOptions<TKey, TValue>> optionsMonitor
  )
  {
    _logger = logger;
    _handler = handler;
    _consumer = consumer;
    _optionsMonitor = optionsMonitor;
  }

  protected KafkaConsumerOptions<TKey, TValue> ConsumerOptions => _optionsMonitor.CurrentValue;

  public async Task ConsumeAsync(CancellationToken ct)
  {
    using var _ = _optionsMonitor.OnChange((o, name) =>
    {
      if (name == nameof(KafkaConsumerOptions<TKey, TValue>.IsEnabled) && o.IsEnabled)
      {
        _semaphore.Release();
      }
    });

    try
    {
      if (ConsumerOptions.IsEnabled)
      {
        await Subscribe(ct);
      }

      while (!ct.IsCancellationRequested)
      {
        if (!ConsumerOptions.IsEnabled)
        {
          _consumer.Unsubscribe();
          await _semaphore.WaitAsync(ct);
          await Subscribe(ct);
        }

        await ConsumeMessages(ct);
      }
    }
    finally
    {
      _consumer?.Close();
    }
  }

  private async Task ConsumeMessages(CancellationToken ct)
  {
    try
    {
      var kafkaConsumeResult = _consumer.Consume(ct);
      var kafkaMessage = kafkaConsumeResult.Message;
      await _handler.HandleAsync(kafkaMessage.Key, kafkaMessage.Value, ct);
      _consumer.StoreOffset(kafkaConsumeResult);

      _logger.LogDebug(
        "Handled message {Topic}: {@Message}",
        ConsumerOptions.TopicName,
        kafkaMessage.Value);
    }

    // FixMe [2023-12-05 klappo] same log error for different type of exceptions, remove or left comment
    catch (KafkaException e)
    {
      _logger.LogError(e,
        "Error at consuming message from topic '{Topic}'",
        ConsumerOptions.TopicName);
    }
    catch (Exception e)
    {
      _logger.LogError(e,
        "Error at consuming message from topic '{Topic}'",
        ConsumerOptions.TopicName);
    }
  }

  private async Task Subscribe(CancellationToken ct)
  {
    var maxRetryCount = ConsumerOptions.MaxSubscribeRetryCount;
    var retryPolicy = Policy
      .Handle<Exception>()
      .WaitAndRetryAsync(
        maxRetryCount,
        PollyHelper.FibonacciDelay,
        (e, _, _) =>
        {
          _logger.LogError(e,
            "Error while Consumer<{KeyType}, {ValueType}> tried to subscribe to topic {Topic}",
            typeof(TKey),
            typeof(TValue),
            ConsumerOptions.TopicName);
        });

    await retryPolicy.ExecuteAsync(SubscribeAction, ct);
  }

  private Task SubscribeAction(CancellationToken ct)
  {
    if (ct.IsCancellationRequested)
    {
      return Task.FromCanceled(ct);
    }

    var topic = ConsumerOptions.TopicName;
    if (string.IsNullOrWhiteSpace(topic))
    {
      return Task.FromException(new InvalidOperationException("Topic name can not be empty"));
    }

    _consumer.Subscribe(topic);

    _logger.LogInformation(
      "Consumer<{KeyType}, {ValueType}> subscribed to topic {Topic}",
      typeof(TKey),
      typeof(TValue),
      topic);

    return Task.CompletedTask;
  }
}
