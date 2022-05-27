using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class GranularInterval : GranularIntervalBase<GranularInterval>
{
    public GranularInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right)
    {
    }

    public GranularInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion intervalInclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, intervalInclusion)
    {
    }

    protected override GranularInterval Create(Point<DateTime> left, Point<DateTime> right) => new(left, right);
}