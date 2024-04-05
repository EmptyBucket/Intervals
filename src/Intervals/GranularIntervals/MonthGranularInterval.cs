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
    private static TimeSpan _essentialGranuleLength = TimeSpan.FromDays(1);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthGranularInterval"/>
    /// with specified <paramref name="leftPoint" />, <paramref name="rightPoint" /> and
    /// <paramref name="granuleMonthsCount" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    /// <param name="granuleMonthsCount"></param>
    public MonthGranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint, int granuleMonthsCount)
        : base(leftPoint, rightPoint)
    {
        ThrowIfNotValid(leftPoint.Value, rightPoint.Value, granuleMonthsCount, Inclusion);

        GranuleMonthsCount = granuleMonthsCount;
        GranulesCount = GetMonthsCount(leftPoint.Value, rightPoint.Value) / granuleMonthsCount;
        Length = GetLength(leftPoint.Value, rightPoint.Value, Inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthGranularInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" />,
    /// <paramref name="granuleMonthsCount" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="granuleMonthsCount"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public MonthGranularInterval(DateTime leftValue, DateTime rightValue, int granuleMonthsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, inclusion)
    {
        ThrowIfNotValid(leftValue, rightValue, granuleMonthsCount, Inclusion);

        GranuleMonthsCount = granuleMonthsCount;
        GranulesCount = GetMonthsCount(leftValue, rightValue) / granuleMonthsCount;
        Length = GetLength(leftValue, rightValue, Inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthGranularInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granuleMonthsCount" /> and
    /// <paramref name="granuleMonthsCount" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granuleMonthsCount"></param>
    /// <param name="inclusion"></param>
    public MonthGranularInterval(DateTime leftValue, int granuleMonthsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : this(leftValue, GetRight(leftValue, granuleMonthsCount, inclusion), granuleMonthsCount, inclusion)
    {
    }

    /// <summary>
    /// Months count in granule
    /// </summary>
    public int GranuleMonthsCount { get; protected set; }

    /// <summary>
    /// Granules count
    /// </summary>
    public int GranulesCount { get; protected set; }

    /// <inheritdoc />
    public override TimeSpan Length { get; }

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> MoveByGranule(int leftMultiplier, int rightMultiplier)
    {
        var leftValue = LeftValue.AddMonths(GranuleMonthsCount * leftMultiplier);
        var rightValue = RightValue.AddMonths(GranuleMonthsCount * rightMultiplier);
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
    public override GranularInterval<DateTime, TimeSpan> MoveByLength(int leftMultiplier, int rightMultiplier) =>
        MoveByGranule(GranulesCount * leftMultiplier, GranulesCount * rightMultiplier);

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> Convert(IntervalInclusion inclusion)
    {
        var (leftInclusion, rightInclusion) = IntervalInclusionConverter.ToInclusions(inclusion);
        var leftAddition = Left.Inclusion != leftInclusion
            ? BitHelper.ToSign(leftInclusion == Points.Inclusion.Included) * _essentialGranuleLength
            : TimeSpan.Zero;
        var rightAddition = Right.Inclusion != rightInclusion
            ? BitHelper.ToSign(leftInclusion == Points.Inclusion.Excluded) * _essentialGranuleLength
            : TimeSpan.Zero;
        return this with
        {
            LeftValue = LeftValue + leftAddition, RightValue = RightValue + rightAddition, Inclusion = inclusion
        };
    }

    private static TimeSpan GetLength(DateTime leftValue, DateTime rightValue, IntervalInclusion inclusion)
    {
        var (leftAddition, rightAddition) = inclusion switch
        {
            IntervalInclusion.Opened => (_essentialGranuleLength, TimeSpan.Zero),
            IntervalInclusion.Closed => (TimeSpan.Zero, _essentialGranuleLength),
            _ => (TimeSpan.Zero, TimeSpan.Zero)
        };
        return GenericMath.Max((rightValue + rightAddition) - (leftValue + leftAddition), TimeSpan.Zero);
    }

    private static DateTime GetRight(DateTime leftValue, int granuleMonthsCount, IntervalInclusion inclusion)
    {
        var addition = inclusion switch
        {
            IntervalInclusion.Opened => _essentialGranuleLength,
            IntervalInclusion.Closed => -_essentialGranuleLength,
            _ => TimeSpan.Zero
        };
        return leftValue.AddMonths(granuleMonthsCount) + addition;
    }

    private static int GetMonthsCount(DateTime leftValue, DateTime rightValue) =>
        (rightValue.Year - leftValue.Year) * DateTimeHelper.MonthsInYear + (rightValue.Month - leftValue.Month);

    private static void ThrowIfNotValid(DateTime leftValue, DateTime rightValue, int granuleMonthsCount,
        IntervalInclusion inclusion)
    {
        if (granuleMonthsCount <= 0)
            throw new ArgumentException($"The {granuleMonthsCount} must not be less or equal zero");

        if (leftValue.Ticks % _essentialGranuleLength.Ticks != rightValue.Ticks % _essentialGranuleLength.Ticks)
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