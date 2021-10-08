namespace PeriodNet.Intervals
{
	public static class InclusionExtensions
	{
		public static Inclusion Invert(this Inclusion inclusion) =>
			(Inclusion)((int)inclusion ^ 1);
	}
}