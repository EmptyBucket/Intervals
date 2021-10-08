namespace PeriodNet.Intervals
{
	public static class IntervalTypeConvert
	{
		public static IntervalType FromInclusions(Inclusion left, Inclusion right) =>
			(IntervalType)((int)left << (int)EndpointLocation.Left | (int)right << (int)EndpointLocation.Right);

		public static (Inclusion Left, Inclusion Right) ToInclusions(IntervalType intervalType) =>
			((Inclusion)((int)intervalType >> (int)EndpointLocation.Left & 1),
				(Inclusion)((int)intervalType >> (int)EndpointLocation.Right & 1));
	}
}