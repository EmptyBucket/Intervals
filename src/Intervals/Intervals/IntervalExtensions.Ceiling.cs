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
    /// Returns the smallest interval aligned to <paramref name="granuleSize" /> that is greater than or equal to the specified <paramref name="interval" />
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="granuleSize"></param>
    /// <returns></returns>
    public static IGranularInterval<DateTime> Ceiling(this IInterval<DateTime> interval, TimeSpan granuleSize)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new TimeGranularInterval(leftVal.Floor(granuleSize), rightVal.Ceiling(granuleSize), interval.Inclusion);
    }

    /// <summary>
    /// Returns the smallest interval aligned to month that is greater than or equal to the specified <paramref name="interval" />
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    public static IGranularInterval<DateTime> CeilingToMonth(this IInterval<DateTime> interval)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new MonthGranularInterval(leftVal.FloorToMonth(), rightVal.CeilingToMonth(), interval.Inclusion);
    }

    /// <summary>
    /// Returns the smallest interval aligned to quarter that is greater than or equal to the specified <paramref name="interval" />
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    public static IGranularInterval<DateTime> CeilingToQuarter(this IInterval<DateTime> interval)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new MonthGranularInterval(leftVal.FloorToQuarter(), rightVal.CeilingToQuarter(), interval.Inclusion);
    }

    /// <summary>
    /// Returns the smallest interval aligned to half-year that is greater than or equal to the specified <paramref name="interval" />
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    public static IGranularInterval<DateTime> CeilingToHalfYear(this IInterval<DateTime> interval)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new MonthGranularInterval(leftVal.FloorToHalfYear(), rightVal.CeilingToHalfYear(), interval.Inclusion);
    }
}