namespace KitchenHell.Business.Orders.Messages.Producers;

public interface IOrderCreatedMessageProducer
{
    Task ProduceAsync(OrderCreatedMessage message, CancellationToken ct);
}
