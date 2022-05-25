namespace Intervals.Intervals;

public static class Point
{
    public static IPoint<T> New<T>(T value, Inclusion inclusion) where T : IEquatable<T> =>
        new Point<T>(value, inclusion);

    public static IPoint<T> Included<T>(T value) where T : IEquatable<T> => New(value, Inclusion.Included);

    public static IPoint<T> Excluded<T>(T value) where T : IEquatable<T> => New(value, Inclusion.Excluded);
}

public readonly record struct Point<T>(T Value, Inclusion Inclusion) : IPoint<T> where T : IEquatable<T>
{
    public override string ToString() => $"{Inclusion} {Value}";

    public bool Equals(IPoint<T>? other) =>
        other != null && EqualityComparer<T>.Default.Equals(Value, other.Value) && Inclusion == other.Inclusion;
}