using Google.Protobuf.WellKnownTypes;
using KitchenHell.Protobuf;

namespace KitchenHell.Common.Grpc.Types;

/// <summary>
/// Conversion extensions for <see cref="TimestampOffset"/> type
/// </summary>

// FixMe [2024-04-14 klappo] need to test properly
public static class TimestampOffsetExtensions
{
  /// <summary>
  /// Converts value of <see cref="DateTimeOffset"/> type to grpc-generated type <see cref="TimestampOffset"/>.
  /// </summary>
  /// <param name="value"><see cref="DateTimeOffset"/> value to convert</param>
  /// <returns><see cref="TimestampOffset"/> representation of <see cref="DateTimeOffset"/> value</returns>
  public static TimestampOffset ToTimestampOffset(this DateTimeOffset value)
  {
    return new TimestampOffset
    {
      Offset = value.Offset.ToDuration(),
      Timestamp = value.ToTimestamp(),
    };
  }

  /// <summary>
  /// Converts value of grpc-generated type <see cref="TimestampOffset"/> type to nullable <see cref="DateTimeOffset"/> type.
  /// </summary>
  /// <param name="value"><see cref="TimestampOffset"/> value to convert</param>
  /// <returns><see cref="DateTimeOffset"/> representation of <see cref="TimestampOffset"/> value</returns>
  /// <exception cref="ArgumentNullException">Parameter <paramref name="value"/> is <see langword="null"/></exception>
  public static DateTimeOffset? ToDateTimeOffset(this TimestampOffset value)
  {
    if (value == default)
    {
      return default;
    }

    var offset = value.Offset.ToTimeSpan();
    var localDatetime = value.Timestamp.ToDateTime().Add(offset);

    return new DateTimeOffset(localDatetime.Ticks, offset);
  }
}
