using KitchenHell.Orders.Business.Common;

namespace KitchenHell.Orders.Business.Orders.Services;

internal class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderService(IOrderRepository orderRepository, IDateTimeProvider dateTimeProvider)
    {
        _orderRepository = orderRepository;
        _dateTimeProvider = dateTimeProvider;
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

        return orderId;
    }

    public async Task<Order> GetOrderByIdAsync(long id, CancellationToken ct)
    {
        return await _orderRepository.GetOrderByIdAsync(id, ct);
    }
}