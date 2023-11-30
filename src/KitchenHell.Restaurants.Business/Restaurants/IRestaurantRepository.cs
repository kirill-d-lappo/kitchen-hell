namespace KitchenHell.Restaurants.Business.Restaurants;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync(CancellationToken ct);
}