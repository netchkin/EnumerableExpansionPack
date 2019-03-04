using System;
using System.Collections.Generic;

namespace EnumerableExpansionPack
{
  public class DiffResult<TLeft, TRight>
  {
    public IEnumerable<TLeft> LeftOnly { get; internal set; }
    public IEnumerable<Both<TLeft, TRight>> Both { get; internal set; }
    public IEnumerable<TRight> RightOnly { get; internal set; }
  }

  public class Both<TLeft, TRight>
  {
    public TLeft Left { get; set; }
    public TRight Right { get; set; }
  }
}
