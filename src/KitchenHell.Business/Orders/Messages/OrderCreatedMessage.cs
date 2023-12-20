namespace KitchenHell.Business.Orders.Messages;

public class OrderCreatedMessage
{
    public long OrderId { get; set; }

    public long RestaurantId { get; set; }
}
