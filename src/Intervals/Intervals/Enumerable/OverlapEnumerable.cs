using Intervals.Points;

namespace Intervals.Intervals.Enumerable;

internal class OverlapEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public OverlapEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
    {
    }

    protected override IInterval<T> CreateInterval(EndpointContext leftContext, EndpointContext rightContext) =>
        new Interval<T>(leftContext.Endpoint, rightContext.Endpoint);

    protected override bool HasGap(EndpointContext leftContext, EndpointContext rightContext) =>
        !leftContext.Endpoint.Value.Equals(rightContext.Endpoint.Value) ||
        (leftContext.Endpoint.Inclusion | rightContext.Endpoint.Inclusion) != Inclusion.Included;

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) => batchBalances.All(b => b > 0);
}