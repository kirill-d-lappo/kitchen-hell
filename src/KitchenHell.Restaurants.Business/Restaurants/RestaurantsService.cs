using KitchenHell.Restaurants.Business.Restaurants.Repositories;

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

        return restaurants.Select(MapToDomain)
            .ToList();
    }

    private static Restaurant MapToDomain(RestaurantEntity restaurant)
    {
        if (restaurant == default)
        {
            return default;
        }

        return new Restaurant
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            FullAddress = restaurant.FullAddress,
            Latitude = restaurant.Latitude,
            Longitude = restaurant.Longitude,
        };
    }
}
