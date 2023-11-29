using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Orders.Persistence;

public static class PersistenceRegistrations
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddPooledDbContextFactory<OrdersDbContext>(
            options => { options.UseOrdersDatabase(); });
    }
}