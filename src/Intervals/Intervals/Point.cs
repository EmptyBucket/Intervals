using System;
using System.Collections.Generic;

namespace PeriodNet.Intervals
{
	public static class Point
	{
		public static IPoint<T> New<T>(T value, Inclusion inclusion) where T : IEquatable<T> =>
			new Point<T>(value, inclusion);

		public static IPoint<T> Included<T>(T value) where T : IEquatable<T> => New(value, Inclusion.Included);

		public static IPoint<T> Excluded<T>(T value) where T : IEquatable<T> => New(value, Inclusion.Excluded);
	}

	public readonly struct Point<T> : IPoint<T> where T : IEquatable<T>
	{
		internal Point(T value, Inclusion inclusion) => (Value, Inclusion) = (value, inclusion);

		public T Value { get; }

		public Inclusion Inclusion { get; }

		public bool Equals(IPoint<T> other) =>
			EqualityComparer<T>.Default.Equals(Value, other.Value) && Inclusion == other.Inclusion;

		public override bool Equals(object? obj) => obj is IPoint<T> other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(Value, Inclusion);

		public override string ToString() => $"{Inclusion} {Value}";
	}
}