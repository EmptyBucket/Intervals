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
using Intervals.Utils;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="granuleLength" /> length and
    /// the rest, which was less than <paramref name="granuleLength" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> Split(this Interval<DateTime> interval, TimeSpan granuleLength)
    {
        var (leftValue, _, inclusion) = interval;
        var granularInterval = new TimeGranularInterval(leftValue, granuleLength, 1, inclusion);
        return Split(interval, granularInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="monthsCount" /> length and
    /// the rest, which was less than <paramref name="monthsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="monthsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByMonths(this Interval<DateTime> interval,
        TimeSpan granuleLength, int monthsCount = 1)
    {
        var (leftValue, _, inclusion) = interval;
        var firstDayOfMonth = leftValue.AddDays(1 - leftValue.Day);
        var granularInterval = new MonthGranularInterval(firstDayOfMonth, granuleLength, monthsCount, inclusion);
        return Split(interval, granularInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="quartersCount" /> length and
    /// the rest, which was less than <paramref name="quartersCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="quartersCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByQuarters(this Interval<DateTime> interval,
        TimeSpan granuleLength, int quartersCount = 1)
    {
        var (leftValue, _, inclusion) = interval;
        var firstMonthOfQuarter = leftValue
            .AddMonths(DateTimeHelper.QuarterToMonth(leftValue.GetQuarter()) - leftValue.Month);
        var firstDayOfQuarter = firstMonthOfQuarter.AddDays(1 - firstMonthOfQuarter.Day);
        var granularInterval = new MonthGranularInterval(firstDayOfQuarter, granuleLength,
            DateTimeHelper.MonthsInQuarter * quartersCount, inclusion);
        return Split(interval, granularInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="halfYearsCount" /> length and
    /// the rest, which was less than <paramref name="halfYearsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="halfYearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByHalfYears(this Interval<DateTime> interval,
        TimeSpan granuleLength, int halfYearsCount = 1)
    {
        var (leftValue, _, inclusion) = interval;
        var firstMonthOfHalfYear = leftValue
            .AddMonths(DateTimeHelper.HalfYearToMonth(leftValue.GetHalfYear()) - leftValue.Month);
        var firstDayOfHalfYear = firstMonthOfHalfYear.AddDays(1 - firstMonthOfHalfYear.Day);
        var granularInterval = new MonthGranularInterval(firstDayOfHalfYear, granuleLength,
            DateTimeHelper.MonthsInHalfYear * halfYearsCount, inclusion);
        return Split(interval, granularInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="yearsCount" /> length and
    /// the rest, which was less than <paramref name="yearsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="yearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByYears(this Interval<DateTime> interval,
        TimeSpan granuleLength, int yearsCount = 1)
    {
        var (leftValue, _, inclusion) = interval;
        var firstMonthOfYear = leftValue.AddMonths(1 - leftValue.Month);
        var firstDayOfYear = firstMonthOfYear.AddDays(1 - firstMonthOfYear.Day);
        var granularInterval = new MonthGranularInterval(firstDayOfYear, granuleLength,
            DateTimeHelper.MonthsInYear * yearsCount, inclusion);
        return Split(interval, granularInterval);
    }

    private static IEnumerable<Interval<DateTime>> Split(Interval<DateTime> originInterval,
        GranularInterval<DateTime, TimeSpan> granularInterval)
    {
        while (true)
        {
            var nextInterval = new Interval<DateTime>(GenericMath.Max(granularInterval.Left, originInterval.Left),
                GenericMath.Min(granularInterval.Right, originInterval.Right));
            if (nextInterval.IsEmpty()) break;
            yield return nextInterval;
            granularInterval = granularInterval.MoveByLength();
        }
    }
}