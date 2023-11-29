namespace KitchenHell.Orders.Api.Grpcs;

public static class OrdersGrpcServerRegistrations
{
    public static GrpcServiceEndpointConventionBuilder MapOrdersGrpcServer(this IEndpointRouteBuilder builder)
    {
        return builder.MapGrpcService<OrdersGrpcServer>();
    }
}