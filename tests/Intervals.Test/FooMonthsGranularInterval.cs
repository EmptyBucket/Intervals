using Intervals.GranularIntervals;
using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.Test;

public record FooMonthsGranularInterval : MonthsGranularInterval<FooMonthsGranularInterval>
{
    public FooMonthsGranularInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right)
    {
    }

    protected override FooMonthsGranularInterval Create(Point<DateTime> left, Point<DateTime> right) =>
        new(left, right);
}