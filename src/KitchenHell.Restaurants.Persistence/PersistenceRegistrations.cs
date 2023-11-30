using KitchenHell.Restaurants.Business.Restaurants;
using KitchenHell.Restaurants.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Restaurants.Persistence;

public static class PersistenceRegistrations
{
    public static void AddRestaurantsPersistence(this IServiceCollection services)
    {
        services.AddPooledDbContextFactory<RestaurantsDbContext>(
            options => { options.UseOrdersDatabase(); });

        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    }
}