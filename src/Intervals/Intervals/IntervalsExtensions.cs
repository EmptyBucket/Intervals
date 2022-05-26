using Intervals.Points;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static bool IsOverlap<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T> => left.Overlap(right).Any();

    public static bool IsInclude<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
        var included = right.ToArray();
        return new HashSet<IInterval<T>>(included).SetEquals(left.Overlap(included).ToArray());
    }

    private static bool HasGap<T>(Point<T> first, Point<T> second) where T : IEquatable<T> =>
        !first.Value.Equals(second.Value) || (first.Inclusion | second.Inclusion) != Inclusion.Included;

    private static int GetBalance<T>(this Endpoint<T> endpoint) where T : IComparable<T>, IEquatable<T> =>
        (int)endpoint.Location * 2 - 1;

    private static IEnumerable<Endpoint<T>> GetEndpoints<T>(this IInterval<T> interval)
        where T : IComparable<T>, IEquatable<T>
    {
        yield return interval.Left;
        yield return interval.Right;
    }

    private interface IOrderedEnumerable<out T> : IEnumerable<T>
    {
    }
}