using System;
using Intervals.GranularIntervals;
using Intervals.Intervals;

namespace Intervals.Test
{
	public class FooMonthsGranularInterval : MonthsGranularInterval<FooMonthsGranularInterval>
	{
		public FooMonthsGranularInterval(IPoint<DateTime> left, IPoint<DateTime> right) : base(left, right)
		{
		}

		protected override FooMonthsGranularInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new FooMonthsGranularInterval(left, right);
	}
}