using System;
using PeriodNet.GranularIntervals;
using PeriodNet.Intervals;

namespace PeriodNet.Test
{
	public class FooInterval : MonthsGranularInterval<FooInterval>
	{
		public FooInterval(IPoint<DateTime> left, IPoint<DateTime> right) : base(left, right)
		{
		}

		protected override FooInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new FooInterval(left, right);
	}
}