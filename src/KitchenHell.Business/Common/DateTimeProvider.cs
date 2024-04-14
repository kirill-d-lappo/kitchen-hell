namespace KitchenHell.Business.Common;

internal class DateTimeProvider : IDateTimeProvider
{
  public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
