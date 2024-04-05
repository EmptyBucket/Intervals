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
/// Represents an granular interval instance
/// </summary>
public abstract record class GranularInterval<T, TLength> : Interval<T> where T : IComparable<T>, IEquatable<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.GranularInterval"/>
    /// with specified <paramref name="leftPoint" /> and <paramref name="rightPoint" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    protected GranularInterval(Point<T> leftPoint, Point<T> rightPoint) : base(leftPoint, rightPoint)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.GranularInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="inclusion"></param>
    protected GranularInterval(T leftValue, T rightValue, IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, inclusion)
    {
    }

    /// <summary>
    /// Length of the interval
    /// </summary>
    /// <returns></returns>
    public abstract TLength Length { get; }

    /// <summary>
    /// Move the left point of the interval by the number of granules multiplied
    /// by the specified <paramref name="leftMultiplier" /> and
    /// the right point of the interval by the number of granules multiplied
    /// by the specified <paramref name="rightMultiplier" />
    /// </summary>
    /// <param name="leftMultiplier"></param>
    /// <param name="rightMultiplier"></param>
    /// <returns></returns>
    public abstract GranularInterval<T, TLength> MoveByGranule(int leftMultiplier, int rightMultiplier);

    /// <summary>
    /// Move the left point and right point of the interval by the number of granules
    /// multiplied by the specified <paramref name="multiplier" />
    /// </summary>
    /// <param name="multiplier"></param>
    /// <returns></returns>
    public GranularInterval<T, TLength> MoveByGranule(int multiplier = 1) => MoveByGranule(multiplier, multiplier);

    /// <summary>
    /// Move the left point of the interval by the length of the interval multiplied
    /// by the specified <paramref name="leftMultiplier" /> and
    /// the right point of the interval by the length of the interval multiplied
    /// by the specified <paramref name="rightMultiplier" />
    /// </summary>
    /// <param name="leftMultiplier"></param>
    /// <param name="rightMultiplier"></param>
    /// <returns></returns>
    public abstract GranularInterval<T, TLength> MoveByLength(int leftMultiplier, int rightMultiplier);

    /// <summary>
    /// Move the left point and right point of the interval by the length of the interval multiplied
    /// by the specified <paramref name="multiplier" />
    /// </summary>
    /// <param name="multiplier"></param>
    /// <returns></returns>
    public GranularInterval<T, TLength> MoveByLength(int multiplier = 1) => MoveByLength(multiplier, multiplier);

    /// <summary>
    /// Convert to granular interval with specified <paramref name="inclusion" />
    /// </summary>
    /// <param name="inclusion"></param>
    /// <returns></returns>
    public abstract GranularInterval<T, TLength> Convert(IntervalInclusion inclusion);
}