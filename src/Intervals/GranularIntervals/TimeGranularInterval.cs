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

using System.Text.Json.Serialization;
using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an time granular interval instance
/// </summary>
[Serializable]
public record class TimeGranularInterval : GranularInterval<DateTime, TimeSpan>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.TimeGranularInterval"/>
    /// with specified <paramref name="leftPoint" />, <paramref name="rightPoint" /> and
    /// <paramref name="granuleLength" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    /// <param name="granuleLength"></param>
    public TimeGranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint, TimeSpan granuleLength)
        : base(leftPoint, rightPoint)
    {
        ThrowIfNotValid(leftPoint.Value, rightPoint.Value, granuleLength);

        GranuleLength = granuleLength;
        Length = GetLength(leftPoint.Value, rightPoint.Value, granuleLength, Inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.TimeGranularInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" />,
    /// <paramref name="granuleLength" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public TimeGranularInterval(DateTime leftValue, DateTime rightValue, TimeSpan granuleLength,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, inclusion)
    {
        ThrowIfNotValid(leftValue, rightValue, granuleLength);

        GranuleLength = granuleLength;
        Length = GetLength(leftValue, rightValue, granuleLength, inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.TimeGranularInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granuleLength" />,
    /// <paramref name="granulesCount" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="granulesCount"></param>
    /// <param name="inclusion"></param>
    public TimeGranularInterval(DateTime leftValue, TimeSpan granuleLength, int granulesCount = 1,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : this(leftValue, GetRight(leftValue, granuleLength, granulesCount, inclusion), granuleLength, inclusion)
    {
    }

    /// <summary>
    /// Length of the granule
    /// </summary>
    public TimeSpan GranuleLength { get; }

    /// <inheritdoc />
    public override TimeSpan Length { get; }

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> MoveByGranule(int leftMultiplier, int rightMultiplier) =>
        this with
        {
            LeftValue = LeftValue + GranuleLength * leftMultiplier,
            RightValue = RightValue + GranuleLength * rightMultiplier
        };

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> MoveByLength(int leftMultiplier, int rightMultiplier) =>
        this with
        {
            LeftValue = LeftValue + Length * leftMultiplier, RightValue = RightValue + Length * rightMultiplier
        };

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> Convert(IntervalInclusion inclusion)
    {
        var (leftInclusion, rightInclusion) = IntervalInclusionConverter.ToInclusions(inclusion);
        var leftAddition = Left.Inclusion != leftInclusion
            ? BitHelper.ToSign(leftInclusion == Points.Inclusion.Excluded) * GranuleLength
            : TimeSpan.Zero;
        var rightAddition = Right.Inclusion != rightInclusion
            ? BitHelper.ToSign(rightInclusion == Points.Inclusion.Included) * GranuleLength
            : TimeSpan.Zero;
        return this with
        {
            LeftValue = LeftValue + leftAddition, RightValue = RightValue + rightAddition, Inclusion = inclusion
        };
    }

    private static TimeSpan GetLength(DateTime leftValue, DateTime rightValue, TimeSpan granuleLength,
        IntervalInclusion inclusion)
    {
        var (leftAddition, rightAddition) = inclusion switch
        {
            IntervalInclusion.Opened => (granuleLength, TimeSpan.Zero),
            IntervalInclusion.Closed => (TimeSpan.Zero, granuleLength),
            _ => (TimeSpan.Zero, TimeSpan.Zero)
        };
        return GenericMath.Max((rightValue + rightAddition) - (leftValue + leftAddition), TimeSpan.Zero);
    }

    private static DateTime GetRight(DateTime leftValue, TimeSpan granuleLength, int granulesCount,
        IntervalInclusion inclusion)
    {
        var addition = inclusion switch
        {
            IntervalInclusion.Opened => granuleLength,
            IntervalInclusion.Closed => -granuleLength,
            _ => TimeSpan.Zero
        };
        return leftValue + granuleLength * granulesCount + addition;
    }

    private static void ThrowIfNotValid(DateTime leftValue, DateTime rightValue, TimeSpan granuleLength)
    {
        if (granuleLength <= TimeSpan.Zero)
            throw new ArgumentException($"The {granuleLength} must not be less or equal zero");

        if (leftValue.Ticks % granuleLength.Ticks != rightValue.Ticks % granuleLength.Ticks)
            throw new ArgumentException(
                $"The {nameof(leftValue)} and {nameof(rightValue)} must be aligned to the {nameof(granuleLength)}");
    }
}