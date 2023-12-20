using KitchenHell.Business.Orders;

namespace KitchenHell.Business.Messages;

public class OrderStatusUpdatedMessage
{
    public long OrderId { get; set; }

    public OrderStatus NewStatus { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}
