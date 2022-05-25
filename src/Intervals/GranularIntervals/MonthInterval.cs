using Intervals.Intervals;

namespace Intervals.GranularIntervals;

public class MonthInterval : MonthsGranularInterval<MonthInterval>
{
	public MonthInterval(int year, int month) : base(
		DateTimeHelper.GetStartOfMonth(year, month), DateTimeHelper.GetOpenedEndOfMonth(year, month),
		IntervalInclusion.RightOpened) =>
		(Year, Month) = (year, month);

	private MonthInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
		(Year, Month) = (left.Value.Year, left.Value.Month);

	public int Year { get; }

	public int Month { get; }

	protected override MonthInterval Create(Point<DateTime> left, Point<DateTime> right) =>
		new MonthInterval(left, right);
}