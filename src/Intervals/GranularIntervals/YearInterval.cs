using System;
using PeriodNet.Intervals;

namespace PeriodNet.GranularIntervals
{
	public class YearInterval : MonthsGranularInterval<YearInterval>
	{
		public YearInterval(int year)
			: base(DateTimeHelper.GetStartOfYear(year), DateTimeHelper.GetOpenEndOfYear(year), IntervalType.RightOpen) =>
			Year = year;

		private YearInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
			Year = left.Value.Year;

		public int Year { get; }

		protected override YearInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new YearInterval(left, right);
	}
}