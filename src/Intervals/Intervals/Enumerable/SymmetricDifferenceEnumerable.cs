using System.Collections.Immutable;

namespace Intervals.Intervals.Enumerable;

internal class SymmetricDifferenceEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public static IEnumerable<IInterval<T>> Create(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
    {
        var builder = ImmutableList.CreateBuilder<IEnumerable<IInterval<T>>>();

        if (left is SymmetricDifferenceEnumerable<T> l) builder.AddRange(l.Batches);
        else builder.Add(left);

        if (right is SymmetricDifferenceEnumerable<T> r) builder.AddRange(r.Batches);
        else builder.Add(right);

        return new SymmetricDifferenceEnumerable<T>(builder.ToImmutable());
    }

    private SymmetricDifferenceEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches) : base(batches)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) => batchBalances.Count(b => b > 0) == 1;
}