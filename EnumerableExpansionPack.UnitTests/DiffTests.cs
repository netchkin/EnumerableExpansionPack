using System;
using System.Collections.Generic;
using Xunit;
using EnumerableExpansionPack;

namespace EnumerableExpansionPack.UnitTests
{
  public class DiffTests
  {
    [Fact]
    public void WhenPartialIntersect_OfDifferentTypes_ReturnsAllNonEmptyGroups()
    {
      var left = new [] { 1, 2, 4, 10 };
      var right = new [] { 2, 3, 4, 5 };

      var result = left.Diff(right, i => i, i => i);

      Assert.Equal(new [] { 1, 10 }, result.LeftOnly);
      Assert.Equal(
        new []
        {
          new Both<int, int> { Left = 2, Right = 2 },
          new Both<int, int> { Left = 4, Right = 4 }
        },
        result.Both,
        new BothEqualityComparer<int>(EqualityComparer<int>.Default));
      Assert.Equal(new [] { 3, 5 }, result.RightOnly);
    }

    [Fact]
    public void WhenPartialIntersect_OfSameTypes_ReturnsAllNonEmptyGroups()
    {
      var left = new [] { 1, 2, 4, 10 };
      var right = new [] { 2, 3, 4, 5 };

      var result = left.Diff(right, i => i);

      Assert.Equal(new [] { 1, 10 }, result.LeftOnly);
      Assert.Equal(
        new []
        {
          new Both<int, int> { Left = 2, Right = 2 },
          new Both<int, int> { Left = 4, Right = 4 }
        },
        result.Both,
        new BothEqualityComparer<int>(EqualityComparer<int>.Default));
      Assert.Equal(new [] { 3, 5 }, result.RightOnly);
    }

    private class BothEqualityComparer<TItem> : IEqualityComparer<Both<TItem, TItem>>
    {
      private readonly IEqualityComparer<TItem> _itemComparer;

      public BothEqualityComparer(IEqualityComparer<TItem> itemComparer)
      {
        _itemComparer = itemComparer;
      }

      public bool Equals(Both<TItem, TItem> x, Both<TItem, TItem> y)
        => _itemComparer.Equals(x.Left, y.Left) && _itemComparer.Equals(x.Right, y.Right);

      public int GetHashCode(Both<TItem, TItem> obj) => HashCode.Combine(
        _itemComparer.GetHashCode(obj.Left),
        _itemComparer.GetHashCode(obj.Right));
    }
  }
}
