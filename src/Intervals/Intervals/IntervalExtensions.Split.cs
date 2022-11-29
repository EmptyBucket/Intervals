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
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="granuleSize" /> length and
    /// the rest, which was less than <paramref name="granuleSize" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleSize"></param>
    /// <returns></returns>
    public static IEnumerable<GranularInterval<DateTime>> Split(this Interval<DateTime> interval,
        TimeSpan granuleSize)
    {
        var (left, right) = interval;

        while (left.CompareTo(right) <= 0)
        {
            var curInterval = new TimeGranularInterval(left.Value, left.Value + granuleSize);
            curInterval = new TimeGranularInterval(
                GenericMath.Max(curInterval.Left, interval.Left), GenericMath.Min(curInterval.Right, interval.Right));
            yield return curInterval;
            left = Endpoint.Left(curInterval.Right.Value, curInterval.Right.Inclusion.Invert());
        }
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="monthsCount" /> length and
    /// the rest, which was less than <paramref name="monthsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="monthsCount"></param>
    /// <returns></returns>
    public static IEnumerable<GranularInterval<DateTime>> SplitByMonths(this Interval<DateTime> interval,
        int monthsCount = 1) =>
        SplitByMonths(interval, l => new MonthInterval(l.Year, l.Month).ExpandRight(monthsCount - 1));

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="quartersCount" /> length and
    /// the rest, which was less than <paramref name="quartersCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="quartersCount"></param>
    /// <returns></returns>
    public static IEnumerable<GranularInterval<DateTime>> SplitByQuarters(this Interval<DateTime> interval,
        int quartersCount = 1) =>
        SplitByMonths(interval, l => new QuarterInterval(l.Year, l.GetQuarter()).ExpandRight(quartersCount - 1));

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="halfYearsCount" /> length and
    /// the rest, which was less than <paramref name="halfYearsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="halfYearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<GranularInterval<DateTime>> SplitByHalfYears(this Interval<DateTime> interval,
        int halfYearsCount = 1) =>
        SplitByMonths(interval, l => new HalfYearInterval(l.Year, l.GetHalfYear()).ExpandRight(halfYearsCount - 1));

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="yearsCount" /> length and
    /// the rest, which was less than <paramref name="yearsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="yearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<GranularInterval<DateTime>> SplitByYears(this Interval<DateTime> interval,
        int yearsCount = 1) =>
        SplitByMonths(interval, l => new YearInterval(l.Year).ExpandRight(yearsCount - 1));


    private static IEnumerable<GranularInterval<DateTime>> SplitByMonths(Interval<DateTime> interval,
        ComputeNext computeNext)
    {
        var (left, right) = interval;

        while (left.CompareTo(right) <= 0)
        {
            var curInterval = computeNext(left.Value);
            curInterval = new MonthGranularInterval(
                GenericMath.Max(curInterval.Left, interval.Left), GenericMath.Min(curInterval.Right, interval.Right));
            yield return curInterval;
            left = Endpoint.Left(curInterval.Right.Value, curInterval.Right.Inclusion.Invert());
        }
    }

    private delegate GranularInterval<DateTime> ComputeNext(DateTime left);
}