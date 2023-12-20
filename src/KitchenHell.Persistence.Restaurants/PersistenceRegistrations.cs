using KitchenHell.Business.Restaurants.Repositories;
using KitchenHell.Persistence.Restaurants.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Persistence.Restaurants;

public static class PersistenceRegistrations
{
    public static void AddRestaurantsPersistence(this IServiceCollection services)
    {
        services.AddPooledDbContextFactory<RestaurantsDbContext>(
            options =>
            {
                options.UseOrdersDatabase();
            });

        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    }
}
