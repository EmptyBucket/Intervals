using System;
using Intervals.Intervals;

namespace Intervals.GranularIntervals
{
	public interface IGranularInterval<T> : IInterval<T>
		where T : IComparable<T>, IEquatable<T>
	{
		IGranularInterval<T> GetPrev();

		IGranularInterval<T> GetNext();
	}

	public interface IGranularInterval<T, out TInterval> : IGranularInterval<T> 
		where T : IComparable<T>, IEquatable<T>
		where TInterval : IGranularInterval<T>
	{
		new TInterval GetPrev();

		IGranularInterval<T> IGranularInterval<T>.GetPrev() => GetPrev();

		new TInterval GetNext();

		IGranularInterval<T> IGranularInterval<T>.GetNext() => GetNext();
	}
}