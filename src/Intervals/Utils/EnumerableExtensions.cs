namespace Intervals.Utils;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> MergeAscending<T>(this IEnumerable<T> first, IEnumerable<T> second)
        where T : IComparable<T>
    {
        using var firstEnumerator = first.GetEnumerator();
        using var secondEnumerator = second.GetEnumerator();
        bool firstHas = firstEnumerator.MoveNext(), secondHas = secondEnumerator.MoveNext();

        if (firstHas && secondHas)
        {
            IEnumerator<T> currentEnumerator;

            do
            {
                currentEnumerator = firstEnumerator.Current.CompareTo(secondEnumerator.Current) <= 0
                    ? firstEnumerator
                    : secondEnumerator;
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