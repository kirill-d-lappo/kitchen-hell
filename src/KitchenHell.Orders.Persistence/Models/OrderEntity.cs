using System.ComponentModel.DataAnnotations;

namespace KitchenHell.Orders.Persistence.Models;

public class OrderEntity
{
    [Key]
    public long Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public long RestaurantId { get; set; }
}