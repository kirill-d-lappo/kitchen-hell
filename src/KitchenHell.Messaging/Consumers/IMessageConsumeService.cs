namespace KitchenHell.Messaging.Consumers;

public interface IMessageConsumeService<TKey, TValue>
{
    Task ConsumeAsync(CancellationToken ct);
}