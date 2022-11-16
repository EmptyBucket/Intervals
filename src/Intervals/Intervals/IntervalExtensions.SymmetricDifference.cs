using Intervals.Intervals.Enumerable;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
    /// <summary>
    /// Returns the symmetric difference between the specified <paramref name="enumerable" /> intervals
    /// </summary>
    /// <param name="enumerable"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> SymmetricDifference<T>(this IEnumerable<Interval<T>> enumerable)
        where T : IComparable<T>, IEquatable<T> => SymmetricDifferenceEnumerable<T>.Create(enumerable);

    /// <summary>
    /// Returns the symmetric difference between the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> SymmetricDifference<T>(this IEnumerable<Interval<T>> left,
        IEnumerable<Interval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        SymmetricDifferenceWithEnumerable<T>.Create(left, right);

    /// <summary>
    /// Returns the symmetric difference between the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> SymmetricDifference<T>(this IEnumerable<Interval<T>> left, Interval<T> right)
        where T : IComparable<T>, IEquatable<T> =>
        SymmetricDifferenceWithEnumerable<T>.Create(left, new IntervalEnumerable<T>(right));

    /// <summary>
    /// Returns the symmetric difference between the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> SymmetricDifference<T>(this Interval<T> left, IEnumerable<Interval<T>> right)
        where T : IComparable<T>, IEquatable<T> =>
        SymmetricDifferenceWithEnumerable<T>.Create(new IntervalEnumerable<T>(left), right);

    /// <summary>
    /// Returns the symmetric difference between the specified <paramref name="left" /> and <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Interval<T>> SymmetricDifference<T>(this Interval<T> left, Interval<T> right)
        where T : IComparable<T>, IEquatable<T> =>
        SymmetricDifferenceWithEnumerable<T>.Create(new IntervalEnumerable<T>(left), new IntervalEnumerable<T>(right));
}