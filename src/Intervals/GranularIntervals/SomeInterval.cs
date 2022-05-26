using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public class SomeInterval : GranularInterval<SomeInterval>
{
    public SomeInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right)
    {
    }

    public SomeInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion intervalInclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, intervalInclusion)
    {
    }

    protected override SomeInterval Create(Point<DateTime> left, Point<DateTime> right) => new(left, right);
}