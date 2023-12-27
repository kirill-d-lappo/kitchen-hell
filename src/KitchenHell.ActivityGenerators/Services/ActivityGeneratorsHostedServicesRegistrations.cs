using KitchenHell.Common.Web.HostedServices;

namespace KitchenHell.ActivityGenerators.Services;

public static class ActivityGeneratorsHostedServicesRegistrations
{
  public static IServiceCollection AddActivityGeneratorsHostedServices(this IServiceCollection services)
  {
    services.AddHostedServiceExecutor<OrdersHostedServiceExecutor>();
    services.AddOptions<OrdersHostedServiceExecutorOptions>()
      .BindConfiguration("OrdersHostedServiceExecutor")
      .ValidateDataAnnotations();

    return services;
  }
}
