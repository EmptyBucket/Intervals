using System;
using PeriodNet.Intervals;

namespace PeriodNet
{
	public static class ValueTupleExtensions
	{
		public static IInterval<T> ToInterval<T>(this (T Left, T Right) pair, IntervalType type = IntervalType.RightOpen)
			where T : IComparable<T>, IEquatable<T> => Interval.New(pair.Left, pair.Right, type);
	}
}