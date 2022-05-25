using System.Collections;
using Intervals.Points;

namespace Intervals.Intervals;

public record Interval<T> : IInterval<T> where T : IComparable<T>, IEquatable<T>
{
    public Interval(Point<T> left, Point<T> right)
    {
        Left = new Endpoint<T>(left, EndpointLocation.Left);
        Right = new Endpoint<T>(right, EndpointLocation.Right);
        Inclusion = IntervalInclusionConvert.FromInclusions(left.Inclusion, right.Inclusion);
    }

    public Interval(T leftValue, T rightValue, IntervalInclusion intervalInclusion = IntervalInclusion.RightOpened)
    {
        var (leftInclusion, rightInclusion) = IntervalInclusionConvert.ToInclusions(intervalInclusion);
        Left = new Endpoint<T>(new Point<T>(leftValue, leftInclusion), EndpointLocation.Left);
        Right = new Endpoint<T>(new Point<T>(rightValue, rightInclusion), EndpointLocation.Right);
        Inclusion = intervalInclusion;
    }

    public Endpoint<T> Left { get; }

    public Endpoint<T> Right { get; }

    public IntervalInclusion Inclusion { get; }

    public bool Equals(IInterval<T>? other) => other is Interval<T> otherInterval && Equals(otherInterval);

    public int CompareTo(IInterval<T>? other)
    {
        if (other == null) return 1;

        return Left.CompareTo(other.Left) is var leftCompared && leftCompared != 0
            ? leftCompared
            : Right.CompareTo(other.Right);
    }

    public override int GetHashCode() => HashCode.Combine(Left, Right);

    public IEnumerator<IInterval<T>> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"{Left}, {Right}";
}