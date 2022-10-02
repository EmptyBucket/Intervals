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
    /// the rest, which was less than <paramref name="granuleSize" /> in length,
    /// if <paramref name="removeIncomplete" /> is false, otherwise removes it
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleSize"></param>
    /// <param name="removeIncomplete"></param>
    /// <returns></returns>
    public static IEnumerable<IInterval<DateTime>> Split(this IInterval<DateTime> interval, TimeSpan granuleSize,
        bool removeIncomplete = false)
    {
        IInterval<DateTime> ComputeInterval(DateTime left)
        {
            var granule = left.Ceiling(granuleSize);

            return !removeIncomplete && left != granule
                ? new Interval<DateTime>(left, granule)
                : new Interval<DateTime>(granule, granule + granuleSize);
        }

        return SplitBy(interval, ComputeInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="monthsCount" /> length and
    /// the rest, which was less than <paramref name="monthsCount" /> in length,
    /// if <paramref name="removeIncomplete" /> is false, otherwise removes it
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="monthsCount"></param>
    /// <param name="removeIncomplete"></param>
    /// <returns></returns>
    public static IEnumerable<IInterval<DateTime>> SplitByMonths(this IInterval<DateTime> interval,
        int monthsCount = 1, bool removeIncomplete = false)
    {
        IInterval<DateTime> ComputeInterval(DateTime left)
        {
            var month = left.CeilingToMonth();

            return !removeIncomplete && left != month
                ? new Interval<DateTime>(left, month)
                : new MonthInterval(month.Year, month.Month).ExpandRight(monthsCount - 1);
        }

        return SplitBy(interval, ComputeInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="quartersCount" /> length and
    /// the rest, which was less than <paramref name="quartersCount" /> in length,
    /// if <paramref name="removeIncomplete" /> is false, otherwise removes it
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="quartersCount"></param>
    /// <param name="removeIncomplete"></param>
    /// <returns></returns>
    public static IEnumerable<IInterval<DateTime>> SplitByQuarters(this IInterval<DateTime> interval,
        int quartersCount = 1, bool removeIncomplete = false)
    {
        IInterval<DateTime> ComputeInterval(DateTime left)
        {
            var quarter = left.CeilingToQuarter();

            return !removeIncomplete && left != quarter
                ? new Interval<DateTime>(left, quarter)
                : new QuarterInterval(quarter.Year, quarter.GetQuarter()).ExpandRight(quartersCount - 1);
        }

        return SplitBy(interval, ComputeInterval);
    }

    /// <summary>
    /// Splits the interval into sub-intervals of <paramref name="halfYearsCount" /> length and
    /// the rest, which was less than <paramref name="halfYearsCount" /> in length,
    /// if <paramref name="removeIncomplete" /> is false, otherwise removes it
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="halfYearsCount"></param>
    /// <param name="removeIncomplete"></param>
    /// <returns></returns>
    public static IEnumerable<IInterval<DateTime>> SplitByHalfYears(this IInterval<DateTime> interval,
        int halfYearsCount = 1, bool removeIncomplete = false)
    {
        IInterval<DateTime> ComputeInterval(DateTime left)
        {
            var halfYear = left.CeilingToHalfYear();

            return !removeIncomplete && left != halfYear
                ? new Interval<DateTime>(left, halfYear)
                : new HalfYearInterval(halfYear.Year, halfYear.GetHalfYear()).ExpandRight(halfYearsCount - 1);
        }

        return SplitBy(interval, ComputeInterval);
    }

    private static IEnumerable<IInterval<DateTime>> SplitBy(IInterval<DateTime> interval,
        Func<DateTime, IInterval<DateTime>> computeInterval)
    {
        var (left, right) = (interval.Left, interval.Right);

        while (left.CompareTo(right) < 0)
        {
            var curInterval = computeInterval(left.Value);
            curInterval = new Interval<DateTime>(curInterval.Left, GenericMath.Min(curInterval.Right, right));
            yield return curInterval;
            left = Endpoint.Left(new Point<DateTime>(curInterval.Right.Value, curInterval.Right.Inclusion.Invert()));
        }
    }
}