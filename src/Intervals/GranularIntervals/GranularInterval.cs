using System;
using Intervals.Intervals;

namespace Intervals.GranularIntervals
{
	public abstract class GranularInterval<TInterval> : Interval<DateTime>, IGranularInterval<DateTime, TInterval>
		where TInterval : IGranularInterval<DateTime>
	{
		private readonly TimeSpan _granuleSize;

		protected GranularInterval(IPoint<DateTime> left, IPoint<DateTime> right)
			: base(left, right) =>
			_granuleSize = ComputeGranuleSize(left.Value, right.Value);

		protected GranularInterval(DateTime leftValue, DateTime rightValue, IntervalInclusion intervalInclusion)
			: base(leftValue, rightValue, intervalInclusion) =>
			_granuleSize = ComputeGranuleSize(leftValue, rightValue);

		public TInterval GetPrev() => AddBatches(-1);

		public TInterval GetNext() => AddBatches(1);

		private static TimeSpan ComputeGranuleSize(DateTime leftValue, DateTime rightValue) => rightValue - leftValue;

		private TInterval AddBatches(int batchesCount)
		{
			var totalGranulesSize = _granuleSize * batchesCount;
			return Create(
				new Point<DateTime>(Left.Value + totalGranulesSize, Right.Inclusion.Invert()),
				new Point<DateTime>(Right.Value + totalGranulesSize, Left.Inclusion.Invert()));
		}

		protected abstract TInterval Create(Point<DateTime> left, Point<DateTime> right);
	}

	public class GranularInterval : GranularInterval<GranularInterval>
	{
		public GranularInterval(IPoint<DateTime> left, IPoint<DateTime> right)
			: base(left, right)
		{
		}

		public GranularInterval(DateTime leftValue, DateTime rightValue,
			IntervalInclusion intervalInclusion = IntervalInclusion.RightOpened)
			: base(leftValue, rightValue, intervalInclusion)
		{
		}

		protected override GranularInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new GranularInterval(left, right);
	}
}