using Intervals.Intervals.Enumerable;

namespace Intervals.Intervals;

public static class IntervalsExtensions
{
    public static bool IsOverlap<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T> => left.Overlap(right).Any();

    public static bool IsInclude<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
        var included = right.ToArray();
        return new HashSet<IInterval<T>>(included).SetEquals(left.Overlap(included).ToArray());
    }

    public static IEnumerable<IInterval<T>> Combine<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        new CombineEnumerable<T>(left, right);

    public static IEnumerable<IInterval<T>> Overlap<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        new OverlapEnumerable<T>(left, right);

    public static IEnumerable<IInterval<T>> Subtract<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        new SubtractEnumerable<T>(left, right);

    public static IEnumerable<IInterval<T>> SymmetricDifference<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
        var enumerable = left.Combine(right).ToArray();
        var overlap = left.Overlap(right).ToArray();
        var symmetricDifference = enumerable.Subtract(overlap).ToArray();
        return symmetricDifference;
    }
}