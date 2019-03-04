using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableExpansionPack
{
  public static partial class SplitExtensions
  {
    public static DiffResult<TLeft, TRight> Diff<TLeft, TRight, TSharedKey>(
      this IEnumerable<TLeft> left,
      IEnumerable<TRight> right,
      Func<TLeft, TSharedKey> leftKeySelector,
      Func<TRight, TSharedKey> rightKeySelector)
    {
      if (left == null) throw new ArgumentNullException(nameof(left));
      if (right == null) throw new ArgumentNullException(nameof(right));
      if (leftKeySelector == null) throw new ArgumentNullException(nameof(leftKeySelector));
      if (rightKeySelector == null) throw new ArgumentNullException(nameof(rightKeySelector));

      var leftDictionary = left.ToDictionary(leftKeySelector);

      var rightOnly = new List<TRight>();
      var both = new List<Both<TLeft, TRight>>();

      foreach (var rightItem in right)
      {
        var rightKey = rightKeySelector(rightItem);
        if (leftDictionary.ContainsKey(rightKey))
        {
          both.Add(new Both<TLeft, TRight> { Left = leftDictionary[rightKey], Right = rightItem });
          leftDictionary.Remove(rightKey);
        }
        else
        {
          rightOnly.Add(rightItem);
        }
      }

      return new DiffResult<TLeft, TRight>
      {
        LeftOnly = leftDictionary.Values,
        Both = both,
        RightOnly = rightOnly
      };
    }

    public static DiffResult<TItem, TItem> Diff<TItem, TKey>(
      this IEnumerable<TItem> left,
      IEnumerable<TItem> right,
      Func<TItem, TKey> keySelector)
    {
      if (left == null) throw new ArgumentNullException(nameof(left));
      if (right == null) throw new ArgumentNullException(nameof(right));

      var leftDictionary = left.ToDictionary(keySelector);

      var rightOnly = new List<TItem>();
      var both = new List<Both<TItem, TItem>>();

      foreach (var rightItem in right)
      {
        var rightKey = keySelector(rightItem);
        if (leftDictionary.ContainsKey(rightKey))
        {
          both.Add(new Both<TItem, TItem> { Left = leftDictionary[rightKey], Right = rightItem });
          leftDictionary.Remove(rightKey);
        }
        else
        {
          rightOnly.Add(rightItem);
        }
      }

      return new DiffResult<TItem, TItem>
      {
        LeftOnly = leftDictionary.Values,
        Both = both,
        RightOnly = rightOnly
      };
    }
  }
}
