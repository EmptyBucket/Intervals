using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

public class QuarterInterval : MonthGranularIntervalBase<QuarterInterval>
{
    public QuarterInterval(int year, int quarter) : base(DateTimeHelper.GetStartOfQuarter(year, quarter),
        DateTimeHelper.GetOpenedEndOfQuarter(year, quarter), IntervalInclusion.RightOpened)
    {
        Year = year;
        Quarter = quarter;
    }

    private QuarterInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        Year = leftPoint.Value.Year;
        Quarter = leftPoint.Value.GetQuarter();
    }

    public int Year { get; }

    public int Quarter { get; }

    protected override QuarterInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint) =>
        new(leftPoint, rightPoint);
}