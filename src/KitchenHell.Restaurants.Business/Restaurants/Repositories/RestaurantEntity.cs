namespace KitchenHell.Restaurants.Business.Restaurants.Repositories;

public class RestaurantEntity
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string FullAddress { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
