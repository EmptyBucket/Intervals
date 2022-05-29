using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

public class MonthInterval : MonthGranularIntervalBase<MonthInterval>
{
    public MonthInterval(int year, int month) : base(DateTimeHelper.GetStartOfMonth(year, month),
        DateTimeHelper.GetOpenedEndOfMonth(year, month), IntervalInclusion.RightOpened)
    {
        Year = year;
        Month = month;
    }

    private MonthInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        Year = leftPoint.Value.Year;
        Month = leftPoint.Value.Month;
    }

    public int Year { get; }

    public int Month { get; }

    protected override MonthInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint) =>
        new(leftPoint, rightPoint);
}