using KitchenHell.Common.GrpcServices;
using KitchenHell.Common.Web;
using KitchenHell.Orders.Api.Grpcs;
using KitchenHell.Orders.Business;
using KitchenHell.Orders.Business.Messages;
using KitchenHell.Orders.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Orders.Api;

internal static class Setup
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddOrdersBusiness();
        builder.Services.AddOrdersPersistence();

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

        app.MapOrdersGrpcServer();

        return app;
    }

    public static async Task MigrateAndRunAsync(this WebApplication app)
    {
        await app.MigrateAsync(CancellationToken.None);

        await app.RunAsync();
    }

    private static async Task MigrateAsync(this IHost app, CancellationToken ct)
    {
        await using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
        var factor = serviceScope.ServiceProvider.GetService<IDbContextFactory<OrdersDbContext>>();
        await using var context = await factor.CreateDbContextAsync(ct);
        await context.Database.MigrateAsync(ct);
    }
}