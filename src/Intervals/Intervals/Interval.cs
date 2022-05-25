using System.Collections;

namespace Intervals.Intervals;

public static class Interval
{
	public static IInterval<T> New<T>(IPoint<T> left, IPoint<T> right)
		where T : IEquatable<T>, IComparable<T> => new Interval<T>(left, right);

	public static IInterval<T> New<T>(T leftValue, T rightValue, IntervalInclusion intervalInclusion)
		where T : IEquatable<T>, IComparable<T> => new Interval<T>(leftValue, rightValue, intervalInclusion);
}

public class Interval<T> : IInterval<T> where T : IComparable<T>, IEquatable<T>
{
	protected internal Interval(IPoint<T> left, IPoint<T> right)
	{
		Left = Endpoint.New(left, EndpointLocation.Left);
		Right = Endpoint.New(right, EndpointLocation.Right);
		Inclusion = IntervalInclusionConvert.FromInclusions(left.Inclusion, right.Inclusion);
	}

	protected internal Interval(T leftValue, T rightValue, IntervalInclusion intervalInclusion)
	{
		var (leftInclusion, rightInclusion) = IntervalInclusionConvert.ToInclusions(intervalInclusion);
		Left = Endpoint.New(Point.New(leftValue, leftInclusion), EndpointLocation.Left);
		Right = Endpoint.New(Point.New(rightValue, rightInclusion), EndpointLocation.Right);
		Inclusion = intervalInclusion;
	}

	public virtual IEndpoint<T> Left { get; }

	public virtual IEndpoint<T> Right { get; }

	public IntervalInclusion Inclusion { get; }

	public bool Equals(IInterval<T> other) => Left.Equals(other.Left) && Right.Equals(other.Right);

	public int CompareTo(IInterval<T> other) => Left.CompareTo(other.Left) is var leftCompareTo && leftCompareTo != 0
		? leftCompareTo
		: Right.CompareTo(other.Right);

	public override int GetHashCode() => HashCode.Combine(Left, Right);

	public IEnumerator<IInterval<T>> GetEnumerator()
	{
		yield return this;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public override string ToString() => $"{Left}, {Right}";
}