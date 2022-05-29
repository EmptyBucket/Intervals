using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

public class HalfAYearInterval : MonthGranularIntervalBase<HalfAYearInterval>
{
    public HalfAYearInterval(int year, int halfAYear) : base(DateTimeHelper.GetStartOfHalfAYear(year, halfAYear),
        DateTimeHelper.GetOpenedEndOfHalfAYear(year, halfAYear), IntervalInclusion.RightOpened)
    {
        Year = year;
        HalfAYear = halfAYear;
    }

    private HalfAYearInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint) : base(leftPoint, rightPoint)
    {
        Year = leftPoint.Value.Year;
        HalfAYear = leftPoint.Value.GetHalfAYear();
    }

    public int Year { get; }

    public int HalfAYear { get; }

    protected override HalfAYearInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint) =>
        new(leftPoint, rightPoint);
}