using Intervals.Intervals;

namespace Intervals.GranularIntervals;

public class YearInterval : MonthsGranularInterval<YearInterval>
{
	public YearInterval(int year) : base(
		DateTimeHelper.GetStartOfYear(year), DateTimeHelper.GetOpenedEndOfYear(year), IntervalInclusion.RightOpened) =>
		Year = year;

	private YearInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
		Year = left.Value.Year;

	public int Year { get; }

	protected override YearInterval Create(Point<DateTime> left, Point<DateTime> right) =>
		new YearInterval(left, right);
}