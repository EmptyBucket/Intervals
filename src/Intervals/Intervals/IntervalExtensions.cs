using System;

namespace Intervals.Intervals
{
	public static class IntervalExtensions
	{
		public static bool IsEmpty<T>(this IInterval<T> interval) where T : IEquatable<T>, IComparable<T> =>
			interval.Left.Value.CompareTo(interval.Right.Value) is var compareTo &&
			interval.Left.Inclusion == Inclusion.Included && interval.Right.Inclusion == Inclusion.Included
				? compareTo > 0
				: compareTo >= 0;
	}
}