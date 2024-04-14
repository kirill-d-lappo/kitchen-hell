using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Persistence.Restaurants.Models;

[PrimaryKey(nameof(RestaurantId), nameof(OrderId))]
internal class RestaurantOrderEfEntity
{
  public long RestaurantId { get; set; }

  public long OrderId { get; set; }

  public RestaurantEfEntity Restaurant { get; set; }
}
