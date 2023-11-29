using KitchenHell.Orders.Persistence;
using KitchenHell.Orders.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenHell.Orders.Api.Business.Orders.Services;

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

    private static void MapToModel(OrderEntity orderEntity, Order order)
    {
        order.Id = orderEntity.Id;
        order.CreatedAt = orderEntity.CreatedAt;
    }

    private static void MapToEntity(Order order, OrderEntity orderEntity)
    {
        orderEntity.CreatedAt = order.CreatedAt;
    }
}