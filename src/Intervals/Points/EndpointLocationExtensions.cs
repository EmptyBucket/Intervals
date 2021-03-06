namespace Intervals.Points;

public static class EndpointLocationExtensions
{
    public static EndpointLocation Invert(this EndpointLocation location) => (EndpointLocation)((int)location ^ 1);
}