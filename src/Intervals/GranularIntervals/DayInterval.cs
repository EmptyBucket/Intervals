using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class DayInterval : GranularInterval<DayInterval>
{
    public DayInterval(DateTime startOfDay) : base(startOfDay, startOfDay.AddDays(1), IntervalInclusion.RightOpened) =>
        StartOfDay = startOfDay;

    private DayInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) => StartOfDay = left.Value;

    public DateTime StartOfDay { get; }

    protected override DayInterval Create(Point<DateTime> left, Point<DateTime> right) => new(left, right);
}