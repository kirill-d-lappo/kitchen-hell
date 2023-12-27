using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KitchenHell.Common.Web.HostedServices;

public class EntrypointHostedService<THostedServiceExecutor> : BackgroundService
  where THostedServiceExecutor : IHostedServiceExecutor
{
  private readonly IServiceProvider _serviceProvider;

  public EntrypointHostedService(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  protected override async Task ExecuteAsync(CancellationToken ct)
  {
    await _serviceProvider.RunScopedServiceAsync<THostedServiceExecutor>(
      async (s, innerCt) => await s.ExecuteAsync(innerCt),
      ct);
  }
}
