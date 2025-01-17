namespace KitchenHell.Api.Grpc;

public static class KitchenHellGrpcServerRegistrations
{
  public static IEndpointRouteBuilder MapKitchenHellGrpcServer(this IEndpointRouteBuilder builder)
  {
    builder.MapOrdersGrpcServer();
    builder.MapRestaurantsGrpcServer();
    builder.MapTestGrpcServer();

    return builder;
  }

  private static GrpcServiceEndpointConventionBuilder MapOrdersGrpcServer(this IEndpointRouteBuilder builder)
  {
    return builder.MapGrpcService<OrdersGrpcServer>();
  }

  private static GrpcServiceEndpointConventionBuilder MapRestaurantsGrpcServer(this IEndpointRouteBuilder builder)
  {
    return builder.MapGrpcService<RestaurantsGrpcServer>();
  }

  private static GrpcServiceEndpointConventionBuilder MapTestGrpcServer(this IEndpointRouteBuilder builder)
  {
    return builder.MapGrpcService<TestGrpcServer>();
  }
}
