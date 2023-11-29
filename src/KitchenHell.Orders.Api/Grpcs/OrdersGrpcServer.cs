using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using KitchenHell.GrpcGen.Orders;
using KitchenHell.Orders.Api.Business.Orders.Services;

namespace KitchenHell.Orders.Api.Grpcs;

public class OrdersGrpcServer : OrdersSvc.OrdersSvcBase
{
    private readonly IOrderService _orderService;

    public OrdersGrpcServer(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public override async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        var createParams = new CreateOrderParams
        {
            CreatedAt = request.CreatedAt?.ToDateTimeOffset(),
        };

        var orderId = await _orderService.CreateOrderAsync(createParams, context.CancellationToken);
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

    private static Order MapToGrpcOrder(Business.Orders.Order order)
    {
        return new Order
        {
            OrderId = order.Id,
            CreatedAt = order.CreatedAt.ToTimestamp(),
        };
    }
}