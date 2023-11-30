namespace KitchenHell.Restaurants.Business.Restaurants;

internal class RestaurantsService : IRestaurantsService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantsService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync(CancellationToken ct)
    {
        var restaurants = await _restaurantRepository.GetRestaurantsAsync(ct);

        return restaurants;
    }
}