using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableExpansionPack
{
  public static partial class SplitExtensions
  {
    public static SplitResult<TItem> SplitBy<TItem>(
      this IEnumerable<TItem> @this,
      Func<TItem, bool> splitCondition)
    {
      if (@this == null) throw new ArgumentNullException(nameof(@this));
      if (splitCondition == null) throw new ArgumentNullException(nameof(splitCondition));

      var left = new List<TItem>();
      var right = new List<TItem>();

      foreach (var item in @this)
      {
        if (splitCondition(item))
        {
          left.Add(item);
        }
        else
        {
          right.Add(item);
        }
      }

      return new SplitResult<TItem>
      {
        Left = left,
        Right = right
      };
    }
  }
}
