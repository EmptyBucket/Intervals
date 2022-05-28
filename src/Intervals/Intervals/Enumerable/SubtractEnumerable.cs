using System.Collections.Immutable;

namespace Intervals.Intervals.Enumerable;

internal class SubtractEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public static IEnumerable<IInterval<T>> Create(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) =>
        new SubtractEnumerable<T>(ImmutableList.Create(left, right));

    private SubtractEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches) : base(batches)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) =>
        batchBalances[0] > 0 && batchBalances.Skip(1).All(b => b == 0);
}