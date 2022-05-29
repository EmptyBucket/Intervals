using System.Collections;
using Intervals.Points;

namespace Intervals.Intervals;

public class Interval<T> : IInterval<T> where T : IComparable<T>, IEquatable<T>
{
    public Interval(Point<T> leftPoint, Point<T> rightPoint)
    {
        Left = Endpoint.Left(leftPoint);
        Right = Endpoint.Right(rightPoint);
        Inclusion = IntervalInclusionConvert.FromInclusions(leftPoint.Inclusion, rightPoint.Inclusion);
    }

    public Interval(T leftValue, T rightValue, IntervalInclusion inclusion = IntervalInclusion.RightOpened)
    {
        var (leftInclusion, rightInclusion) = IntervalInclusionConvert.ToInclusions(inclusion);
        Left = Endpoint.Left(new Point<T>(leftValue, leftInclusion));
        Right = Endpoint.Right(new Point<T>(rightValue, rightInclusion));
        Inclusion = inclusion;
    }

    public Endpoint<T> Left { get; }

    public Endpoint<T> Right { get; }

    public IntervalInclusion Inclusion { get; }

    public void Deconstruct(out Endpoint<T> left, out Endpoint<T> right)
    {
        left = Left;
        right = Right;
    }

    public bool Equals(IInterval<T>? other) =>
        other is Interval<T> otherInterval && Left == otherInterval.Left && Right == otherInterval.Right;

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