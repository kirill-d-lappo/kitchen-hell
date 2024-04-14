namespace KitchenHell.Business.Common;

public interface IDateTimeProvider
{
  DateTimeOffset UtcNow { get; }
}
