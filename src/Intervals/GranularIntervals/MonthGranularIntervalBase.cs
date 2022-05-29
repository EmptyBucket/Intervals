using Intervals.Intervals;
using Intervals.Points;

namespace Intervals.GranularIntervals;

public abstract class MonthGranularIntervalBase<TInterval> : Interval<DateTime>, IGranularInterval<DateTime, TInterval>
    where TInterval : IGranularInterval<DateTime, TInterval>
{
    private readonly int _granulesCount;

    protected MonthGranularIntervalBase(Point<DateTime> leftPoint, Point<DateTime> rightPoint)
        : base(leftPoint, rightPoint)
    {
        _granulesCount = GranulesCount(leftPoint.Value, rightPoint.Value);
    }

    protected MonthGranularIntervalBase(DateTime leftValue, DateTime rightValue, IntervalInclusion inclusion)
        : base(leftValue, rightValue, inclusion)
    {
        _granulesCount = GranulesCount(leftValue, rightValue);
    }

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

    protected abstract TInterval Create(Point<DateTime> leftPoint, Point<DateTime> rightPoint);
}