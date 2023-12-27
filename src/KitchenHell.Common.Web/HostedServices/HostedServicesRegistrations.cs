using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Common.Web.HostedServices;

public static class HostedServicesRegistrations
{
  public static IServiceCollection AddHostedServiceExecutor<THostedServiceExecutor>(this IServiceCollection services)
    where THostedServiceExecutor : class, IHostedServiceExecutor
  {
    services.AddHostedService<EntrypointHostedService<THostedServiceExecutor>>();
    services.AddScoped<THostedServiceExecutor>();

    return services;
  }
}
