using System;
using Intervals.Intervals;

namespace Intervals.GranularIntervals
{
	public class WeekInterval : GranularInterval<WeekInterval>
	{
		public WeekInterval(DateTime dateTime) : base(dateTime, dateTime.AddDays(7), IntervalInclusion.RightOpened) =>
			DateTime = dateTime;

		private WeekInterval(IPoint<DateTime> left, IPoint<DateTime> right) : base(left, right) => DateTime = left.Value;

		public DateTime DateTime { get; }

		protected override WeekInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new WeekInterval(left, right);
	}
}