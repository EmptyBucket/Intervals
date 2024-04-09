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
    /// Splits the interval into sub-intervals of <paramref name="length" /> and
    /// the rest, which was less than <paramref name="length" />
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> Split(this Interval<DateTime> interval, TimeSpan length)
    {
        var (leftValue, _, inclusion) = interval;
        GranularInterval<DateTime, TimeSpan> granularInterval = new TickInterval(leftValue, length.Ticks, inclusion);
        var leftInclusion = granularInterval.Left.Inclusion;
        while (true)
        {
            var nextInterval = new Interval<DateTime>(
                GenericMath.Max(granularInterval.Left, interval.Left) with { Inclusion = leftInclusion },
                GenericMath.Min(granularInterval.Right, interval.Right));
            if (nextInterval.IsEmpty()) break;
            yield return nextInterval;
            granularInterval = granularInterval.MoveByLength();
            leftInclusion = nextInterval.Right.Inclusion.Invert();
        }
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="granuleLength" /> * <paramref name="granulesCount" /> length and
    /// the rest, which was less than <paramref name="granuleLength" /> * <paramref name="granulesCount" /> in length
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="granulesCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> Split(this Interval<DateTime> interval, TimeSpan granuleLength,
        long granulesCount)
    {
        var (leftValue, _, inclusion) = interval;
        var granularInterval = new TimeGranularInterval(leftValue, granuleLength, granulesCount, inclusion);
        return GranularSplit(interval, granularInterval);
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
        TimeSpan granuleLength, int monthsCount)
    {
        var (leftValue, _, inclusion) = interval;
        var granularInterval = new MonthlyInterval(leftValue.Year, leftValue.Month, granuleLength, monthsCount,
            inclusion, leftValue.Kind);
        return GranularSplit(interval, granularInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="quartersCount" /> length and
    /// the rest, which was less than <paramref name="quartersCount" /> in length
    /// aligned to <paramref name="granuleLength" />
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="quartersCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByQuarters(this Interval<DateTime> interval,
        TimeSpan granuleLength, int quartersCount)
    {
        var (leftValue, _, inclusion) = interval;
        var granularInterval = new QuarterlyInterval(leftValue.Year, leftValue.GetQuarter(), granuleLength,
            quartersCount, inclusion, leftValue.Kind);
        return GranularSplit(interval, granularInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="halfYearsCount" /> length and
    /// the rest, which was less than <paramref name="halfYearsCount" /> in length
    /// aligned to <paramref name="granuleLength" />
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="halfYearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByHalfYears(this Interval<DateTime> interval,
        TimeSpan granuleLength, int halfYearsCount)
    {
        var (leftValue, _, inclusion) = interval;
        var granularInterval = new HalfYearlyInterval(leftValue.Year, leftValue.GetHalfYear(), granuleLength,
            halfYearsCount, inclusion, leftValue.Kind);
        return GranularSplit(interval, granularInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="yearsCount" /> length and
    /// the rest, which was less than <paramref name="yearsCount" /> in length
    /// aligned to <paramref name="granuleLength" />
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleLength"></param>
    /// <param name="yearsCount"></param>
    /// <returns></returns>
    public static IEnumerable<Interval<DateTime>> SplitByYears(this Interval<DateTime> interval,
        TimeSpan granuleLength, int yearsCount)
    {
        var (leftValue, _, inclusion) = interval;
        var granularInterval = new YearlyInterval(leftValue.Year, granuleLength, yearsCount, inclusion, leftValue.Kind);
        return GranularSplit(interval, granularInterval);
    }

    private static IEnumerable<Interval<DateTime>> GranularSplit(Interval<DateTime> originInterval,
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