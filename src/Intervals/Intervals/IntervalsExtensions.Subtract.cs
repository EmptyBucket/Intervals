using Intervals.Points;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> Subtract<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        new SubtractEnumerable<T>(left, right);

    private class SubtractEnumerable<T> : IntervalDeviationEnumerable<T> where T : IEquatable<T>, IComparable<T>
    {
        private const int MinuendBatchIndex = 0;

        public SubtractEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
        {
        }

        protected override bool GetDeviance(IReadOnlyList<int> batchBalances) =>
            batchBalances[MinuendBatchIndex] > 0 && batchBalances.Skip(1).All(b => b == 0);

        protected override Point<T> CreatePoint(Point<T> point, int batchIndex) =>
            batchIndex == MinuendBatchIndex ? point : point with { Inclusion = point.Inclusion.Invert() };
    }
}