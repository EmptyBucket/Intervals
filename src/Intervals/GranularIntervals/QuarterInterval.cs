using System;
using PeriodNet.Intervals;

namespace PeriodNet.GranularIntervals
{
	public class QuarterInterval : MonthsGranularInterval<QuarterInterval>
	{
		public QuarterInterval(int year, int quarter)
			: base(DateTimeHelper.GetStartOfQuarter(year, quarter), DateTimeHelper.GetOpenEndOfQuarter(year, quarter),
				IntervalType.RightOpen) =>
			(Year, Quarter) = (year, quarter);

		private QuarterInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
			(Year, Quarter) = (left.Value.Year, left.Value.GetQuarter());

		public int Year { get; }

		public int Quarter { get; }

		protected override QuarterInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new QuarterInterval(left, right);
	}
}