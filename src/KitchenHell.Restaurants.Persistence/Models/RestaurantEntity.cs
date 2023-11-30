namespace KitchenHell.Restaurants.Persistence.Models;

public class RestaurantEntity
{
    [Key]
    public long Id { get; set; }

    [MaxLength(64)]
    public string Name { get; set; }

    [MaxLength(64)]
    public string FullAddress { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}