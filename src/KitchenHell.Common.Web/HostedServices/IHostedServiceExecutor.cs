namespace KitchenHell.Common.Web.HostedServices;

public interface IHostedServiceExecutor
{
  Task ExecuteAsync(CancellationToken ct);
}
