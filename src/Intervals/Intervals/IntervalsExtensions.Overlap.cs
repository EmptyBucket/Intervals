using Intervals.Points;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> Overlap<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        new OverlapEnumerable<T>(left, right);

    private class OverlapEnumerable<T> : IntervalDeviationEnumerable<T> where T : IEquatable<T>, IComparable<T>
    {
        public OverlapEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
        {
        }

        protected override bool GetDeviance(IReadOnlyList<int> batchBalances) => batchBalances.All(b => b > 0);

        protected override Point<T> CreatePoint(Point<T> point, int batchIndex) => point;
    }
}