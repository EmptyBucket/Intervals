using Intervals.Points;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> Combine<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        new CombineEnumerable<T>(left, right);

    private class CombineEnumerable<T> : IntervalDeviationEnumerable<T> where T : IEquatable<T>, IComparable<T>
    {
        public CombineEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
        {
        }

        protected override bool GetDeviance(IReadOnlyList<int> batchBalances) => batchBalances.Any(b => b > 0);

        protected override Point<T> CreatePoint(Point<T> point, int batchIndex) => point;
    }
}