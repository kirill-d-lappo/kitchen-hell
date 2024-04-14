namespace KitchenHell.Common;

public static class StringExtensions
{
  public static string TrimEnd(this string source, string value)
  {
    if (!source.EndsWith(value))
    {
      return source;
    }

    return source.Remove(source.LastIndexOf(value, StringComparison.Ordinal));
  }

  /// <summary>
  ///   Truncates <paramref name="source" /> string value to specified <paramref name="length" />.
  /// </summary>
  /// <param name="source">Source string to truncate.</param>
  /// <param name="length">Amount of chars to keep.</param>
  /// <returns>
  ///   Truncated value or source string, when <paramref name="length" /> parameter is more than length of
  ///   <paramref name="source" /> string.
  /// </returns>
  /// <exception cref="ArgumentNullException"><paramref name="source" /> is null</exception>
  /// <exception cref="ArgumentException"><paramref name="length" /> is lesser that 0</exception>
  public static string Truncate(this string source, int length)
  {
    if (source == default)
    {
      throw new ArgumentNullException(nameof(source));
    }

    if (length < 0)
    {
      throw new ArgumentException("Must be more or equal then 0");
    }

    if (length == 0)
    {
      return string.Empty;
    }

    if (source.Length > length)
    {
      return source[..length];
    }

    return source;
  }
}
