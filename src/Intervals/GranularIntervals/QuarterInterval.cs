using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

public class QuarterInterval : MonthsGranularInterval<QuarterInterval>
{
    public QuarterInterval(int year, int quarter)
        : base(DateTimeHelper.GetStartOfQuarter(year, quarter), DateTimeHelper.GetOpenedEndOfQuarter(year, quarter),
            IntervalInclusion.RightOpened) =>
        (Year, Quarter) = (year, quarter);

    private QuarterInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
        (Year, Quarter) = (left.Value.Year, left.Value.GetQuarter());

    public int Year { get; }

    public int Quarter { get; }

    protected override QuarterInterval Create(Point<DateTime> left, Point<DateTime> right) => new(left, right);
}