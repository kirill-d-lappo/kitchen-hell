using KitchenHell.Messaging.Consumers;
using KitchenHell.Orders.Business.Orders.Services;

namespace KitchenHell.Orders.Business.Messages.Consumers;

public class OrderRestaurantStatusUpdatedMessageHandler : IMessageHandler<string, OrderRestaurantStatusUpdatedMessage>
{
    private readonly IOrderRepository _orderRepository;

    public OrderRestaurantStatusUpdatedMessageHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task HandleAsync(string key, OrderRestaurantStatusUpdatedMessage value, CancellationToken ct)
    {
        var orderId = value.OrderId;
        var status = value.NewStatus;
        await _orderRepository.UpdateRestaurantOrderStatusAsync(orderId, status, ct);
    }
}