namespace KitchenHell.Messaging.Producers;

public interface IMessageProducer<TMessageKey, in TMessage>
  where TMessage : IMessage<TMessageKey>
{
  Task ProduceAsync(TMessage message, CancellationToken ct);
}
