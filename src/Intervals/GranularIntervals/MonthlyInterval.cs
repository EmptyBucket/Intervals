using System.Text.Json.Serialization;
using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an month interval instance where the granule size is month
/// </summary>
[Serializable]
public record class MonthlyInterval : MonthGranularInterval
{
    private new const int GranuleMonthsCount = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="leftPoint" /> and <paramref name="rightPoint" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    public MonthlyInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint)
        : base(leftPoint, rightPoint, GranuleMonthsCount)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public MonthlyInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, GranuleMonthsCount, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="leftValue" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="inclusion"></param>
    public MonthlyInterval(DateTime leftValue, IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, GranuleMonthsCount, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthInterval"/>
    /// with specified <paramref name="year" />, <paramref name="month" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="kind"></param>
    public MonthlyInterval(int year, int month, DateTimeKind kind = DateTimeKind.Unspecified)
        : base(DateTimeHelper.New(year, month, 1, kind), GranuleMonthsCount)
    {
    }
}