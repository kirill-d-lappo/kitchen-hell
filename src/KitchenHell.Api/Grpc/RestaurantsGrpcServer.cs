using Grpc.Core;
using KitchenHell.Api.Grpc.Generated.Restaurants;
using KitchenHell.Restaurants.Business.Restaurants;
using Restaurant = KitchenHell.Api.Grpc.Generated.Restaurants.Restaurant;
using RestaurantModel = KitchenHell.Restaurants.Business.Restaurants.Restaurant;

namespace KitchenHell.Api.Grpc;

public class RestaurantsGrpcServer : RestaurantsSvc.RestaurantsSvcBase
{
    private readonly IRestaurantsService _restaurantsService;

    public RestaurantsGrpcServer(IRestaurantsService restaurantsService)
    {
        _restaurantsService = restaurantsService;
    }

    public override async Task<GetAllRestaurantsResponse> GetAllRestaurants(
        GetAllRestaurantsRequest request,
        ServerCallContext context)
    {
        var restaurants = await _restaurantsService.GetAllRestaurantsAsync(context.CancellationToken);

        var restaurantGrpcs = restaurants.Select(MapToGrpc);

        var response = new GetAllRestaurantsResponse();
        response.Restaurants.AddRange(restaurantGrpcs);

        return response;
    }

    private static Restaurant MapToGrpc(RestaurantModel arg)
    {
        if (arg == default)
        {
            return default;
        }

        return new Restaurant
        {
            Id = arg.Id,
            Name = arg.Name,
            FullAddress = arg.FullAddress,
            Latitude = arg.Latitude,
            Longitude = arg.Longitude,
        };
    }
}
