using KitchenHell.Messaging.Consumers;
using KitchenHell.Orders.Business.Orders.Repositories;
using KitchenHell.Orders.Business.Orders.Services;

namespace KitchenHell.Orders.Business.Messages.Consumers;

public class OrderStatusUpdatedMessageHandler : IMessageHandler<string, OrderStatusUpdatedMessage>
{
    private readonly IOrderRepository _orderRepository;

    public OrderStatusUpdatedMessageHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task HandleAsync(string key, OrderStatusUpdatedMessage value, CancellationToken ct)
    {
        var orderId = value.OrderId;
        var status = value.NewStatus;
        await _orderRepository.UpdateOrderStatusAsync(orderId, status, ct);
    }
}
