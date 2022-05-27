using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class WeekInterval : GranularIntervalBase<WeekInterval>
{
    public WeekInterval(DateTime startOfWeek)
        : base(startOfWeek, startOfWeek.AddDays(7), IntervalInclusion.RightOpened) =>
        StartOfWeek = startOfWeek;

    private WeekInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) => StartOfWeek = left.Value;

    public DateTime StartOfWeek { get; }

    protected override WeekInterval Create(Point<DateTime> left, Point<DateTime> right) => new(left, right);
}