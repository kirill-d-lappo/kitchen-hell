using KitchenHell.Orders.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Orders.Persistence;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DatabaseConfigurations.Schema);
    }

    internal DbSet<OrderEfEntity> Orders { get; set; }
}
