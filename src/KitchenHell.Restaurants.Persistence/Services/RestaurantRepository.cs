using KitchenHell.Restaurants.Business.Restaurants;
using KitchenHell.Restaurants.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Restaurants.Persistence.Services;

internal class RestaurantRepository : IRestaurantRepository
{
    private readonly IDbContextFactory<RestaurantsDbContext> _dbContextFactory;

    public RestaurantRepository(IDbContextFactory<RestaurantsDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(CancellationToken ct)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        return dbContext.Restaurants
            .Select(MapToRestaurant)
            .ToList();
    }

    private static Restaurant MapToRestaurant(RestaurantEntity arg)
    {
        if (arg == default)
        {
            return default;
        }

        return new Restaurant
        {
            Id = arg.Id,
            Name = arg.Name,
            FullAddress = arg.FullAddress,
            Latitude = arg.Latitude,
            Longitude = arg.Longitude,
        };
    }
}