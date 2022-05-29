using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class WeekInterval : GranularIntervalBase<WeekInterval>
{
    public WeekInterval(DateTime startOfWeek) : base(startOfWeek, startOfWeek.AddDays(7), IntervalInclusion.RightOpened)
    {
        StartOfWeek = startOfWeek;
    }

    private WeekInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        StartOfWeek = leftPoint.Value;
    }

    public DateTime StartOfWeek { get; }

    protected override WeekInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint) =>
        new(leftPoint, rightPoint);
}