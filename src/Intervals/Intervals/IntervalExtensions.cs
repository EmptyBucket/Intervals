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

    public static IEnumerable<IInterval<DateTime>> SplitBy(this IInterval<DateTime> interval, TimeSpan timeSpan) =>
        SplitBy(interval, l => new Interval<DateTime>(l, l.Add(timeSpan)));
    
    public static IEnumerable<IInterval<DateTime>> SplitBySeconds(this IInterval<DateTime> interval,
        int secondsCount = 1) =>
        SplitBy(interval,
            l => new SecondInterval(l.Year, l.Month, l.Day, l.Hour, l.Minute, l.Second).AddRight(secondsCount - 1));
    
    public static IEnumerable<IInterval<DateTime>> SplitByMinutes(this IInterval<DateTime> interval,
        int minutesCount = 1) =>
        SplitBy(interval, l => new MinuteInterval(l.Year, l.Month, l.Day, l.Hour, l.Minute).AddRight(minutesCount - 1));
    
    public static IEnumerable<IInterval<DateTime>> SplitByHours(this IInterval<DateTime> interval,
        int hoursCount = 1) =>
        SplitBy(interval, l => new HourInterval(l.Year, l.Month, l.Day, l.Hour).AddRight(hoursCount - 1));

    public static IEnumerable<IInterval<DateTime>> SplitByDays(this IInterval<DateTime> interval,
        int daysCount = 1) =>
        SplitBy(interval, l => new DayInterval(l.Year, l.Month, l.Day).AddRight(daysCount - 1));

    public static IEnumerable<IInterval<DateTime>> SplitByMonths(this IInterval<DateTime> interval,
        int monthsCount = 1) => 
        SplitBy(interval, l => new MonthInterval(l.Year, l.Month).AddRight(monthsCount - 1));

    public static IEnumerable<IInterval<DateTime>> SplitByQuarters(this IInterval<DateTime> interval,
        int quartersCount = 1) =>
        SplitBy(interval, l => new QuarterInterval(l.Year, l.GetQuarter()).AddRight(quartersCount - 1));
    
    public static IEnumerable<IInterval<DateTime>> SplitByHalfYears(this IInterval<DateTime> interval,
        int halfYearsCount = 1) =>
        SplitBy(interval, l => new HalfAYearInterval(l.Year, l.GetHalfAYear()).AddRight(halfYearsCount - 1));
    
    public static IEnumerable<IInterval<DateTime>> SplitByYears(this IInterval<DateTime> interval,
        int yearsCount = 1) =>
        SplitBy(interval, l => new YearInterval(l.Year).AddRight(yearsCount - 1));

    private static IEnumerable<IInterval<DateTime>> SplitBy(IInterval<DateTime> interval,
        Func<DateTime, IInterval<DateTime>> computeInterval)
    {
        var (left, right) = (interval.Left, interval.Right);

        while (left.CompareTo(right) < 0)
        {
            var curInterval = computeInterval(left.Value);
            curInterval = new Interval<DateTime>(
                curInterval.Left.CompareTo(left) > 0 ? curInterval.Left : left,
                curInterval.Right.CompareTo(right) < 0 ? curInterval.Right : right);
            yield return curInterval;
            left = Endpoint.Left(new Point<DateTime>(curInterval.Right.Value, curInterval.Right.Inclusion.Invert()));
        }
    }
}