using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KitchenHell.Common.Grpc.Registrations;

public static class GrpcRegistrations
{
  public static IServiceCollection AddGrpcServices(
    this IServiceCollection services
  )
  {
    services.AddGrpc();
    services.AddGrpcReflection();
    services.AddGrpcHealthChecks()
      .AddCheck("live", () => HealthCheckResult.Healthy());

    return services;
  }
}
