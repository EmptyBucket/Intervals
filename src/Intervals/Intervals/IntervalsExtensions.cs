namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static bool IsOverlap<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T> => left.Overlap(right).Any();

    public static bool IsInclude<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
        var included = right.ToArray();
        var enumerable = left.Overlap(included).ToArray();
        return new HashSet<IInterval<T>>(included).SetEquals(enumerable);
    }

    private static int ToBalance<T>(this IEndpoint<T> endpoint) where T : IComparable<T>, IEquatable<T> =>
        (int)endpoint.Location * 2 - 1;

    private static IPoint<T> WithInvertedInclusion<T>(this IPoint<T> point) where T : IComparable<T>, IEquatable<T> =>
        new Point<T>(point.Value, point.Inclusion.Invert());

    private static IEnumerable<IEndpoint<T>> GetEndpoints<T>(this IInterval<T> interval)
        where T : IComparable<T>, IEquatable<T>
    {
        yield return interval.Left;
        yield return interval.Right;
    }
}