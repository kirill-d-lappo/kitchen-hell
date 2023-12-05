using KitchenHell.Orders.Business.Orders;
using KitchenHell.Orders.Business.Orders.Services;
using KitchenHell.Orders.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Orders.Persistence.Services;

internal class OrderRepository : IOrderRepository
{
    private readonly IDbContextFactory<OrdersDbContext> _dbContextFactory;

    public OrderRepository(IDbContextFactory<OrdersDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<long> InsertAsync(Order order, CancellationToken ct)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        var orderEntity = new OrderEntity();
        MapToEntity(order, orderEntity);

        var orderEntry = await dbContext.Orders.AddAsync(orderEntity, ct);
        await dbContext.SaveChangesAsync(ct);

        return orderEntry.Entity.Id;
    }

    public async Task<Order> GetOrderByIdAsync(long id, CancellationToken ct)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        var orderEntity = await dbContext.Orders.FirstAsync(o => o.Id == id, ct);

        var order = new Order();
        MapToModel(orderEntity, order);

        return order;
    }

    private static void MapToModel(OrderEntity from, Order to)
    {
        to.Id = from.Id;
        to.CreatedAt = from.CreatedAt;
        to.RestaurantId = from.RestaurantId;
        to.OrderStatus = from.OrderStatus;
        to.RestaurantStatus = from.RestaurantStatus;
        to.DeliveryStatus = from.DeliveryStatus;
    }

    private static void MapToEntity(Order from, OrderEntity to)
    {
        to.RestaurantId = from.RestaurantId;
        to.CreatedAt = from.CreatedAt;
        to.OrderStatus = from.OrderStatus;
        to.RestaurantStatus = from.RestaurantStatus;
        to.DeliveryStatus = from.DeliveryStatus;
    }
}