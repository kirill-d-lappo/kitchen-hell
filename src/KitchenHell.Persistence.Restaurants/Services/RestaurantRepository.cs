using KitchenHell.Business.Restaurants.Repositories;
using KitchenHell.Persistence.Restaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Persistence.Restaurants.Services;

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
      .Select(MapToDomain)
      .ToList();
  }

  public async Task CreateRestaurantOrderAsync(RestaurantOrderEntity newOrder, CancellationToken ct)
  {
    var entity = MapToEntity(newOrder);

    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    await dbContext.RestaurantOrders.AddAsync(entity, ct);
  }

  private static RestaurantEntity MapToDomain(RestaurantEfEntity arg)
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

  private static RestaurantOrderEfEntity MapToEntity(RestaurantOrderEntity arg)
  {
    if (arg == default)
    {
      return default;
    }

    return new RestaurantOrderEfEntity
    {
      OrderId = arg.OrderId,
      RestaurantId = arg.RestaurantId,
    };
  }
}
