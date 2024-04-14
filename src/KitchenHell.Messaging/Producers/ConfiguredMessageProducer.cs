namespace KitchenHell.Messaging.Producers;

public abstract class ConfiguredMessageProducer<TMessageKey, TMessage, TMessageProducerOptions>
  : MessageProducer<TMessageKey, TMessage>
  where TMessageProducerOptions : MessageProducerOptions
  where TMessage : IMessage<TMessageKey>
{
  protected ConfiguredMessageProducer(
    IMessageProduceService<TMessageKey, TMessage> produceService
  )
    : base(produceService)
  {
  }

  public override Task ProduceAsync(TMessage message, CancellationToken ct)
  {
    if (!ProducerOptions.IsEnabled)
    {
      return Task.CompletedTask;
    }

    return base.ProduceAsync(message, ct);
  }

  protected abstract TMessageProducerOptions ProducerOptions { get; }

  protected override string GetTopicName()
  {
    return ProducerOptions.TopicName;
  }
}
