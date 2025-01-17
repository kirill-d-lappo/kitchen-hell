using KitchenHell.Api.Grpc;
using KitchenHell.Business;
using KitchenHell.Business.Orders.Messaging;
using KitchenHell.Common.Grpc.Registrations;
using KitchenHell.Common.Web;
using KitchenHell.Persistence.Orders;
using KitchenHell.Persistence.Restaurants;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Api;

internal static class Setup
{
  public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
  {
    builder.Services.AddKitchenHellBusiness();
    builder.Services.AddOrdersPersistence();
    builder.Services.AddRestaurantsPersistence();

    builder.Services.AddHealthChecks();

    builder.Services.AddGrpcServices();
    builder.Host.AddLogging();

    builder.Services.AddOrdersMessaging();

    return builder;
  }

  public static WebApplication Configure(this WebApplication app)
  {
    app.MapHealthChecks("/healthz");

    app.MapGrpcHealthChecksService();
    app.MapGrpcReflectionService();

    app.MapKitchenHellGrpcServer();

    return app;
  }

  public static async Task MigrateDatabasesAsync(this WebApplication app)
  {
    await app.MigrateAsync<OrdersDbContext>(CancellationToken.None);
    await app.MigrateAsync<RestaurantsDbContext>(CancellationToken.None);
  }

  public static async Task RunWithConsoleCancellationAsync(this WebApplication app)
  {
    var cts = new CancellationTokenSource();

    Console.CancelKeyPress += ConsoleCancelKeyPress;

    await app.RunAsync(cts.Token);

    return;

    void ConsoleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
      app.Logger.LogWarning("Cancelling server execution...");
      cts.Cancel();
      e.Cancel = true;
    }
  }

  private static async Task MigrateAsync<TDbContext>(this IHost app, CancellationToken ct)
    where TDbContext : DbContext
  {
    await using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
    var factor = serviceScope.ServiceProvider.GetService<IDbContextFactory<TDbContext>>();
    await using var context = await factor.CreateDbContextAsync(ct);
    await context.Database.MigrateAsync(ct);
  }
}
