using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public abstract class GranularIntervalBase<TInterval> : Interval<DateTime>, IGranularInterval<DateTime, TInterval>
    where TInterval : IGranularInterval<DateTime, TInterval>
{
    private readonly TimeSpan _granuleSize;

    protected GranularIntervalBase(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
        _granuleSize = ComputeGranuleSize(left.Value, right.Value);

    protected GranularIntervalBase(DateTime leftValue, DateTime rightValue, IntervalInclusion intervalInclusion)
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