using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KitchenHell.Common.Web;

public static class WebApplicationExtensions
{
  public static async Task RunScopedServiceAsync<TService>(
    this WebApplication app,
    Func<TService, CancellationToken, Task> runAction,
    CancellationToken ct
  )
  {
    await app.Services.RunScopedServiceAsync(runAction, ct);
  }

  public static async Task RunWithConsoleCancellationAsync(
    this WebApplication app,
    Func<WebApplication, CancellationToken, Task> runAction = default
  )
  {
    var cts = new CancellationTokenSource();
    var ct = cts.Token;

    Console.CancelKeyPress += ConsoleCancelKeyPress;

    if (runAction != default)
    {
      await runAction(app, ct);
    }
    else
    {
      await app.RunAsync(ct);
    }

    return;

    void ConsoleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
      app.Logger.LogWarning("Cancelling server execution...");
      cts.Cancel();
      e.Cancel = true;
    }
  }

  public static async Task RunScopedServiceAsync<TService>(
    this IServiceProvider serviceProvider,
    Func<TService, CancellationToken, Task> runAction,
    CancellationToken ct
  )
  {
    await using var scope = serviceProvider.CreateAsyncScope();
    var service = scope.ServiceProvider.GetRequiredService<TService>();
    await runAction(service, ct);
  }
}
