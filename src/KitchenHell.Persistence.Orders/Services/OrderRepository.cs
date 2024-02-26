using KitchenHell.Business.Orders;
using KitchenHell.Business.Orders.Repositories;
using KitchenHell.Persistence.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KitchenHell.Persistence.Orders.Services;

internal class OrderRepository : IOrderRepository
{
  private readonly IDbContextFactory<OrdersDbContext> _dbContextFactory;
  private readonly ILogger<OrderRepository> _logger;

  public OrderRepository(IDbContextFactory<OrdersDbContext> dbContextFactory, ILogger<OrderRepository> logger)
  {
    _dbContextFactory = dbContextFactory;
    _logger = logger;
  }

  public async Task<long> InsertAsync(OrderEntity order, CancellationToken ct)
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    var orderEntity = new OrderEfEntity();
    MapToEfEntity(order, orderEntity);

    var orderEntry = await dbContext.Orders.AddAsync(orderEntity, ct);
    await dbContext.SaveChangesAsync(ct);

    return orderEntry.Entity.Id;
  }

  public async Task<OrderEntity> GetOrderByIdAsync(long id, CancellationToken ct)
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    var orderEntity = await dbContext.Orders.FirstAsync(o => o.Id == id, ct);

    var order = new OrderEntity();
    MapToEntity(orderEntity, order);

    return order;
  }

  public async Task UpdateOrderStatusAsync(long id, OrderStatus status, CancellationToken ct)
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    var orderEntity = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id, ct);

    if (orderEntity == default)
    {
      _logger.LogError("Order with id {OrderId} was not found and can not be set to status {Status}", id, status);

      return;
    }

    orderEntity.OrderStatus = status;

    await dbContext.SaveChangesAsync(ct);
  }

  public async Task UpdateRestaurantOrderStatusAsync(long id, OrderRestaurantStatus status, CancellationToken ct)
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    var orderEntity = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id, ct);

    if (orderEntity == default)
    {
      _logger.LogError("Order with id {OrderId} was not found and can not be set to status {Status}", id, status);

      return;
    }

    orderEntity.RestaurantStatus = status;

    await dbContext.SaveChangesAsync(ct);
  }

  private static void MapToEntity(OrderEfEntity from, OrderEntity to)
  {
    to.Id = from.Id;
    to.CreatedAt = from.CreatedAt;
    to.RestaurantId = from.RestaurantId;
    to.OrderStatus = from.OrderStatus;
    to.RestaurantStatus = from.RestaurantStatus;
    to.DeliveryStatus = from.DeliveryStatus;
  }

  private static void MapToEfEntity(OrderEntity from, OrderEfEntity to)
  {
    to.RestaurantId = from.RestaurantId;
    to.CreatedAt = from.CreatedAt;
    to.OrderStatus = from.OrderStatus;
    to.RestaurantStatus = from.RestaurantStatus;
    to.DeliveryStatus = from.DeliveryStatus;
  }
}
