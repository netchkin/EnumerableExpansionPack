using System;
using System.Collections.Generic;

namespace EnumerableExpansionPack
{
  public class ThreeWayDiff<TLeft, TRight>
  {
    public IEnumerable<TLeft> LeftOnly { get; internal set; }
    public IEnumerable<Tuple<TLeft, TRight>> Both { get; internal set; }
    public IEnumerable<TRight> RightOnly { get; internal set; }
  }
}
