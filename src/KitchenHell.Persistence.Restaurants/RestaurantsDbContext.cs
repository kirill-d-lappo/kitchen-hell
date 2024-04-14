using KitchenHell.Persistence.Restaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Persistence.Restaurants;

public class RestaurantsDbContext : DbContext
{
  public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
    : base(options)
  {
  }

  internal DbSet<RestaurantEfEntity> Restaurants { get; set; }

  internal DbSet<RestaurantOrderEfEntity> RestaurantOrders { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema(DatabaseConfigurations.Schema);
  }
}
