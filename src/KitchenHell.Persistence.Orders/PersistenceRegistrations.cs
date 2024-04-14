using KitchenHell.Business.Orders.Repositories;
using KitchenHell.Persistence.Orders.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Persistence.Orders;

public static class PersistenceRegistrations
{
  public static void AddOrdersPersistence(this IServiceCollection services)
  {
    services.AddPooledDbContextFactory<OrdersDbContext>(
      options =>
      {
        options.UseOrdersDatabase();
      });

    services.AddScoped<IOrderRepository, OrderRepository>();
  }
}
