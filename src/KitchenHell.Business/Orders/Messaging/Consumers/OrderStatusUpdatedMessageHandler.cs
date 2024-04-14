using KitchenHell.Business.Messages;
using KitchenHell.Business.Orders.Repositories;
using KitchenHell.Messaging.Consumers;

namespace KitchenHell.Business.Orders.Messaging.Consumers;

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
