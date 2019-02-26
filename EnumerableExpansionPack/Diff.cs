using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableExpansionPack
{
  public static partial class SplitExtensions
  {
    public static ThreeWayDiff<TLeft, TRight> Diff<TLeft, TRight, TSharedKey>(
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

      var leftOnly = new List<TLeft>();
      var rightOnly = new List<TRight>();
      var both = new List<Tuple<TLeft, TRight>>();

      foreach (var rightItem in right)
      {
        var rightKey = rightKeySelector(rightItem);
        if (leftDictionary.ContainsKey(rightKey))
        {
          both.Add(new Tuple<TLeft, TRight>(leftDictionary[rightKey], rightItem));
          leftDictionary.Remove(rightKey);
        }
        else
        {
          rightOnly.Add(rightItem);
        }
      }

      leftOnly.AddRange(leftDictionary.Values);

      return new ThreeWayDiff<TLeft, TRight>
      {
        LeftOnly = leftOnly,
        Both = both,
        RightOnly = rightOnly
      };
    }
  }
}
