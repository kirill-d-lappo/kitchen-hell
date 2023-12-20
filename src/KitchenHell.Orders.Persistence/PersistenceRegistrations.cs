using KitchenHell.Orders.Business.Orders.Repositories;
using KitchenHell.Orders.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Orders.Persistence;

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
