using KitchenHell.Persistence.Restaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Persistence.Restaurants;

public class RestaurantsDbContext : DbContext
{
    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DatabaseConfigurations.Schema);
    }

    internal DbSet<RestaurantEfEntity> Restaurants { get; set; }

    internal DbSet<RestaurantOrderEfEntity> RestaurantOrders { get; set; }
}
