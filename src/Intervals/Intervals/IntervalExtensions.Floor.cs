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
    public static IGranularInterval<DateTime> Floor(this IInterval<DateTime> interval, TimeSpan granuleSize)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new GranularInterval(leftVal.Ceiling(granuleSize), rightVal.Floor(granuleSize), interval.Inclusion);
    }

    public static IGranularInterval<DateTime> FloorToMonth(this IInterval<DateTime> interval)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new MonthGranularInterval(leftVal.CeilingToMonth(), rightVal.FloorToMonth(), interval.Inclusion);
    }

    public static IGranularInterval<DateTime> FloorToQuarter(this IInterval<DateTime> interval)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new MonthGranularInterval(leftVal.CeilingToQuarter(), rightVal.FloorToQuarter(), interval.Inclusion);
    }

    public static IGranularInterval<DateTime> FloorToHalfYear(this IInterval<DateTime> interval)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new MonthGranularInterval(leftVal.CeilingToHalfYear(), rightVal.FloorToHalfYear(), interval.Inclusion);
    }
}