using KitchenHell.Business.Messages;
using KitchenHell.Business.Orders.Repositories;
using KitchenHell.Messaging.Consumers;

namespace KitchenHell.Business.Orders.Messaging.Consumers;

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
