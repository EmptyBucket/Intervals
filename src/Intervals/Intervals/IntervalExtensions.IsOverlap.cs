using Intervals.Intervals.Enumerable;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
    /// <summary>
    /// Returns true if the specified <paramref name="left" /> and <paramref name="right" /> intervals intersect, otherwise returns false
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsOverlap<T>(this IEnumerable<Interval<T>> left, IEnumerable<Interval<T>> right)
        where T : IComparable<T>, IEquatable<T> =>
        left.Overlap(right).Any();

    /// <summary>
    /// Returns true if the specified <paramref name="left" /> and <paramref name="right" /> intervals intersect, otherwise returns false
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsOverlap<T>(this IEnumerable<Interval<T>> left, Interval<T> right)
        where T : IComparable<T>, IEquatable<T> =>
        left.Overlap(new IntervalEnumerable<T>(right)).Any();

    /// <summary>
    /// Returns true if the specified <paramref name="left" /> and <paramref name="right" /> intervals intersect, otherwise returns false
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsOverlap<T>(this Interval<T> left, IEnumerable<Interval<T>> right)
        where T : IComparable<T>, IEquatable<T> =>
        new IntervalEnumerable<T>(left).Overlap(right).Any();

    /// <summary>
    /// Returns true if the specified <paramref name="left" /> and <paramref name="right" /> intervals intersect, otherwise returns false
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsOverlap<T>(this Interval<T> left, Interval<T> right)
        where T : IComparable<T>, IEquatable<T> =>
        new IntervalEnumerable<T>(left).Overlap(new IntervalEnumerable<T>(right)).Any();
}