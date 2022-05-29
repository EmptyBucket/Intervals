using Intervals.Utils;

namespace Intervals.Points;

public static class Endpoint
{
    public static Endpoint<T> Left<T>(Point<T> point) where T : IEquatable<T>, IComparable<T> =>
        new(point, EndpointLocation.Left);

    public static Endpoint<T> Right<T>(Point<T> point) where T : IEquatable<T>, IComparable<T> =>
        new(point, EndpointLocation.Right);
}

public readonly record struct Endpoint<T> : IComparable<Endpoint<T>> where T : IComparable<T>, IEquatable<T>
{
    private readonly Point<T> _point;

    public Endpoint(Point<T> point, EndpointLocation location)
    {
        _point = point;
        Location = location;
    }

    public T Value => _point.Value;

    public Inclusion Inclusion => _point.Inclusion;

    public EndpointLocation Location { get; }

    public void Deconstruct(out T value, out Inclusion inclusion, out EndpointLocation location)
    {
        (value, inclusion) = _point;
        location = Location;
    }

    public int CompareTo(Endpoint<T> other)
    {
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

    public static implicit operator Point<T>(Endpoint<T> endpoint) => endpoint._point;

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