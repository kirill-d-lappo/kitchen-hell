using Confluent.Kafka;
using KitchenHell.Messaging.Producers;
using Microsoft.Extensions.Logging;

namespace KitchenHell.Messaging.Kafka.Producers;

public class KafkaMessageProduceService<TKey, TValue> : IMessageProduceService<TKey, TValue>
{
  private readonly ILogger<KafkaMessageProduceService<TKey, TValue>> _logger;
  private readonly IProducer<TKey, TValue> _producer;

  public KafkaMessageProduceService(
    ILogger<KafkaMessageProduceService<TKey, TValue>> logger,
    IProducer<TKey, TValue> producer
  )
  {
    _logger = logger;
    _producer = producer;
  }

  public async Task ProduceMessageAsync(TKey key, TValue value, string topic, CancellationToken ct)
  {
    try
    {
      var message = new Message<TKey, TValue>
      {
        Key = key,
        Value = value,
      };

      await _producer.ProduceAsync(topic, message, ct);

      _logger.LogDebug(
        "Message was produced to {Topic}: {@Message}",
        topic,
        value);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Error in producing to topic: {Topic}", topic);

      throw;
    }
  }
}
