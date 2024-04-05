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
    /// Splits the interval into sub-intervals of <paramref name="granuleLength" /> and
    /// the rest, which was less than <paramref name="granuleLength" />
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> Split(this Interval<DateTime> interval, TimeSpan granuleLength)
    {
        var (leftValue, _, inclusion) = interval;
        return Split(interval, new TimeGranularInterval(leftValue, granuleLength, inclusion));
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="monthsCount" /> length and
    /// the rest, which was less than <paramref name="monthsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="monthsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByMonths(this Interval<DateTime> interval, int monthsCount = 1)
    {
        var (leftValue, _, inclusion) = interval;
        return Split(interval, new MonthGranularInterval(leftValue, monthsCount, inclusion));
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="quartersCount" /> length and
    /// the rest, which was less than <paramref name="quartersCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="quartersCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByQuarters(this Interval<DateTime> interval,
        int quartersCount = 1) => interval.SplitByMonths(quartersCount * DateTimeHelper.MonthsInQuarter);

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="halfYearsCount" /> length and
    /// the rest, which was less than <paramref name="halfYearsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="halfYearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByHalfYears(this Interval<DateTime> interval,
        int halfYearsCount = 1) => interval.SplitByMonths(halfYearsCount * DateTimeHelper.MonthsInHalfYear);

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="yearsCount" /> length and
    /// the rest, which was less than <paramref name="yearsCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="yearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByYears(this Interval<DateTime> interval,
        int yearsCount = 1) => interval.SplitByMonths(yearsCount * DateTimeHelper.MonthsInYear);

    private static IEnumerable<Interval<DateTime>> Split(Interval<DateTime> originInterval,
        GranularInterval<DateTime, TimeSpan> granularInterval)
    {
        while (true)
        {
            var nextInterval = new Interval<DateTime>(granularInterval.Left,
                GenericMath.Min(granularInterval.Right, originInterval.Right));
            if (nextInterval.IsEmpty()) break;
            yield return nextInterval;
            granularInterval = granularInterval.MoveByGranule();
        }
    }
}