namespace Intervals.Intervals.Enumerable;

internal class OverlapEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public OverlapEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) => batchBalances.All(b => b > 0);
}