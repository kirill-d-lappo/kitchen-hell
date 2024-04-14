namespace KitchenHell.Common;

/// <summary>
///   Helper extension methods to check-n-get value from nullable reference
/// </summary>
public static class NullableExtensions
{
  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this long? nullable, out long value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }

  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this int? nullable, out int value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }

  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this double? nullable, out double value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }

  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this decimal? nullable, out decimal value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }

  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this float? nullable, out float value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }

  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this Guid? nullable, out Guid value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }

  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this DateTime? nullable, out DateTime value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }

  /// <summary>
  ///   Returns <see langword="true" /> and sets <paramref name="value" /> when <paramref name="nullable" /> has value.
  ///   <br />
  ///   Otherwise, returns <see langword="false" /> and sets <paramref name="value" /> to default value.
  /// </summary>
  /// <param name="nullable">Nullable value to test</param>
  /// <param name="value">Value that <paramref name="nullable" /> contains</param>
  /// <returns></returns>
  public static bool TryGetValue(this DateTimeOffset? nullable, out DateTimeOffset value)
  {
    if (nullable.HasValue)
    {
      value = nullable.Value;

      return true;
    }

    value = default;

    return false;
  }
}
