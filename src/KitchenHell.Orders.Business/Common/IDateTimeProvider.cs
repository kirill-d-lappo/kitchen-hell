namespace KitchenHell.Orders.Business.Common;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}