using Intervals.Intervals;

namespace Intervals.GranularIntervals;

public class HalfAYearInterval : MonthsGranularInterval<HalfAYearInterval>
{
	public HalfAYearInterval(int year, int halfAYear) : base(
		DateTimeHelper.GetStartOfHalfAYear(year, halfAYear), DateTimeHelper.GetOpenedEndOfHalfAYear(year, halfAYear),
		IntervalInclusion.RightOpened) =>
		(Year, HalfAYear) = (year, halfAYear);

	private HalfAYearInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
		(Year, HalfAYear) = (left.Value.Year, left.Value.GetHalfAYear());

	public int Year { get; }

	public int HalfAYear { get; }

	protected override HalfAYearInterval Create(Point<DateTime> left, Point<DateTime> right) =>
		new HalfAYearInterval(left, right);
}