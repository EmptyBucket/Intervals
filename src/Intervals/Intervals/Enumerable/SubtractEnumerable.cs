namespace Intervals.Intervals.Enumerable;

internal class SubtractEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public SubtractEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) =>
        batchBalances[0] > 0 && batchBalances.Skip(1).All(b => b == 0);
}