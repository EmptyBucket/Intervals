using Intervals.Intervals;

namespace Intervals.GranularIntervals;

public class DayInterval : GranularInterval<DayInterval>
{
	public DayInterval(DateTime dateTime) : base(dateTime, dateTime.AddDays(1), IntervalInclusion.RightOpened) =>
		DateTime = dateTime;

	private DayInterval(IPoint<DateTime> left, IPoint<DateTime> right) : base(left, right) => DateTime = left.Value;

	public DateTime DateTime { get; }

	protected override DayInterval Create(Point<DateTime> left, Point<DateTime> right) => new(left, right);
}