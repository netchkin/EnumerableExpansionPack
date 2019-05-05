using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableExpansionPack
{
  public static partial class SplitExtensions
  {
    /// <summary>
    /// Compares two source collections using a different key selector for each source.
    /// </summary>
    /// <param name="left">Left source collection</param>
    /// <param name="right">Right source collection</param>
    /// <param name="leftKeySelector">Function that extracts key from items of the left source to be compared with the right source</param>
    /// <param name="rightKeySelector">Function that extracts key from items of the right source to be compared with the left source</param>
    /// <typeparam name="TLeft">Type of items in the left source</typeparam>
    /// <typeparam name="TRight">Type of items in the right source</typeparam>
    /// <typeparam name="TSharedKey">Type of the key based on which items from both sources can be compared</typeparam>
    /// <returns>Diff explaining which items are in both, left ony or right only source collections</returns>
    /// <exception cref="ArgumentNullException"></exception>
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

    /// <summary>
    /// Compares two source collections using a key selector.
    /// </summary>
    /// <param name="left">Left source collection</param>
    /// <param name="right">Right source collection</param>
    /// <param name="keySelector">Function that extracts key from items of the sources</param>
    /// <typeparam name="TItem">Type of items in the sources</typeparam>
    /// <typeparam name="TKey">Type of the key based on which items from both sources can be compared</typeparam>
    /// <returns>Diff explaining which items are in both, left ony or right only source collections</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static DiffResult<TItem, TItem> Diff<TItem, TKey>(
      this IEnumerable<TItem> left,
      IEnumerable<TItem> right,
      Func<TItem, TKey> keySelector)
    {
      if (left == null) throw new ArgumentNullException(nameof(left));
      if (right == null) throw new ArgumentNullException(nameof(right));
      if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

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
