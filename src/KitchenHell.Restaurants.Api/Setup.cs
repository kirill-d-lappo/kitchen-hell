using KitchenHell.Common.GrpcServices;
using KitchenHell.Common.Web;
using KitchenHell.Restaurants.Api.Grpcs;
using KitchenHell.Restaurants.Business;
using KitchenHell.Restaurants.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Restaurants.Api;

internal static class Setup
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddRestaurantsBusiness();
        builder.Services.AddRestaurantsPersistence();

        builder.Services.AddHealthChecks();

        builder.Services.AddGrpcServices();
        builder.Host.AddLogging();

        return builder;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.MapHealthChecks("/healthz");

        app.MapGrpcHealthChecksService();
        app.MapGrpcReflectionService();

        app.MapRestaurantsGrpcServer();

        return app;
    }

    public static async Task MigrateAndRunAsync(this WebApplication app)
    {
        await app.MigrateAsync<RestaurantsDbContext>(CancellationToken.None);

        await app.RunAsync();
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