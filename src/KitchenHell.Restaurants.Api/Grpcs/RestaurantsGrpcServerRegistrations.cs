namespace KitchenHell.Restaurants.Api.Grpcs;

public static class RestaurantsGrpcServerRegistrations
{
    public static GrpcServiceEndpointConventionBuilder MapRestaurantsGrpcServer(this IEndpointRouteBuilder builder)
    {
        return builder.MapGrpcService<RestaurantsGrpcServer>();
    }
}