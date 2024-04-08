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
/// Represents an month granular interval instance
/// </summary>
[Serializable]
public record class MonthGranularInterval : GranularInterval<DateTime, TimeSpan>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthGranularInterval"/>
    /// with specified <paramref name="leftPoint" />, <paramref name="rightPoint" /> and
    /// <paramref name="granuleLength" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    /// <param name="granuleLength"></param>
    public MonthGranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint, TimeSpan granuleLength)
        : base(leftPoint, rightPoint)
    {
        MonthsCount = GetMonthsCount(leftPoint.Value, rightPoint.Value, Inclusion);

        ThrowIfNotValid(leftPoint.Value, rightPoint.Value, granuleLength, MonthsCount, Inclusion);

        GranuleLength = granuleLength;
        Length = GetLength(leftPoint.Value, rightPoint.Value, granuleLength, Inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthGranularInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" />,
    /// <paramref name="granuleLength" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public MonthGranularInterval(DateTime leftValue, DateTime rightValue, TimeSpan granuleLength,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, inclusion)
    {
        MonthsCount = GetMonthsCount(leftValue, rightValue, inclusion);

        ThrowIfNotValid(leftValue, rightValue, granuleLength, MonthsCount, inclusion);

        GranuleLength = granuleLength;
        Length = GetLength(leftValue, rightValue, granuleLength, inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthGranularInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granuleLength" />,
    /// <paramref name="monthsCount" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="monthsCount"></param>
    /// <param name="inclusion"></param>
    public MonthGranularInterval(DateTime leftValue, TimeSpan granuleLength, int monthsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : this(leftValue, GetRight(leftValue, granuleLength, monthsCount, inclusion), granuleLength, inclusion)
    {
    }

    /// <summary>
    /// Length of the granule
    /// </summary>
    public TimeSpan GranuleLength { get; }

    /// <inheritdoc />
    public override TimeSpan Length { get; }

    /// <summary>
    /// Months count in granule
    /// </summary>
    public int MonthsCount { get; protected set; }

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> MoveByGranule(int leftMultiplier, int rightMultiplier) =>
        this with
        {
            LeftValue = LeftValue + GranuleLength * leftMultiplier,
            RightValue = RightValue + GranuleLength * rightMultiplier
        };

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> MoveByLength(int leftMultiplier, int rightMultiplier)
    {
        var leftValue = LeftValue.AddMonths(MonthsCount * leftMultiplier);
        var rightValue = RightValue.AddMonths(MonthsCount * rightMultiplier);
        var (leftAddition, rightAddition) = Inclusion switch
        {
            IntervalInclusion.Opened => (
                TimeSpan.FromDays(DateTime.DaysInMonth(leftValue.Year, leftValue.Month) - leftValue.Day),
                TimeSpan.Zero),
            IntervalInclusion.Closed => (
                TimeSpan.Zero,
                TimeSpan.FromDays(DateTime.DaysInMonth(rightValue.Year, rightValue.Month) - rightValue.Day)),
            _ => (TimeSpan.Zero, TimeSpan.Zero)
        };
        return this with { LeftValue = leftValue + leftAddition, RightValue = rightValue + rightAddition };
    }

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

    private static DateTime GetRight(DateTime leftValue, TimeSpan granuleLength, int monthsCount,
        IntervalInclusion inclusion)
    {
        var addition = inclusion switch
        {
            IntervalInclusion.Opened => granuleLength,
            IntervalInclusion.Closed => -granuleLength,
            _ => TimeSpan.Zero
        };
        return leftValue.AddMonths(monthsCount) + addition;
    }

    private static int GetMonthsCount(DateTime leftValue, DateTime rightValue, IntervalInclusion inclusion)
    {
        var addition = inclusion switch
        {
            IntervalInclusion.Opened => -1,
            IntervalInclusion.Closed => 1,
            _ => 0
        };
        return (rightValue.Year - leftValue.Year) * DateTimeHelper.MonthsInYear + (rightValue.Month - leftValue.Month) +
               addition;
    }

    private static void ThrowIfNotValid(DateTime leftValue, DateTime rightValue, TimeSpan granuleLength,
        int monthsCount, IntervalInclusion inclusion)
    {
        if (monthsCount <= 0)
            throw new ArgumentException($"The {monthsCount} must not be less or equal zero");

        if (leftValue.Ticks % granuleLength.Ticks != rightValue.Ticks % granuleLength.Ticks)
            throw new ArgumentException(
                $"The {nameof(leftValue)} and {nameof(rightValue)} must be aligned to the time of day");

        var (leftInclusion, rightInclusion) = IntervalInclusionConverter.ToInclusions(inclusion);

        if (leftInclusion == Points.Inclusion.Excluded &&
            leftValue.Day != DateTime.DaysInMonth(leftValue.Year, leftValue.Month) ||
            leftInclusion == Points.Inclusion.Included && leftValue.Day != 1)
            throw new ArgumentException(
                $"The {nameof(leftValue)} must be last day of month when excluded and first day of month when included");

        if (rightInclusion == Points.Inclusion.Excluded && rightValue.Day != 1 ||
            rightInclusion == Points.Inclusion.Included &&
            rightValue.Day != DateTime.DaysInMonth(rightValue.Year, rightValue.Month))
            throw new ArgumentException(
                $"The {nameof(rightValue)} must be first day of month when excluded and last day of month when included");
    }
}