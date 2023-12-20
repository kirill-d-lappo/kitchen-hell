namespace KitchenHell.Business.Orders.Services;

public class CreateOrderParams
{
    public DateTimeOffset? CreatedAt { get; set; }

    public long RestaurantId { get; set; }
}
