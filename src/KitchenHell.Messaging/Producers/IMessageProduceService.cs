namespace KitchenHell.Messaging.Producers;

public interface IMessageProduceService<in TKey, in TValue>
{
    Task ProduceMessageAsync(TKey key, TValue value, string topic, CancellationToken ct);
}
