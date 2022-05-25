using System;

namespace Intervals.Intervals
{
	public interface IPoint<T> : IEquatable<IPoint<T>> where T : IEquatable<T>
	{
		T Value { get; }

		Inclusion Inclusion { get; }
	}
}