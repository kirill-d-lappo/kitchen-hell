using KitchenHell.Common.Web.HostedServices;
using Microsoft.Extensions.Options;

namespace KitchenHell.ActivityGenerators.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public class OrdersHostedServiceExecutor : IHostedServiceExecutor
{
  private readonly IOptions<OrdersHostedServiceExecutorOptions> _optionsSource;
  private readonly ILogger<OrdersHostedServiceExecutor> _logger;

  private OrdersHostedServiceExecutorOptions Options => _optionsSource.Value;

  public OrdersHostedServiceExecutor(
    IOptions<OrdersHostedServiceExecutorOptions> options,
    ILogger<OrdersHostedServiceExecutor> logger)
  {
    _optionsSource = options;
    _logger = logger;
  }

  public async Task ExecuteAsync(CancellationToken ct)
  {
    while (!ct.IsCancellationRequested)
    {
      var delay = Options.Delay;
      await Task.Delay(delay, ct);

      _logger.LogInformation("Sending new order to order service");
    }
  }
}
