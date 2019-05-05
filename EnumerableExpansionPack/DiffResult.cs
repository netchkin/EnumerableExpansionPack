using System;
using System.Collections.Generic;

namespace EnumerableExpansionPack
{
  /// <summary>
  /// Result of the diff operation.
  /// </summary>
  public class DiffResult<TLeft, TRight>
  {
    /// <summary>
    /// Items that were present only in the left source of the diff.
    /// </summary>
    public IEnumerable<TLeft> LeftOnly { get; internal set; }

    /// <summary>
    /// Items that were present in the both sources of the diff.
    /// </summary>
    public IEnumerable<Both<TLeft, TRight>> Both { get; internal set; }

    /// <summary>
    /// Items that were present only in the right source of the diff.
    /// </summary>
    public IEnumerable<TRight> RightOnly { get; internal set; }
  }

  /// <summary>
  /// Correlated items from two sources of the diff.
  /// </summary>
  public class Both<TLeft, TRight>
  {
    /// <summary>
    /// Item from the left source
    /// </summary>
    public TLeft Left { get; set; }

    /// <summary>
    /// Item from the right source
    /// </summary>
    public TRight Right { get; set; }
  }
}
