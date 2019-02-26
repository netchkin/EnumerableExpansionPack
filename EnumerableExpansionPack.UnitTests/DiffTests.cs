using System;
using Xunit;
using EnumerableExpansionPack;

namespace EnumerableExpansionPack.UnitTests
{
  public class DiffTests
  {
    [Fact]
    public void WhenPartialIntersect_ReturnsAllNonEmptyGroups()
    {
      var left = new [] { 1, 2, 4, 10 };
      var right = new [] { 2, 3, 4, 5 };

      var result = left.Diff(right, i => i, i => i);

      Assert.Equal(new [] { 1, 10 }, result.LeftOnly);
      Assert.Equal(new [] { new Tuple<int, int>(2, 2), new Tuple<int, int>(4, 4) }, result.Both);
      Assert.Equal(new [] { 3, 5 }, result.RightOnly);
    }
  }
}
