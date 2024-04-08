using System.Text.Json.Serialization;
using Intervals.Intervals;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an month interval instance
/// </summary>
[Serializable]
public record class MonthInterval : MonthGranularInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthlyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granuleLength" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public MonthInterval(DateTime leftValue, TimeSpan granuleLength,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, granuleLength, 1, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.MonthInterval"/>
    /// with specified <paramref name="year" />, <paramref name="month" />, <paramref name="granuleLength" /> and
    /// <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="granuleLength"></param>
    /// <param name="kind"></param>
    public MonthInterval(int year, int month, TimeSpan granuleLength, DateTimeKind kind = DateTimeKind.Unspecified)
        : this(DateTimeHelper.New(year, month, 1, kind), granuleLength)
    {
    }
}