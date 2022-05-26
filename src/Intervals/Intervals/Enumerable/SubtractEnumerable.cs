using Intervals.Points;

namespace Intervals.Intervals.Enumerable;

internal class SubtractEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public SubtractEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
    {
    }

    protected override IInterval<T> CreateInterval(EndpointContext leftContext, EndpointContext rightContext) =>
        new Interval<T>(CreatePoint(leftContext), CreatePoint(rightContext));

    protected override bool HasGap(EndpointContext leftContext, EndpointContext rightContext)
    {
        var leftPoint = CreatePoint(leftContext);
        var rightPoint = CreatePoint(rightContext);
        return !leftPoint.Value.Equals(rightPoint.Value) ||
               (leftPoint.Inclusion | rightPoint.Inclusion) != Inclusion.Included;
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) =>
        batchBalances[0] > 0 && batchBalances.Skip(1).All(b => b == 0);

    private static Point<T> CreatePoint(EndpointContext context) => context.BatchIndex == 0
        ? context.Endpoint
        : new Point<T>(context.Endpoint.Value, context.Endpoint.Inclusion.Invert());
}