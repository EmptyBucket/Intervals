using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class GranularInterval : GranularIntervalBase<GranularInterval>
{
    public GranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
    }

    public GranularInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, inclusion)
    {
    }

    protected override GranularInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint) =>
        new(leftPoint, rightPoint);
}