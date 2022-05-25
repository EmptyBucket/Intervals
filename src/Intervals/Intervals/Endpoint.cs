namespace Intervals.Intervals;

public readonly record struct Endpoint<T> : IEndpoint<T> where T : IComparable<T>, IEquatable<T>
{
    private readonly IPoint<T> _point;

    public Endpoint(IPoint<T> point, EndpointLocation location)
    {
        _point = point;
        Location = location;
    }

    public T Value => _point.Value;

    public Inclusion Inclusion => _point.Inclusion;

    public EndpointLocation Location { get; }

    public int CompareTo(IEndpoint<T>? other)
    {
        if (other == null) return 1;

        var valueCompared = Value.CompareTo(other.Value);

        if (valueCompared != 0) return valueCompared;

        var locationCompared = Location - other.Location;
        var bothIsIncluded = Inclusion & other.Inclusion;

        // left location more right location, except both locations included
        //   )<(   ]<(   )<[   ]>[
        if (locationCompared != 0) return locationCompared * BitHelper.ToSign((int)bothIsIncluded);

        var inclusionCompared = Inclusion - other.Inclusion;
        var thisIsLeft = Location;

        // same inclusions equals, exclude more include when left location, otherwise include more exclude
        //   )=)   (=(   ]=]   [=[   )<]   (>[
        return inclusionCompared * BitHelper.ToSign((int)thisIsLeft);
    }

    public bool Equals(IEndpoint<T>? other) => other is Endpoint<T> otherEndpoint && Equals(otherEndpoint);

    public bool Equals(IPoint<T>? other) => _point.Equals(other);

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

    private static class BitHelper
    {
        /// <summary>
        /// Convert least significant bit to sign int. From 0 to 1, from 1 to -1
        /// </summary>
        /// <param name="bit">least significant bit</param>
        /// <returns>sign int</returns>
        public static int ToSign(int bit) => -bit | 1;
    }
}