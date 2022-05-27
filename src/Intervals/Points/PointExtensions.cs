namespace Intervals.Points;

public static class PointExtensions
{
    public static bool HasGap<T>(this Point<T> firstPoint, Point<T> secondPoint) where T : IEquatable<T> =>
        !firstPoint.Value.Equals(secondPoint.Value) ||
        (firstPoint.Inclusion | secondPoint.Inclusion) != Inclusion.Included;
}