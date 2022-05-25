using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public abstract record MonthsGranularInterval<TInterval> : Interval<DateTime>, IGranularInterval<DateTime, TInterval>
    where TInterval : IGranularInterval<DateTime, TInterval>
{
    private readonly int _granulesCount;

    protected MonthsGranularInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
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