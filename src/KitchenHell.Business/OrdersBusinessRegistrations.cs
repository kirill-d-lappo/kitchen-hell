using KitchenHell.Business.Common;
using KitchenHell.Business.Orders.Services;
using KitchenHell.Business.Restaurants.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Business;

public static class OrdersBusinessRegistrations
{
    public static IServiceCollection AddKitchenHellBusiness(
        this IServiceCollection services
    )
    {
        services.AddOrdersBusiness();
        services.AddRestaurantsBusiness();

        return services;
    }

    private static IServiceCollection AddOrdersBusiness(
        this IServiceCollection services
    )
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IOrderService, OrderService>();

        return services;
    }

    private static IServiceCollection AddRestaurantsBusiness(
        this IServiceCollection services
    )
    {
        services.AddScoped<IRestaurantsService, RestaurantsService>();

        return services;
    }
}
