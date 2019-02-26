using System;
using Xunit;
using EnumerableExpansionPack;

namespace EnumerableExpansionPack.UnitTests
{
  public class SplitByTests
  {
    [Fact]
    public void WhenCollectionContainsBothGrous_ReturnCorrectSplit()
    {
      var original = new [] { 1, 2, 3, 4, 5, 6 };
      var expectedLeft = new [] { 2, 4, 6 };
      var expectedRight = new [] { 1, 3, 5 };

      var result = original.SplitBy(i => i % 2 == 0);

      Assert.Equal(expectedLeft, result.Left);
      Assert.Equal(expectedRight, result.Right);
    }
  }
}
