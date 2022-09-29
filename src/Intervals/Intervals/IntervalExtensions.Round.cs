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

using Intervals.Utils;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
    public static IInterval<DateTime> Round(this IInterval<DateTime> interval, TimeSpan component,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new Interval<DateTime>(leftVal.Round(component, midpointRounding),
            rightVal.Round(component, midpointRounding), interval.Inclusion);
    }

    public static IInterval<DateTime> RoundToMonth(this IInterval<DateTime> interval,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new Interval<DateTime>(leftVal.RoundToMonth(midpointRounding),
            rightVal.RoundToMonth(midpointRounding), interval.Inclusion);
    }

    public static IInterval<DateTime> RoundToQuarter(this IInterval<DateTime> interval,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new Interval<DateTime>(leftVal.RoundToQuarter(midpointRounding),
            rightVal.RoundToQuarter(midpointRounding), interval.Inclusion);
    }

    public static IInterval<DateTime> RoundToHalfYear(this IInterval<DateTime> interval,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (leftVal, rightVal) = (interval.Left.Value, interval.Right.Value);
        return new Interval<DateTime>(leftVal.RoundToHalfYear(midpointRounding),
            rightVal.RoundToHalfYear(midpointRounding), interval.Inclusion);
    }
}