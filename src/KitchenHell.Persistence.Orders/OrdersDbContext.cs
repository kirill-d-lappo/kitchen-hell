using KitchenHell.Persistence.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Persistence.Orders;

public class OrdersDbContext : DbContext
{
  public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
    : base(options)
  {
  }

  internal DbSet<OrderEntity> Orders { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema(DatabaseConfigurations.Schema);
  }
}
