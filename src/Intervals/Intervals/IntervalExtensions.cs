// MIT License
// 
// Copyright (c) 2021 Alexey Politov, Yevgeny Khoroshavin
// https://github.com/EmptyBucket/Intervals
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Intervals.GranularIntervals;
using Intervals.Intervals.Enumerable;
using Intervals.Points;
using Intervals.Utils;

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

    public static IEnumerable<IInterval<DateTime>> SplitByDays(this IInterval<DateTime> interval,
        int daysCount = 1) =>
        SplitBy(interval, l => new DayInterval(l).Add(daysCount - 1));

    public static IEnumerable<IInterval<DateTime>> SplitByMonths(this IInterval<DateTime> interval,
        int monthsCount = 1) => 
        SplitBy(interval, l => new MonthInterval(l.Year, l.Month).Add(monthsCount - 1));

    public static IEnumerable<IInterval<DateTime>> SplitByQuarters(this IInterval<DateTime> interval,
        int quartersCount = 1) =>
        SplitBy(interval, l => new QuarterInterval(l.Year, l.GetQuarter()).Add(quartersCount - 1));
    
    public static IEnumerable<IInterval<DateTime>> SplitByHalfYears(this IInterval<DateTime> interval,
        int halfYearsCount = 1) =>
        SplitBy(interval, l => new HalfAYearInterval(l.Year, l.GetHalfAYear()).Add(halfYearsCount - 1));
    
    public static IEnumerable<IInterval<DateTime>> SplitByYears(this IInterval<DateTime> interval,
        int yearsCount = 1) =>
        SplitBy(interval, l => new YearInterval(l.Year).Add(yearsCount - 1));

    private static IEnumerable<IInterval<DateTime>> SplitBy(IInterval<DateTime> interval,
        Func<DateTime, IInterval<DateTime>> computeInterval)
    {
        var (left, right) = (interval.Left, interval.Right);

        while (left.CompareTo(right) < 0)
        {
            var curInterval = computeInterval(left.Value);
            curInterval =
                new Interval<DateTime>(left, curInterval.Right.CompareTo(right) < 0 ? curInterval.Right : right);
            yield return curInterval;
            left = Endpoint.Left(new Point<DateTime>(curInterval.Right.Value, curInterval.Right.Inclusion.Invert()));
        }
    }
}