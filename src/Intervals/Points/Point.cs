namespace Intervals.Points;

public static class Point
{
    public static Point<T> Included<T>(T value) where T : IEquatable<T> => new(value, Inclusion.Included);

    public static Point<T> Excluded<T>(T value) where T : IEquatable<T> => new(value, Inclusion.Excluded);
}

public readonly record struct Point<T>(T Value, Inclusion Inclusion) where T : IEquatable<T>
{
    public override string ToString() => $"{Value}-{Inclusion}";
}