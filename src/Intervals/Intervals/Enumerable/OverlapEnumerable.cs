using System.Collections.Immutable;

namespace Intervals.Intervals.Enumerable;

internal class OverlapEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public static IEnumerable<IInterval<T>> Create(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
    {
        var builder = ImmutableList.CreateBuilder<IEnumerable<IInterval<T>>>();

        if (left is OverlapEnumerable<T> l) builder.AddRange(l.Batches);
        else builder.Add(left);

        if (right is OverlapEnumerable<T> r) builder.AddRange(r.Batches);
        else builder.Add(right);

        return new OverlapEnumerable<T>(builder.ToImmutable());
    }

    private OverlapEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches) : base(batches)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) => batchBalances.All(b => b > 0);
}