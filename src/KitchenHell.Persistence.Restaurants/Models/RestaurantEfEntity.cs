namespace KitchenHell.Persistence.Restaurants.Models;

internal class RestaurantEfEntity
{
  [Key]
  public long Id { get; set; }

  [MaxLength(64)]
  public string Name { get; set; }

  [MaxLength(64)]
  public string FullAddress { get; set; }

  public double Latitude { get; set; }

  public double Longitude { get; set; }

  public List<RestaurantOrderEfEntity> RestaurantOrders { get; set; }
}
