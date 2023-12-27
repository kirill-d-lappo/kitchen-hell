using KitchenHell.ActivityGenerators.Grpc;
using KitchenHell.ActivityGenerators.Services;
using KitchenHell.Common.Web;

namespace KitchenHell.ActivityGenerators;

internal static class Setup
{
  public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
  {
    builder.Services.AddActivityGeneratorsHostedServices();

    builder.Services.AddActivityGeneratorsGrpcClients();

    builder.Host.AddLogging();
    builder.Services.AddHealthChecks();

    return builder;
  }

  public static WebApplication Configure(this WebApplication app)
  {
    app.MapHealthChecks("/healthz");

    return app;
  }
}
