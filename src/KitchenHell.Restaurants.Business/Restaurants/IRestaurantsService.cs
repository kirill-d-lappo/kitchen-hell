namespace KitchenHell.Restaurants.Business.Restaurants;

public interface IRestaurantsService
{
    Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync(CancellationToken ct);
}
