namespace Intervals.Intervals;

public static class Point
{
    public static IPoint<T> Included<T>(T value) where T : IEquatable<T> => new Point<T>(value, Inclusion.Included);

    public static IPoint<T> Excluded<T>(T value) where T : IEquatable<T> => new Point<T>(value, Inclusion.Excluded);
}

public readonly record struct Point<T>(T Value, Inclusion Inclusion) : IPoint<T> where T : IEquatable<T>
{
    public bool Equals(IPoint<T>? other) => other is Point<T> otherPoint && Equals(otherPoint);

    public override string ToString() => $"{Value}-{Inclusion}";
}