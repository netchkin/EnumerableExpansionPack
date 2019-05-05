using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableExpansionPack
{
  public static partial class SplitExtensions
  {
    /// <summary>
    /// Splits a collection based on a condition into two collections.
    /// </summary>
    /// <param name="this">Collection to split</param>
    /// <param name="splitCondition">Condition based on which the items from the collection should be split</param>
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
        Included = left,
        Excluded = right
      };
    }
  }
}
