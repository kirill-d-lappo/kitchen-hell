namespace KitchenHell.Orders.Business.Messages;

public class OrderCreatedMessage
{
    public long OrderId { get; set; }

    public long RestaurantId { get; set; }
}