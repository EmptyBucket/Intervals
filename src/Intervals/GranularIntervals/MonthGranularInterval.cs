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

using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class MonthGranularInterval : Interval<DateTime>, IGranularInterval<DateTime>
{
    private readonly int _monthsCount;
    
    public MonthGranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint, int monthsCount)
        : base(leftPoint, rightPoint)
    {
        _monthsCount = monthsCount;
    }

    public MonthGranularInterval(DateTime leftValue, DateTime rightValue, int monthsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, inclusion)
    {
        _monthsCount = monthsCount;
    }

    public MonthGranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint)
        : this(leftPoint, rightPoint, ComputeMonthsCount(leftPoint.Value, rightPoint.Value))
    {
    }

    public MonthGranularInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : this(leftValue, rightValue, ComputeMonthsCount(leftValue, rightValue), inclusion)
    {
    }

    public IGranularInterval<DateTime> Move(int granulesCount = 1)
    {
        var totalGranulesCount = _monthsCount * granulesCount;
        return new MonthGranularInterval(
            new Point<DateTime>(Left.Value.AddMonths(totalGranulesCount), Right.Inclusion.Invert()),
            new Point<DateTime>(Right.Value.AddMonths(totalGranulesCount), Left.Inclusion.Invert()));
    }

    public IGranularInterval<DateTime> AddRight(int granulesCount = 1)
    {
        var totalGranulesCount = _monthsCount * granulesCount;
        return new MonthGranularInterval(Left, Right with { Value = Right.Value.AddMonths(totalGranulesCount) });
    }

    public IGranularInterval<DateTime> AddLeft(int granulesCount = 1)
    {
        var totalGranulesCount = _monthsCount * granulesCount;
        return new MonthGranularInterval(Left with { Value = Left.Value.AddMonths(-totalGranulesCount) }, Right);
    }

    private static int ComputeMonthsCount(DateTime leftValue, DateTime rightValue) =>
        (rightValue.Year - leftValue.Year) * 12 + (rightValue.Month - leftValue.Month);
}