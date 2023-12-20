namespace KitchenHell.Orders.Business.Common;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
