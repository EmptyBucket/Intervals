using System.Text.Json.Serialization;
using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an weekly interval instance where the granule length is 7 day
/// </summary>
[Serializable]
public record class WeeklyInterval : TimeGranularInterval
{
    private new static readonly TimeSpan GranuleLength = TimeSpan.FromDays(7);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.WeeklyInterval"/>
    /// with specified <paramref name="leftPoint" /> and <paramref name="rightPoint" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    public WeeklyInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint)
        : base(leftPoint, rightPoint, GranuleLength)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.WeeklyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public WeeklyInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, GranuleLength, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.WeeklyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granulesCount" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granulesCount"></param>
    /// <param name="inclusion"></param>
    public WeeklyInterval(DateTime leftValue, int granulesCount = 1,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, GranuleLength, granulesCount, inclusion)
    {
    }
}