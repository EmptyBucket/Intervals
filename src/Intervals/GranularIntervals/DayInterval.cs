using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class DayInterval : GranularIntervalBase<DayInterval>
{
    public DayInterval(DateTime startOfDay) : base(startOfDay, startOfDay.AddDays(1), IntervalInclusion.RightOpened)
    {
        StartOfDay = startOfDay;
    }

    private DayInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        StartOfDay = leftPoint.Value;
    }

    public DateTime StartOfDay { get; }

    protected override DayInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint) =>
        new(leftPoint, rightPoint);
}