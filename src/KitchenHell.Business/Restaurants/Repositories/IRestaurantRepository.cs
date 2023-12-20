namespace KitchenHell.Business.Restaurants.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<RestaurantEntity>> GetRestaurantsAsync(CancellationToken ct);
}
