using KitchenHell.Orders.Api.Business.Common;
using KitchenHell.Orders.Api.Business.Orders.Services;

namespace KitchenHell.Orders.Api.Business;

public static class BusinessRegistrations
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services
    )
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}