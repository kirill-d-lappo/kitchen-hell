using KitchenHell.Orders.Business.Orders;

namespace KitchenHell.Orders.Business.Messages;

public class OrderStatusUpdatedMessage
{
    public long OrderId { get; set; }

    public OrderStatus NewStatus { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}