using KitchenHell.Orders.Business.Orders;

namespace KitchenHell.Orders.Business.Messages;

public class OrderRestaurantStatusUpdatedMessage
{
    public long OrderId { get; set; }

    public OrderRestaurantStatus NewStatus { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}
