using KitchenHell.Business.Common;
using KitchenHell.Business.Messages;
using KitchenHell.Business.Orders.Repositories;
using KitchenHell.Messaging.Producers;

namespace KitchenHell.Business.Orders.Services;

internal class OrderService : IOrderService
{
  private readonly IDateTimeProvider _dateTimeProvider;
  private readonly IMessageProducer<string, OrderCreatedMessage> _orderCreatedMessageProducer;
  private readonly IOrderRepository _orderRepository;

  public OrderService(
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IMessageProducer<string, OrderCreatedMessage> orderCreatedMessageProducer
  )
  {
    _orderRepository = orderRepository;
    _dateTimeProvider = dateTimeProvider;
    _orderCreatedMessageProducer = orderCreatedMessageProducer;
  }

  public async Task<long> CreateOrderAsync(CreateOrderParams createParams, CancellationToken ct)
  {
    var now = _dateTimeProvider.UtcNow;
    var order = new Order();

    order.RestaurantId = createParams.RestaurantId;
    order.CreatedAt = createParams.CreatedAt.GetValueOrDefault(now);
    order.OrderStatus = OrderStatus.Created;
    order.RestaurantStatus = OrderRestaurantStatus.Pending;
    order.DeliveryStatus = OrderDeliveryStatus.Pending;

    var orderId = await _orderRepository.InsertAsync(order, ct);

    var createdMessage = new OrderCreatedMessage
    {
      OrderId = orderId,
      RestaurantId = createParams.RestaurantId,
      Timestamp = createParams.CreatedAt ?? DateTimeOffset.UtcNow,
    };

    await _orderCreatedMessageProducer.ProduceAsync(createdMessage, ct);

    return orderId;
  }

  public async Task<Order> GetOrderByIdAsync(long id, CancellationToken ct)
  {
    var order = await _orderRepository.GetOrderByIdAsync(id, ct);

    return MapToDomain(order);
  }

  private static Order MapToDomain(Order order)
  {
    if (order == default)
    {
      return default;
    }

    return new Order
    {
      Id = order.Id,
      CreatedAt = order.CreatedAt,
      RestaurantId = order.RestaurantId,
      OrderStatus = order.OrderStatus,
      RestaurantStatus = order.RestaurantStatus,
      DeliveryStatus = order.DeliveryStatus,
    };
  }
}
