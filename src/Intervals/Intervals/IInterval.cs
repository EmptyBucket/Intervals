using Intervals.Points;

namespace Intervals.Intervals;

public interface IInterval<T> : IEquatable<IInterval<T>>, IComparable<IInterval<T>>, IEnumerable<IInterval<T>>
    where T : IComparable<T>, IEquatable<T>
{
    Endpoint<T> Left { get; }

    Endpoint<T> Right { get; }

    IntervalInclusion Inclusion { get; }
}