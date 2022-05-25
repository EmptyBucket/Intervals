using System;
using Intervals.Intervals;

namespace Intervals
{
	public static class ValueTupleExtensions
	{
		public static IInterval<T> ToInterval<T>(this (T Left, T Right) pair,
			IntervalInclusion inclusion = IntervalInclusion.RightOpened)
			where T : IComparable<T>, IEquatable<T> => Interval.New(pair.Left, pair.Right, inclusion);
	}
}