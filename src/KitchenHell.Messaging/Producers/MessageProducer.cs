namespace KitchenHell.Messaging.Producers;

public abstract class MessageProducer<TMessageKey, TMessage> : IMessageProducer<TMessageKey, TMessage>
  where TMessage : IMessage<TMessageKey>
{
  private readonly IMessageProduceService<TMessageKey, TMessage> _produceService;

  protected MessageProducer(IMessageProduceService<TMessageKey, TMessage> produceService)
  {
    _produceService = produceService;
  }

  public virtual async Task ProduceAsync(TMessage message, CancellationToken ct)
  {
    var topic = GetTopicName();
    var key = GetKey(message);
    await _produceService.ProduceMessageAsync(key, message, topic, ct);
  }

  protected abstract string GetTopicName();

  protected virtual TMessageKey GetKey(TMessage message)
  {
    return message.GetKey();
  }
}
