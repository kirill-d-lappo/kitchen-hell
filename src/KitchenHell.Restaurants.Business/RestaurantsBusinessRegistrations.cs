using KitchenHell.Restaurants.Business.Restaurants;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Restaurants.Business;

public static class RestaurantsBusinessRegistrations
{
    public static IServiceCollection AddRestaurantsBusiness(
        this IServiceCollection services
    )
    {
        services.AddScoped<IRestaurantsService, RestaurantsService>();

        return services;
    }
}