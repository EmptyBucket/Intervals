using Intervals.Intervals.Enumerable;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
    /// <summary>
    /// Returns the intersection of the specified <paramref name="enumerable" /> intervals
    /// </summary>
    /// <param name="enumerable"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> Overlap<T>(this IEnumerable<Interval<T>> enumerable)
        where T : IComparable<T>, IEquatable<T> => OverlapEnumerable<T>.Create(enumerable);

    /// <summary>
    /// Returns the intersection of the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> Overlap<T>(this IEnumerable<Interval<T>> left,
        IEnumerable<Interval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        OverlapWithEnumerable<T>.Create(left, right);

    /// <summary>
    /// Returns the intersection of the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> Overlap<T>(this IEnumerable<Interval<T>> left, Interval<T> right)
        where T : IComparable<T>, IEquatable<T> =>
        OverlapWithEnumerable<T>.Create(left, new IntervalEnumerable<T>(right));

    /// <summary>
    /// Returns the intersection of the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> Overlap<T>(this Interval<T> left, IEnumerable<Interval<T>> right)
        where T : IComparable<T>, IEquatable<T> =>
        OverlapWithEnumerable<T>.Create(new IntervalEnumerable<T>(left), right);

    /// <summary>
    /// Returns the intersection of the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> Overlap<T>(this Interval<T> left, Interval<T> right)
        where T : IComparable<T>, IEquatable<T> =>
        OverlapWithEnumerable<T>.Create(new IntervalEnumerable<T>(left), new IntervalEnumerable<T>(right));
}