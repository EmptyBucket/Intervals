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
using System.Collections.Generic;

namespace Intervals.Intervals
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
		public Point(T value, Inclusion inclusion) => (Value, Inclusion) = (value, inclusion);

		public T Value { get; }

		public Inclusion Inclusion { get; }

		public bool Equals(IPoint<T> other) =>
			EqualityComparer<T>.Default.Equals(Value, other.Value) && Inclusion == other.Inclusion;

		public override bool Equals(object? obj) => obj is IPoint<T> other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(Value, Inclusion);

		public override string ToString() => $"{Inclusion} {Value}";
	}
}