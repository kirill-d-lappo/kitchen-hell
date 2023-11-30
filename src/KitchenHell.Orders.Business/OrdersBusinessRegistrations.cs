using KitchenHell.Orders.Business.Common;
using KitchenHell.Orders.Business.Orders.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Orders.Business;

public static class OrdersBusinessRegistrations
{
    public static IServiceCollection AddOrdersBusiness(
        this IServiceCollection services
    )
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}