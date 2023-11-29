using KitchenHell.Orders.Api.Business;
using KitchenHell.Orders.Api.Grpcs;
using KitchenHell.Orders.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace KitchenHell.Orders.Api;

internal static class Setup
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddBusiness();
        builder.Services.AddPersistence();

        builder.Services.AddHealthChecks();

        builder.Services.AddGrpc();
        builder.Services.AddGrpcReflection();
        builder.Services.AddGrpcHealthChecks()
            .AddCheck("live", () => HealthCheckResult.Healthy());

        builder.Host.UseSerilog(
            (context, services, config) =>
            {
                config.MinimumLevel.Warning();

                config.WriteTo.Async(
                    wt =>
                    {
                        wt.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss}] [{Level:u4}] <s:{SourceContext}> {Message:lj}{NewLine}{Exception}");
                    });

                config.Destructure.ToMaximumDepth(2);
                config.Destructure.ToMaximumStringLength(60);
                config.Destructure.ToMaximumCollectionCount(10);

                config
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);
            });

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