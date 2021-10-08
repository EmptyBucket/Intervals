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
using System.Collections;
using System.Collections.Generic;

namespace Intervals.Intervals
{
	public static class Interval
	{
		public static IInterval<T> New<T>(IPoint<T> left, IPoint<T> right)
			where T : IEquatable<T>, IComparable<T> => new Interval<T>(left, right);

		public static IInterval<T> New<T>(T leftValue, T rightValue, IntervalType intervalType)
			where T : IEquatable<T>, IComparable<T> => new Interval<T>(leftValue, rightValue, intervalType);
	}

	public class Interval<T> : IInterval<T> where T : IComparable<T>, IEquatable<T>
	{
		protected internal Interval(IPoint<T> left, IPoint<T> right)
		{
			Left = Endpoint.New(left, EndLocation.Left);
			Right = Endpoint.New(right, EndLocation.Right);
			IntervalType = IntervalTypeConvert.FromInclusions(left.Inclusion, right.Inclusion);
		}

		protected internal Interval(T leftValue, T rightValue, IntervalType intervalType)
		{
			var (leftInclusion, rightInclusion) = IntervalTypeConvert.ToInclusions(intervalType);
			Left = Endpoint.New(Point.New(leftValue, leftInclusion), EndLocation.Left);
			Right = Endpoint.New(Point.New(rightValue, rightInclusion), EndLocation.Right);
			IntervalType = intervalType;
		}

		public virtual IEndpoint<T> Left { get; }

		public virtual IEndpoint<T> Right { get; }

		public IntervalType IntervalType { get; }

		public bool Equals(IInterval<T> other) => Left.Equals(other.Left) && Right.Equals(other.Right);

		public int CompareTo(IInterval<T> other) => Left.CompareTo(other.Left) is var leftCompareTo && leftCompareTo != 0
			? leftCompareTo
			: Right.CompareTo(other.Right);

		public override int GetHashCode() => HashCode.Combine(Left, Right);

		public IEnumerator<IInterval<T>> GetEnumerator()
		{
			yield return this;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public override string ToString() => $"{Left}, {Right}";
	}
}