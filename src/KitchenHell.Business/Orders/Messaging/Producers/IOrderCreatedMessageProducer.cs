using KitchenHell.Business.Messages;

namespace KitchenHell.Business.Orders.Messaging.Producers;

public interface IOrderCreatedMessageProducer
{
    Task ProduceAsync(OrderCreatedMessage message, CancellationToken ct);
}
