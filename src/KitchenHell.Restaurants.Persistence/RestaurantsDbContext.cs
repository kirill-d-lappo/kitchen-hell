using KitchenHell.Restaurants.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Restaurants.Persistence;

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

    public DbSet<RestaurantEntity> Restaurants { get; set; }
}