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

/// <summary>
/// Represents an granular interval instance where the granule size is determined by the number of months in the interval
/// </summary>
public class MonthGranularInterval : Interval<DateTime>, IGranularInterval<DateTime>
{
    protected int GranulesCount;

    public MonthGranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        GranulesCount = ComputeGranulesCount(leftPoint.Value, rightPoint.Value);
    }

    public MonthGranularInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened) : base(leftValue, rightValue, inclusion)
    {
        GranulesCount = ComputeGranulesCount(leftValue, rightValue);
    }

    /// <summary>
    /// Returns a new interval moved by the specified <paramref name="granulesCount" />.
    /// If the <paramref name="granulesCount" /> is positive, then move to the right, if negative, then move to the left
    /// </summary>
    /// <param name="granulesCount"></param>
    /// <returns></returns>
    public IGranularInterval<DateTime> Move(int granulesCount = 1)
    {
        var totalGranulesCount = GranulesCount * granulesCount;
        return new MonthGranularInterval(
            Left with { Value = Left.Value.AddMonths(totalGranulesCount) },
            Right with { Value = Right.Value.AddMonths(totalGranulesCount) });
    }

    /// <summary>
    /// Returns a new interval expanded by the specified <paramref name="granulesCount" /> to the right
    /// </summary>
    /// <param name="granulesCount"></param>
    /// <returns></returns>
    public IGranularInterval<DateTime> ExpandLeft(int granulesCount = 1)
    {
        var totalGranulesCount = GranulesCount * granulesCount;
        return new MonthGranularInterval(Left with { Value = Left.Value.AddMonths(-totalGranulesCount) }, Right);
    }

    /// <summary>
    /// Returns a new interval expanded by the specified <paramref name="granulesCount" /> to the left
    /// </summary>
    /// <param name="granulesCount"></param>
    /// <returns></returns>
    public IGranularInterval<DateTime> ExpandRight(int granulesCount = 1)
    {
        var totalGranulesCount = GranulesCount * granulesCount;
        return new MonthGranularInterval(Left, Right with { Value = Right.Value.AddMonths(totalGranulesCount) });
    }

    private static int ComputeGranulesCount(DateTime leftValue, DateTime rightValue) =>
        (rightValue.Year - leftValue.Year) * 12 + (rightValue.Month - leftValue.Month);
}