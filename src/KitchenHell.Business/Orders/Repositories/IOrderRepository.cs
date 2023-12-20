namespace KitchenHell.Business.Orders.Repositories;

public interface IOrderRepository
{
    Task<long> InsertAsync(OrderEntity order, CancellationToken ct);

    Task<OrderEntity> GetOrderByIdAsync(long id, CancellationToken ct);

    Task UpdateOrderStatusAsync(long id, OrderStatus status, CancellationToken ct);

    Task UpdateRestaurantOrderStatusAsync(long id, OrderRestaurantStatus status, CancellationToken ct);
}
