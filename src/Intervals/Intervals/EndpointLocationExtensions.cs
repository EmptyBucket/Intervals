namespace Intervals.Intervals;

public static class EndpointLocationExtensions
{
	public static EndpointLocation Invert(this EndpointLocation endpointLocation) =>
		(EndpointLocation)((int)endpointLocation ^ 1);
}