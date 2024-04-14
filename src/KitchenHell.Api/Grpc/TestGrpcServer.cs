using Grpc.Core;
using KitchenHell.Api.Grpc.Generated.Orders;
using KitchenHell.Business.Messages;
using KitchenHell.Common.Grpc.Types;
using KitchenHell.Messaging.Producers;

namespace KitchenHell.Api.Grpc;

public class TestGrpcServer : TestSvc.TestSvcBase
{
  private readonly IMessageProducer<string, OrderCreatedMessage> _orderCreatedMessageProducer;

  public TestGrpcServer(IMessageProducer<string, OrderCreatedMessage> orderCreatedMessageProducer)
  {
    _orderCreatedMessageProducer = orderCreatedMessageProducer;
  }

  public override async Task<ProduceOrderCreatedMessageResponse> ProduceOrderCreatedMessage(
    ProduceOrderCreatedMessageRequest request,
    ServerCallContext context
  )
  {
    var ct = context.CancellationToken;
    var restaurantId = request.RestaurantId;

    var message = new OrderCreatedMessage
    {
      OrderId = restaurantId,
      RestaurantId = restaurantId,
      Timestamp = request.CreatedAt?.ToDateTimeOffset() ?? DateTimeOffset.UtcNow,
    };

    await _orderCreatedMessageProducer.ProduceAsync(message, ct);

    return new ProduceOrderCreatedMessageResponse
    {
      RestaurantId = restaurantId,
    };
  }
}
