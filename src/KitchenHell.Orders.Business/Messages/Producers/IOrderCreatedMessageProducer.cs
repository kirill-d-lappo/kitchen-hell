namespace KitchenHell.Orders.Business.Messages.Producers;

public interface IOrderCreatedMessageProducer
{
    Task ProduceAsync(OrderCreatedMessage message, CancellationToken ct);
}