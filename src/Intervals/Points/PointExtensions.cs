namespace Intervals.Points;

public static class PointExtensions
{
    public static bool HasGap<T>(this Point<T> left, Point<T> right) where T : IEquatable<T> =>
        !left.Value.Equals(right.Value) || (left.Inclusion | right.Inclusion) != Inclusion.Included;
}