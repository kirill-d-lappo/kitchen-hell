using Google.Protobuf.WellKnownTypes;
using KitchenHell.Api.Grpc.Generated.Orders;
using KitchenHell.Api.Grpc.Generated.Restaurants;
using KitchenHell.Common.Web.HostedServices;
using Microsoft.Extensions.Options;

namespace KitchenHell.ActivityGenerators.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public class OrdersHostedServiceExecutor : IHostedServiceExecutor
{
  private readonly IOptions<OrdersHostedServiceExecutorOptions> _optionsSource;
  private readonly ILogger<OrdersHostedServiceExecutor> _logger;
  private readonly OrdersSvc.OrdersSvcClient _ordersClient;
  private readonly RestaurantsSvc.RestaurantsSvcClient _restaurantsClient;

  private readonly HashSet<long> _restaurantIds = new();

  private OrdersHostedServiceExecutorOptions Options => _optionsSource.Value;

  public OrdersHostedServiceExecutor(
    IOptions<OrdersHostedServiceExecutorOptions> options,
    ILogger<OrdersHostedServiceExecutor> logger,
    OrdersSvc.OrdersSvcClient ordersClient,
    RestaurantsSvc.RestaurantsSvcClient restaurantsClient
  )
  {
    _optionsSource = options;
    _logger = logger;
    _ordersClient = ordersClient;
    _restaurantsClient = restaurantsClient;
  }

  public async Task ExecuteAsync(CancellationToken ct)
  {
    while (!ct.IsCancellationRequested)
    {
      var delay = Options.Delay;
      await Task.Delay(delay, ct);

      _logger.LogInformation("Sending new order to order service");

      var request = await GenerateCreateOrderRequest(ct);
      await _ordersClient.CreateOrderAsync(request, cancellationToken: ct);
    }
  }

  private async Task<CreateOrderRequest> GenerateCreateOrderRequest(CancellationToken ct)
  {
    var restaurantId = await GetRandomRestaurantId(ct);

    return new CreateOrderRequest
    {
      RestaurantId = restaurantId,
      CreatedAt = DateTime.UtcNow.ToTimestamp(),
    };
  }

  private async Task<long> GetRandomRestaurantId(CancellationToken ct)
  {
    var response =
      await _restaurantsClient.GetAllRestaurantsAsync(new GetAllRestaurantsRequest(), cancellationToken: ct);

    var restaurants = response.Restaurants;

    var index = Random.Shared.Next(0, restaurants.Count);

    return restaurants[index].Id;
  }
}
