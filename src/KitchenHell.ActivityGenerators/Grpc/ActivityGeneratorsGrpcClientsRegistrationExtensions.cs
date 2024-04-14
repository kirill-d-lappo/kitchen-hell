using KitchenHell.Api.Grpc.Generated.Orders;
using KitchenHell.Api.Grpc.Generated.Restaurants;
using KitchenHell.Common.Grpc.Registrations;

namespace KitchenHell.ActivityGenerators.Grpc;

public static class ActivityGeneratorsGrpcClientsRegistrationExtensions
{
  public static IServiceCollection AddActivityGeneratorsGrpcClients(this IServiceCollection services)
  {
    services.AddConfiguredGrpcClient<OrdersSvc.OrdersSvcClient>();
    services.AddConfiguredGrpcClient<RestaurantsSvc.RestaurantsSvcClient>();

    return services;
  }
}
