namespace KitchenHell.Common;

/// <summary>
///   Common extension methods for <see cref="IEnumerable{T}" />
/// </summary>
public static partial class EnumerableExtensions
{
  /// <summary>
  ///   Returns empty enumerable of type <typeparamref name="T" /> when <paramref name="source" /> is null. Otherwise returns
  ///   <paramref name="source" /> enumerable.
  /// </summary>
  /// <param name="source">Target enumerable</param>
  /// <typeparam name="T">Type of items in enumerable</typeparam>
  /// <returns>Empty enumerable of type <typeparamref name="T" /> or <paramref name="source" /> enumerable.</returns>
  public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
  {
    return source ?? ArraySegment<T>.Empty;
  }
}
