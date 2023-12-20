using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using KitchenHell.Api.Grpc.Generated.Orders;
using KitchenHell.Orders.Business.Orders.Services;

namespace KitchenHell.Api.Grpc;

public class OrdersGrpcServer : OrdersSvc.OrdersSvcBase
{
    private readonly IOrderService _orderService;

    public OrdersGrpcServer(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public override async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        var ct = context.CancellationToken;
        var createParams = new CreateOrderParams
        {
            RestaurantId = request.RestaurantId,
            CreatedAt = request.CreatedAt?.ToDateTimeOffset(),
        };

        var orderId = await _orderService.CreateOrderAsync(createParams, ct);
        var result = new CreateOrderResponse
        {
            OrderId = orderId,
        };

        return result;
    }

    public override async Task<GetOrderResponse> GetOrder(GetOrderRequest request, ServerCallContext context)
    {
        var orderId = request.OrderId;
        var order = await _orderService.GetOrderByIdAsync(orderId, context.CancellationToken);
        var grpcOrder = MapToGrpcOrder(order);

        var result = new GetOrderResponse
        {
            Order = grpcOrder,
        };

        return result;
    }

    private static Order MapToGrpcOrder(Orders.Business.Orders.Order order)
    {
        return new Order
        {
            OrderId = order.Id,
            CreatedAt = order.CreatedAt.ToTimestamp(),
        };
    }
}