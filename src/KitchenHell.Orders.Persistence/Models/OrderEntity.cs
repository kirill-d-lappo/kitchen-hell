using KitchenHell.Orders.Business.Orders;

namespace KitchenHell.Orders.Persistence.Models;

public class OrderEntity
{
    [Key]
    public long Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public long RestaurantId { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public OrderRestaurantStatus RestaurantStatus { get; set; }

    public OrderDeliveryStatus DeliveryStatus { get; set; }
}