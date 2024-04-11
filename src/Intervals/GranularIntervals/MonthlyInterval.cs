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
/// Represents an monthly interval instance
/// </summary>
[Serializable]
public record class MonthlyInterval : TimeGranularInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="leftPoint" />, <paramref name="rightPoint" /> and
    /// <paramref name="granuleLength" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    /// <param name="granuleLength"></param>
    public MonthlyInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint, TimeSpan granuleLength)
        : base(leftPoint, rightPoint, granuleLength)
    {
        MonthsCount = GetMonthsCount(leftPoint.Value, rightPoint.Value, Inclusion);

        ThrowIfNotValid(leftPoint.Value, rightPoint.Value, Inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" />,
    /// <paramref name="granuleLength" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public MonthlyInterval(DateTime leftValue, DateTime rightValue, TimeSpan granuleLength,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, granuleLength, inclusion)
    {
        MonthsCount = GetMonthsCount(leftValue, rightValue, inclusion);

        ThrowIfNotValid(leftValue, rightValue, inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granuleLength" />,
    /// <paramref name="monthsCount" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="monthsCount"></param>
    /// <param name="inclusion"></param>
    public MonthlyInterval(DateTime leftValue, TimeSpan granuleLength, int monthsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, GetRight(leftValue, granuleLength, monthsCount, inclusion), granuleLength, inclusion)
    {
        MonthsCount = monthsCount;

        ThrowIfNotValid(leftValue, RightValue, inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="year" />, <paramref name="month" />, <paramref name="granuleLength" />,
    /// <paramref name="granuleLength" />, <paramref name="inclusion" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="granuleLength"></param>
    /// <param name="monthsCount"></param>
    /// <param name="inclusion"></param>
    /// <param name="kind"></param>
    public MonthlyInterval(int year, int month, TimeSpan granuleLength, int monthsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened, DateTimeKind kind = DateTimeKind.Unspecified)
        : this(GetLeft(DateTimeHelper.GetMonthStart(year, month, kind), granuleLength, inclusion), granuleLength,
            monthsCount, inclusion)
    {
    }

    /// <summary>
    /// Months count in granule
    /// </summary>
    public int MonthsCount { get; protected set; }

    /// <inheritdoc />
    public override GranularInterval<DateTime, TimeSpan> MoveByLength(int leftMultiplier, int rightMultiplier)
    {
        var leftValue = LeftValue.AddMonths(MonthsCount * leftMultiplier);
        var rightValue = RightValue.AddMonths(MonthsCount * rightMultiplier);
        var (leftAddition, rightAddition) = Inclusion switch
        {
            IntervalInclusion.Opened => (TimeSpan.FromDays(leftValue.GetDaysUntilMonth()), TimeSpan.Zero),
            IntervalInclusion.Closed => (TimeSpan.Zero, TimeSpan.FromDays(rightValue.GetDaysUntilMonth())),
            _ => (TimeSpan.Zero, TimeSpan.Zero)
        };
        return this with { LeftValue = leftValue + leftAddition, RightValue = rightValue + rightAddition };
    }

    private static DateTime GetLeft(DateTime monthStart, TimeSpan granuleLength, IntervalInclusion inclusion)
    {
        var addition = inclusion is IntervalInclusion.Opened or IntervalInclusion.LeftOpened
            ? -granuleLength
            : TimeSpan.Zero;
        return monthStart + addition;
    }

    private static DateTime GetRight(DateTime leftValue, TimeSpan granuleLength, int monthsCount,
        IntervalInclusion inclusion)
    {
        var rightValue = leftValue.AddMonths(monthsCount);
        var addition = inclusion switch
        {
            IntervalInclusion.Opened => TimeSpan.FromDays(rightValue.GetDaysUntilMonth()) + granuleLength,
            IntervalInclusion.LeftOpened => TimeSpan.FromDays(rightValue.GetDaysUntilMonth()),
            IntervalInclusion.Closed => -granuleLength,
            _ => TimeSpan.Zero
        };
        return rightValue + addition;
    }

    private static int GetMonthsCount(DateTime leftValue, DateTime rightValue, IntervalInclusion inclusion)
    {
        var addition = inclusion switch
        {
            IntervalInclusion.Opened => -1,
            IntervalInclusion.Closed => 1,
            _ => 0
        };
        return (rightValue.Year - leftValue.Year) * DateTimeHelper.MonthsInYear +
               (rightValue.Month - leftValue.Month) + addition;
    }

    private static void ThrowIfNotValid(DateTime leftValue, DateTime rightValue, IntervalInclusion inclusion)
    {
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