namespace Intervals.Intervals
{
	public static class IntervalInclusionConvert
	{
		public static IntervalInclusion FromInclusions(Inclusion left, Inclusion right) =>
			(IntervalInclusion)((int)left << (int)EndpointLocation.Left | (int)right << (int)EndpointLocation.Right);

		public static (Inclusion Left, Inclusion Right) ToInclusions(IntervalInclusion intervalInclusion) =>
			((Inclusion)((int)intervalInclusion >> (int)EndpointLocation.Left & 1),
				(Inclusion)((int)intervalInclusion >> (int)EndpointLocation.Right & 1));
	}
}