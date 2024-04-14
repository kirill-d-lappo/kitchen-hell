namespace KitchenHell.Common;

/// <summary>
///   Common extension methods for <see cref="IEnumerable{T}" />
/// </summary>
public static partial class EnumerableExtensions
{
  /// <summary>
  ///   Joins the elements of a sequence by using the specified separator between each element.
  ///   <br />
  ///   Method uses .ToString() to convert each element to string.
  /// </summary>
  /// <param name="source">Source enumeration</param>
  /// <param name="separator">Parts separator</param>
  /// <typeparam name="T">Type of items in enumeration</typeparam>
  /// <returns>Result joined string</returns>
  public static string JoinToString<T>(this IEnumerable<T> source, string separator = null)
  {
    if (source is null)
    {
      throw new ArgumentNullException(nameof(source));
    }

    return string.Join(separator, source);
  }

  /// <summary>
  ///   Joins the elements of a sequence by using the specified separator between each element.
  ///   <br />
  ///   Method uses .ToString() to convert each element to string.
  /// </summary>
  /// <param name="source">Source enumeration</param>
  /// <param name="separator">Parts separator</param>
  /// <typeparam name="T">Type of items in enumeration</typeparam>
  /// <returns>Result joined string</returns>
  public static string JoinToString<T>(this IEnumerable<T> source, char separator)
  {
    if (source is null)
    {
      throw new ArgumentNullException(nameof(source));
    }

    return string.Join(separator, source);
  }
}
