namespace KitchenHell.Orders.Business.Orders.Services;

public interface IOrderService
{
    Task<long> CreateOrderAsync(CreateOrderParams createParams, CancellationToken ct);

    Task<Order> GetOrderByIdAsync(long id, CancellationToken ct);
}
