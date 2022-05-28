using Intervals.Intervals.Enumerable;
using Intervals.Points;

namespace Intervals.Intervals;

public static class IntervalExtensions
{
    public static bool IsEmpty<T>(this IInterval<T> interval) where T : IEquatable<T>, IComparable<T> =>
        interval.Left.Value.CompareTo(interval.Right.Value) is var compareTo &&
        (interval.Left.Inclusion & interval.Right.Inclusion) == Inclusion.Included
            ? compareTo > 0
            : compareTo >= 0;

    public static bool IsOverlap<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T> => left.Overlap(right).Any();

    public static bool IsInclude<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
        var included = right as IInterval<T>[] ?? right.ToArray();
        return new HashSet<IInterval<T>>(included).SetEquals(left.Overlap(included));
    }

    public static IEnumerable<IInterval<T>> Combine<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        CombineEnumerable<T>.Create(left, right);

    public static IEnumerable<IInterval<T>> Overlap<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        OverlapEnumerable<T>.Create(left, right);

    public static IEnumerable<IInterval<T>> Subtract<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        SubtractEnumerable<T>.Create(left, right);

    public static IEnumerable<IInterval<T>> SymmetricDifference<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        SymmetricDifferenceEnumerable<T>.Create(left, right);
}