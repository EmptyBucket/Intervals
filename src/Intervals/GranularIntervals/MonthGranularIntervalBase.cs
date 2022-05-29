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

public abstract class MonthGranularIntervalBase<TInterval> : Interval<DateTime>, IGranularInterval<DateTime, TInterval>
    where TInterval : IGranularInterval<DateTime, TInterval>
{
    private readonly int _granulesCount;

    protected MonthGranularIntervalBase(Point<DateTime> leftPoint, Point<DateTime> rightPoint)
        : base(leftPoint, rightPoint)
    {
        _granulesCount = GranulesCount(leftPoint.Value, rightPoint.Value);
    }

    protected MonthGranularIntervalBase(DateTime leftValue, DateTime rightValue, IntervalInclusion inclusion)
        : base(leftValue, rightValue, inclusion)
    {
        _granulesCount = GranulesCount(leftValue, rightValue);
    }

    private static int GranulesCount(DateTime leftValue, DateTime rightValue) =>
        (rightValue.Year - leftValue.Year) * 12 + (rightValue.Month - leftValue.Month);

    public TInterval Move(int granulesCount = 1)
    {
        var totalGranulesCount = _granulesCount * granulesCount;
        return Create(
            new Point<DateTime>(Left.Value.AddMonths(totalGranulesCount), Right.Inclusion.Invert()),
            new Point<DateTime>(Right.Value.AddMonths(totalGranulesCount), Left.Inclusion.Invert()));
    }

    public TInterval Add(int granulesCount = 1)
    {
        var totalGranulesCount = _granulesCount * granulesCount;
        return Create(Left, new Point<DateTime>(Right.Value.AddMonths(totalGranulesCount), Left.Inclusion.Invert()));
    }

    protected abstract TInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint);
}