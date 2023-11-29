namespace KitchenHell.Orders.Api.Business.Orders.Services;

public interface IOrderRepository
{
    Task<long> InsertAsync(Order order, CancellationToken ct);

    Task<Order> GetOrderByIdAsync(long id, CancellationToken ct);
}