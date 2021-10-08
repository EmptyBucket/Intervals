// MIT License
// 
// Copyright (c) 2021 Alexey Politov, Yevgeny Khoroshavin
// https://github.com/EmptyBucket/Intervals
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;

namespace Intervals.Intervals
{
	public static class Endpoint
	{
		public static IEndpoint<T> New<T>(IPoint<T> point, EndLocation location)
			where T : IEquatable<T>, IComparable<T> => new Endpoint<T>(point, location);
	}

	public readonly struct Endpoint<T> : IEndpoint<T> where T : IComparable<T>, IEquatable<T>
	{
		private readonly IPoint<T> _point;

		internal Endpoint(IPoint<T> point, EndLocation location) => (_point, Location) = (point, location);

		public T Value => _point.Value;

		public Inclusion Inclusion => _point.Inclusion;

		public EndLocation Location { get; }

		public int CompareTo(IEnd<T> other) =>
			other is IEndpoint<T> otherEndpoint ? CompareTo(otherEndpoint) : (int)other.Location * 2 - 1;

		public int CompareTo(IEndpoint<T> other)
		{
			var valueCompared = Value.CompareTo(other.Value);

			if (valueCompared != 0) return valueCompared;

			var locationCompared = Location - other.Location;
			var inclusionCompared = Inclusion - other.Inclusion;

			//flags
			var byLocation = locationCompared & 1;
			var byInclusion = byLocation ^ 1;
			var bothIsIncluded = (int)(Inclusion & other.Inclusion);
			var thisIsRight = (int)Location;
			int ToSign(int signBit) => -signBit | 1;

			return byLocation * locationCompared * ToSign(bothIsIncluded) |
			       byInclusion * inclusionCompared * ToSign(thisIsRight);
		}

		public bool Equals(IPoint<T> other) =>
			other is IEndpoint<T> otherEndpoint ? Equals(otherEndpoint) : _point.Equals(other);

		public bool Equals(IEndpoint<T> other) => _point.Equals(other) && Location == other.Location;

		public override bool Equals(object? obj) => obj is IEndpoint<T> other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(Value, Inclusion, Location);

		public override string ToString() => Location switch
		{
			EndLocation.Left => Inclusion switch
			{
				Inclusion.Excluded => $"({Value}",
				Inclusion.Included => $"[{Value}",
				_ => throw new ArgumentOutOfRangeException()
			},
			EndLocation.Right => Inclusion switch
			{
				Inclusion.Excluded => $"{Value})",
				Inclusion.Included => $"{Value}]",
				_ => throw new ArgumentOutOfRangeException()
			},
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}