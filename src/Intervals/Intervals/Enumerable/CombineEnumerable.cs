namespace Intervals.Intervals.Enumerable;

internal class CombineEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public CombineEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) => batchBalances.Any(b => b > 0);
}