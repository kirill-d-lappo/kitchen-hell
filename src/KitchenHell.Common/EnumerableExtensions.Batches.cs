using System.Collections;

namespace KitchenHell.Common;

/// <summary>
///   Helper batch extensions for enumerables
/// </summary>
public static partial class EnumerableExtensions
{
  /// <summary>
  ///   Splits enumerable by batches
  /// </summary>
  /// <param name="source">Source enumerable</param>
  /// <param name="batchSize">Amount of elements in one batch</param>
  /// <typeparam name="T">Type of elements in enumerable</typeparam>
  /// <returns>Enumerable of batches as collections of elements from source enumerable</returns>
  /// <exception cref="ArgumentNullException">Source enumerable is null</exception>
  /// <exception cref="ArgumentException">batch size must be more than 0</exception>
  public static IEnumerable<ICollection<T>> TakeByBatch<T>(
    this IEnumerable<T> source,
    int batchSize
  )
  {
    if (source == default)
    {
      throw new ArgumentNullException(nameof(source));
    }

    if (batchSize <= 0)
    {
      throw new ArgumentException("Must greater than 0", nameof(batchSize));
    }

    return new TakeByBatchEnumerable<T>(source, batchSize);
  }

  private class TakeByBatchEnumerable<T> : IEnumerable<ICollection<T>>
  {
    private readonly int _batchSize;
    private readonly IEnumerable<T> _innerEnumerable;

    public TakeByBatchEnumerable(IEnumerable<T> innerEnumerable, int batchSize)
    {
      _innerEnumerable = innerEnumerable;
      _batchSize = batchSize;
    }

    public IEnumerator<ICollection<T>> GetEnumerator()
    {
      return new TakeByBatchEnumerator<T>(_innerEnumerable.GetEnumerator(), _batchSize);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }

  private class TakeByBatchEnumerator<T> : IEnumerator<ICollection<T>>
  {
    private readonly int _batchSize;
    private readonly IEnumerator<T> _innerEnumerator;

    private bool _previousIsMoved = true;

    public TakeByBatchEnumerator(IEnumerator<T> innerEnumerator, int batchSize)
    {
      _innerEnumerator = innerEnumerator;
      _batchSize = batchSize;
    }

    public ICollection<T> Current { get; private set; }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
      if (!_previousIsMoved)
      {
        Current = Array.Empty<T>();

        return false;
      }

      _previousIsMoved = false;
      var currentBatchIndex = 0;
      var batch = new List<T>();
      while (currentBatchIndex < _batchSize && (_previousIsMoved = _innerEnumerator.MoveNext()))
      {
        batch.Add(_innerEnumerator.Current);
        currentBatchIndex++;
      }

      Current = batch;

      return batch.Count > 0;
    }

    public void Dispose()
    {
      _innerEnumerator?.Dispose();
    }

    public void Reset()
    {
      _innerEnumerator.Reset();
    }
  }
}
