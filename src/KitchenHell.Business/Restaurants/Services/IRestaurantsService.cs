namespace KitchenHell.Business.Restaurants.Services;

public interface IRestaurantsService
{
  Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync(CancellationToken ct);
}
