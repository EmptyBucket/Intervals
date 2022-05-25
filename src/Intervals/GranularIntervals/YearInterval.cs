using Intervals.Intervals;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

public record YearInterval : MonthsGranularInterval<YearInterval>
{
    public YearInterval(int year) : base(DateTimeHelper.GetStartOfYear(year), DateTimeHelper.GetOpenedEndOfYear(year),
        IntervalInclusion.RightOpened) =>
        Year = year;

    private YearInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) => Year = left.Value.Year;

    public int Year { get; }

    protected override YearInterval Create(Point<DateTime> left, Point<DateTime> right) => new(left, right);
}