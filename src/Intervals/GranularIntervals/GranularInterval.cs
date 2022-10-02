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
/// Represents an granular interval instance where the granule size is determined by the length of the interval
/// </summary>
public class GranularInterval : Interval<DateTime>, IGranularInterval<DateTime>
{
    protected TimeSpan GranuleSize;

    public GranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        GranuleSize = ComputeGranuleSize(leftPoint.Value, rightPoint.Value);
    }

    public GranularInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened) : base(leftValue, rightValue, inclusion)
    {
        GranuleSize = ComputeGranuleSize(leftValue, rightValue);
    }

    /// <summary>
    /// Returns a new interval moved by the specified <paramref name="granulesCount" />.
    /// If the <paramref name="granulesCount" /> is positive, then move to the right, if negative, then move to the left
    /// </summary>
    /// <param name="granulesCount"></param>
    /// <returns></returns>
    public IGranularInterval<DateTime> Move(int granulesCount = 1)
    {
        var totalGranulesSize = GranuleSize * granulesCount;
        return new GranularInterval(
            Left with { Value = Left.Value + totalGranulesSize },
            Right with { Value = Right.Value + totalGranulesSize });
    }

    /// <summary>
    /// Returns a new interval expanded by the specified <paramref name="granulesCount" /> to the right
    /// </summary>
    /// <param name="granulesCount"></param>
    /// <returns></returns>
    public IGranularInterval<DateTime> ExpandLeft(int granulesCount = 1)
    {
        var totalGranulesSize = GranuleSize * granulesCount;
        return new GranularInterval(Left with { Value = Left.Value - totalGranulesSize }, Right);
    }

    /// <summary>
    /// Returns a new interval expanded by the specified <paramref name="granulesCount" /> to the left
    /// </summary>
    /// <param name="granulesCount"></param>
    /// <returns></returns>
    public IGranularInterval<DateTime> ExpandRight(int granulesCount = 1)
    {
        var totalGranulesSize = GranuleSize * granulesCount;
        return new GranularInterval(Left, Right with { Value = Right.Value + totalGranulesSize });
    }

    private static TimeSpan ComputeGranuleSize(DateTime leftValue, DateTime rightValue) => rightValue - leftValue;
}