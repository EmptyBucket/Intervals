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
    /// Rounds a <paramref name="interval" /> to a interval aligned to <paramref name="granuleSize" />, and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleSize"></param>
    /// <param name="midpointRounding"></param>
    /// <returns></returns>
    public static GranularInterval<DateTime> Round(this Interval<DateTime> interval, TimeSpan granuleSize,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal, _) = interval;
        return new TimeGranularInterval(leftVal.Round(granuleSize, midpointRounding),
            rightVal.Round(granuleSize, midpointRounding), interval.Inclusion);
    }

    /// <summary>
    /// Rounds a <paramref name="interval" /> to a interval aligned to month, and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="midpointRounding"></param>
    /// <returns></returns>
    public static GranularInterval<DateTime> RoundToMonth(this Interval<DateTime> interval,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal, _) = interval;
        return new MonthGranularInterval(leftVal.RoundToMonth(midpointRounding),
            rightVal.RoundToMonth(midpointRounding), interval.Inclusion);
    }

    /// <summary>
    /// Rounds a <paramref name="interval" /> to a interval aligned to quarter, and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="midpointRounding"></param>
    /// <returns></returns>
    public static GranularInterval<DateTime> RoundToQuarter(this Interval<DateTime> interval,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal, _) = interval;
        return new MonthGranularInterval(leftVal.RoundToQuarter(midpointRounding),
            rightVal.RoundToQuarter(midpointRounding), interval.Inclusion);
    }

    /// <summary>
    /// Rounds a <paramref name="interval" /> to a interval aligned to half-year, and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="midpointRounding"></param>
    /// <returns></returns>
    public static GranularInterval<DateTime> RoundToHalfYear(this Interval<DateTime> interval,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal, _) = interval;
        return new MonthGranularInterval(leftVal.RoundToHalfYear(midpointRounding),
            rightVal.RoundToHalfYear(midpointRounding), interval.Inclusion);
    }

    /// <summary>
    /// Rounds a <paramref name="interval" /> to a interval aligned to year, and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="midpointRounding"></param>
    /// <returns></returns>
    public static GranularInterval<DateTime> RoundToYear(this Interval<DateTime> interval,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal, _) = interval;
        return new MonthGranularInterval(leftVal.RoundToYear(midpointRounding),
            rightVal.RoundToYear(midpointRounding), interval.Inclusion);
    }
}