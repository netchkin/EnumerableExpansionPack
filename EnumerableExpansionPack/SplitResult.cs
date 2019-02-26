using System.Collections.Generic;

namespace EnumerableExpansionPack
{
  public class SplitResult<TItem>
  {
    public IEnumerable<TItem> Left { get; internal set; }
    public IEnumerable<TItem> Right { get; internal set; }
  }
}
