using Intervals.Intervals.Enumerable;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
    /// <summary>
    /// Returns true if the specified <paramref name="outer" /> intervals include <paramref name="inner" /> intervals, otherwise returns false
    /// </summary>
    /// <param name="outer"></param>
    /// <param name="inner"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsInclude<T>(this IEnumerable<Interval<T>> outer, IEnumerable<Interval<T>> inner)
        where T : IComparable<T>, IEquatable<T>
    {
        var innerArray = inner as Interval<T>[] ?? inner.ToArray();
        return new HashSet<Interval<T>>(innerArray).SetEquals(outer.Overlap(innerArray));
    }

    /// <summary>
    /// Returns true if the specified <paramref name="outer" /> intervals include <paramref name="inner" /> intervals, otherwise returns false
    /// </summary>
    /// <param name="outer"></param>
    /// <param name="inner"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsInclude<T>(this IEnumerable<Interval<T>> outer, Interval<T> inner)
        where T : IComparable<T>, IEquatable<T>
    {
        var innerArray = new IntervalEnumerable<T>(inner);
        return new HashSet<Interval<T>>(innerArray).SetEquals(outer.Overlap(innerArray));
    }

    /// <summary>
    /// Returns true if the specified <paramref name="outer" /> intervals include <paramref name="inner" /> intervals, otherwise returns false
    /// </summary>
    /// <param name="outer"></param>
    /// <param name="inner"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsInclude<T>(this Interval<T> outer, IEnumerable<Interval<T>> inner)
        where T : IComparable<T>, IEquatable<T>
    {
        var innerArray = inner as Interval<T>[] ?? inner.ToArray();
        return new HashSet<Interval<T>>(innerArray).SetEquals(new IntervalEnumerable<T>(outer).Overlap(innerArray));
    }

    /// <summary>
    /// Returns true if the specified <paramref name="outer" /> intervals include <paramref name="inner" /> intervals, otherwise returns false
    /// </summary>
    /// <param name="outer"></param>
    /// <param name="inner"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsInclude<T>(this Interval<T> outer, Interval<T> inner)
        where T : IComparable<T>, IEquatable<T>
    {
        var innerArray = new IntervalEnumerable<T>(inner);
        return new HashSet<Interval<T>>(innerArray).SetEquals(new IntervalEnumerable<T>(outer).Overlap(innerArray));
    }
}