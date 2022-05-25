namespace Intervals.Intervals;

public static class Endpoint
{
	public static IEndpoint<T> New<T>(IPoint<T> point, EndpointLocation location)
		where T : IEquatable<T>, IComparable<T> => new Endpoint<T>(point, location);
}

public readonly struct Endpoint<T> : IEndpoint<T> where T : IComparable<T>, IEquatable<T>
{
	private readonly IPoint<T> _point;

	public Endpoint(IPoint<T> point, EndpointLocation location) => (_point, Location) = (point, location);

	public T Value => _point.Value;

	public Inclusion Inclusion => _point.Inclusion;

	public EndpointLocation Location { get; }

	public int CompareTo(IEndpoint<T>? other)
	{
		var valueCompared = Value.CompareTo(other.Value);

		if (valueCompared != 0) return valueCompared;

		var locationCompared = Location - other.Location;
		var inclusionCompared = Inclusion - other.Inclusion;

		//flags
		var byLocation = locationCompared & 1;
		var byInclusion = byLocation ^ 1;
		var bothIsIncluded = (int)(Inclusion & other.Inclusion);
		var thisIsRight = (int)Location;
		int ToSign(int signBit) => -signBit | 1;

		return byLocation * locationCompared * ToSign(bothIsIncluded) |
		       byInclusion * inclusionCompared * ToSign(thisIsRight);
	}

	public bool Equals(IEndpoint<T> other) => Equals((IPoint<T>)other) && Location == other.Location;

	public bool Equals(IPoint<T> other) => _point.Equals(other);

	public override bool Equals(object? obj) => obj is IEndpoint<T> other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(Value, Inclusion, Location);

	public override string ToString() => Location switch
	{
		EndpointLocation.Left => Inclusion switch
		{
			Inclusion.Excluded => $"({Value}",
			Inclusion.Included => $"[{Value}",
			_ => throw new ArgumentOutOfRangeException()
		},
		EndpointLocation.Right => Inclusion switch
		{
			Inclusion.Excluded => $"{Value})",
			Inclusion.Included => $"{Value}]",
			_ => throw new ArgumentOutOfRangeException()
		},
		_ => throw new ArgumentOutOfRangeException()
	};
}