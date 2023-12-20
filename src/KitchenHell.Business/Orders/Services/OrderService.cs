using KitchenHell.Business.Common;
using KitchenHell.Business.Orders.Messages;
using KitchenHell.Business.Orders.Messages.Producers;
using KitchenHell.Business.Orders.Repositories;

namespace KitchenHell.Business.Orders.Services;

internal class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IOrderCreatedMessageProducer _orderCreatedMessageProducer;

    public OrderService(
        IOrderRepository orderRepository,
        IDateTimeProvider dateTimeProvider,
        IOrderCreatedMessageProducer orderCreatedMessageProducer)
    {
        _orderRepository = orderRepository;
        _dateTimeProvider = dateTimeProvider;
        _orderCreatedMessageProducer = orderCreatedMessageProducer;
    }

    public async Task<long> CreateOrderAsync(CreateOrderParams createParams, CancellationToken ct)
    {
        var now = _dateTimeProvider.UtcNow;
        var order = new OrderEntity();

        order.RestaurantId = createParams.RestaurantId;
        order.CreatedAt = createParams.CreatedAt.GetValueOrDefault(now);
        order.OrderStatus = OrderStatus.Created;
        order.RestaurantStatus = OrderRestaurantStatus.Pending;
        order.DeliveryStatus = OrderDeliveryStatus.Pending;

        var orderId = await _orderRepository.InsertAsync(order, ct);

        var createdMessage = new OrderCreatedMessage
        {
            OrderId = orderId, RestaurantId = createParams.RestaurantId,
        };

        await _orderCreatedMessageProducer.ProduceAsync(createdMessage, ct);

        return orderId;
    }

    public async Task<Order> GetOrderByIdAsync(long id, CancellationToken ct)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id, ct);

        return MapToDomain(order);
    }

    private static Order MapToDomain(OrderEntity order)
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
