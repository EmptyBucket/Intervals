using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

public class YearInterval : MonthGranularIntervalBase<YearInterval>
{
    public YearInterval(int year) : base(DateTimeHelper.GetStartOfYear(year), DateTimeHelper.GetOpenedEndOfYear(year),
        IntervalInclusion.RightOpened)
    {
        Year = year;
    }

    private YearInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        Year = leftPoint.Value.Year;
    }

    public int Year { get; }

    protected override YearInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint) =>
        new(leftPoint, rightPoint);
}