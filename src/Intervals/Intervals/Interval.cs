using System.Collections;

namespace Intervals.Intervals;

public class Interval<T> : IInterval<T> where T : IComparable<T>, IEquatable<T>
{
    public Interval(IPoint<T> left, IPoint<T> right)
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

    public virtual IEndpoint<T> Left { get; }

    public virtual IEndpoint<T> Right { get; }

    public IntervalInclusion Inclusion { get; }

    public bool Equals(IInterval<T>? other) =>
        other is Interval<T> otherInterval && Left.Equals(otherInterval.Left) && Right.Equals(otherInterval.Right);

    public int CompareTo(IInterval<T>? other)
    {
        if (other == null) return 1;

        return Left.CompareTo(other.Left) is var leftCompared && leftCompared != 0
            ? leftCompared
            : Right.CompareTo(other.Right);
    }

    public IEnumerator<IInterval<T>> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"{Left}, {Right}";
}