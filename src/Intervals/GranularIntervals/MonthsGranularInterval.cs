using Intervals.Intervals;

namespace Intervals.GranularIntervals;

public abstract class MonthsGranularInterval<TInterval> : Interval<DateTime>, IGranularInterval<DateTime, TInterval>
	where TInterval : IGranularInterval<DateTime>
{
	private readonly int _granulesCount;

	protected MonthsGranularInterval(IPoint<DateTime> left, IPoint<DateTime> right)
		: base(left, right) =>
		_granulesCount = GranulesCount(left.Value, right.Value);

	protected MonthsGranularInterval(DateTime leftValue, DateTime rightValue, IntervalInclusion intervalInclusion)
		: base(leftValue, rightValue, intervalInclusion) =>
		_granulesCount = GranulesCount(leftValue, rightValue);

	public TInterval GetPrev() => AddBatches(-1);

	public TInterval GetNext() => AddBatches(1);

	private static int GranulesCount(DateTime leftValue, DateTime rightValue) =>
		(rightValue.Year - leftValue.Year) * 12 + (rightValue.Month - leftValue.Month);

	private TInterval AddBatches(int batchesCount)
	{
		var totalGranulesCount = _granulesCount * batchesCount;
		return Create(
			new Point<DateTime>(Left.Value.AddMonths(totalGranulesCount), Right.Inclusion.Invert()),
			new Point<DateTime>(Right.Value.AddMonths(totalGranulesCount), Left.Inclusion.Invert()));
	}

	protected abstract TInterval Create(Point<DateTime> left, Point<DateTime> right);
}