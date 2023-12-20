using KitchenHell.Restaurants.Business.Restaurants;
using KitchenHell.Restaurants.Business.Restaurants.Repositories;
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

    public async Task<IEnumerable<RestaurantEntity>> GetRestaurantsAsync(CancellationToken ct)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        return dbContext.Restaurants
            .Select(MapToRestaurant)
            .ToList();
    }

    private static RestaurantEntity MapToRestaurant(RestaurantEfEntity arg)
    {
        if (arg == default)
        {
            return default;
        }

        return new RestaurantEntity
        {
            Id = arg.Id,
            Name = arg.Name,
            FullAddress = arg.FullAddress,
            Latitude = arg.Latitude,
            Longitude = arg.Longitude,
        };
    }
}
