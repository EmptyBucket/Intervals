namespace Intervals.Utils;

internal static class EnumerableExtensions
{
    public static IEnumerable<TSource> Merge<TSource, TKey>(this IEnumerable<TSource> first,
        IEnumerable<TSource> second, Func<TSource, TKey> selector, bool ascending = true)
        where TKey : IComparable<TKey>
    {
        Func<int, bool> order = ascending ? i => i <= 0 : i => i >= 0;
        using var firstEnumerator = first.GetEnumerator();
        using var secondEnumerator = second.GetEnumerator();
        bool firstHas = firstEnumerator.MoveNext(), secondHas = secondEnumerator.MoveNext();

        if (firstHas && secondHas)
        {
            IEnumerator<TSource> currentEnumerator;

            do
            {
                var firstKey = selector(firstEnumerator.Current);
                var secondKey = selector(secondEnumerator.Current);
                currentEnumerator = order(firstKey.CompareTo(secondKey)) ? firstEnumerator : secondEnumerator;
                yield return currentEnumerator.Current;
            } while (currentEnumerator.MoveNext());

            currentEnumerator = currentEnumerator == firstEnumerator ? secondEnumerator : firstEnumerator;

            do yield return currentEnumerator.Current;
            while (currentEnumerator.MoveNext());
        }
        else if (firstHas)
        {
            do yield return firstEnumerator.Current;
            while (firstEnumerator.MoveNext());
        }
        else if (secondHas)
        {
            do yield return secondEnumerator.Current;
            while (secondEnumerator.MoveNext());
        }
    }
}