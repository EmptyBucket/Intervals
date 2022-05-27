namespace Intervals.Intervals.Enumerable;

internal class SymmetricDifferenceEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public SymmetricDifferenceEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
        : base(left, right)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) => batchBalances.Count(b => b > 0) == 1;
}