namespace Intervals.Intervals;

public static class EndpointLocationExtensions
{
    public static EndpointLocation Invert(this EndpointLocation location) => (EndpointLocation)((int)location ^ 1);
}