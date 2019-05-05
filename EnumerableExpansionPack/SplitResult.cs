using System.Collections.Generic;

namespace EnumerableExpansionPack
{
  /// <summary>
  /// Result of the split operation
  /// </summary>
  public class SplitResult<TItem>
  {
    /// <summary>
    /// Items that fulfill the split condition.
    /// </summary>
    public IEnumerable<TItem> Included { get; internal set; }

    /// <summary>
    /// Items that do not fulfill the split condition.
    /// </summary>
    public IEnumerable<TItem> Excluded { get; internal set; }
  }
}
