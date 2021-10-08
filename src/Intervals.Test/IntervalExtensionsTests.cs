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

using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test
{
	public class IntervalExtensionsTests
	{
		[Test]
		[TestCase(IntervalType.Open)]
		[TestCase(IntervalType.Closed)]
		[TestCase(IntervalType.LeftOpen)]
		[TestCase(IntervalType.RightOpen)]
		public void IsEmpty_WhenLeftLessThanRight_ReturnTrue(IntervalType type)
		{
			var interval = Interval.New(0, -1, type);

			var actual = interval.IsEmpty();

			actual.Should().BeTrue();
		}

		[Test]
		public void IsEmpty_WhenClosedIntervalWithEqualEndpoints_ReturnFalse()
		{
			var interval = Interval.New(0, 0, IntervalType.Closed);

			var actual = interval.IsEmpty();

			actual.Should().BeFalse();
		}

		[Test]
		[TestCase(IntervalType.Open)]
		[TestCase(IntervalType.LeftOpen)]
		[TestCase(IntervalType.RightOpen)]
		public void IsEmpty_WhenNonClosedIntervalWithEqualEndpoints_ReturnTrue(IntervalType type)
		{
			var interval = Interval.New(0, 0, type);

			var actual = interval.IsEmpty();

			actual.Should().BeTrue();
		}

		[Test]
		[TestCase(IntervalType.Open)]
		[TestCase(IntervalType.Closed)]
		[TestCase(IntervalType.LeftOpen)]
		[TestCase(IntervalType.RightOpen)]
		public void IsEmpty_WhenIntervalWithNonEqualEndpoints_ReturnFalse(IntervalType type)
		{
			var interval = Interval.New(0, 1, type);

			var actual = interval.IsEmpty();

			actual.Should().BeFalse();
		}

		[Test]
		public void IsOverlap_WhenIntervalsNotIntersect_ReturnFalse()
		{
			var left = Interval.New(0, 1, IntervalType.Closed);
			var right = Interval.New(2, 3, IntervalType.Closed);

			var actual1 = left.IsOverlap(right);
			var actual2 = right.IsOverlap(left);

			actual1.Should().BeFalse();
			actual2.Should().BeFalse();
		}

		[Test]
		public void IsOverlap_WhenOpenIntervalsHaveSameEndpointWithDifferentLocation_ReturnFalse()
		{
			var left = Interval.New(0, 1, IntervalType.Open);
			var right = Interval.New(1, 2, IntervalType.Open);

			var actual1 = left.IsOverlap(right);
			var actual2 = right.IsOverlap(left);

			actual1.Should().BeFalse();
			actual2.Should().BeFalse();
		}

		[Test]
		public void IsOverlap_WhenClosedIntervalsHaveSameEndpointWithDifferentLocation_ReturnTrue()
		{
			var left = Interval.New(0, 1, IntervalType.Closed);
			var right = Interval.New(1, 2, IntervalType.Closed);

			var actual1 = left.IsOverlap(right);
			var actual2 = right.IsOverlap(left);

			actual1.Should().BeTrue();
			actual2.Should().BeTrue();
		}

		[Test]
		public void IsOverlap_WhenIntervalsIntersect_ReturnTrue()
		{
			var left = Interval.New(0, 2, IntervalType.Closed);
			var right = Interval.New(1, 3, IntervalType.Closed);

			var actual1 = left.IsOverlap(right);
			var actual2 = right.IsOverlap(left);

			actual1.Should().BeTrue();
			actual2.Should().BeTrue();
		}

		[Test]
		public void IsInclude_WhenIntervalsNotIntersect_ReturnFalse()
		{
			var left = Interval.New(0, 1, IntervalType.Closed);
			var right = Interval.New(2, 3, IntervalType.Closed);

			var actual = left.IsInclude(right);

			actual.Should().BeFalse();
		}

		[Test]
		public void IsInclude_WhenIntervalsIntersectButNotContain_ReturnFalse()
		{
			var left = Interval.New(0, 2, IntervalType.Closed);
			var right = Interval.New(1, 3, IntervalType.Closed);

			var actual = left.IsInclude(right);

			actual.Should().BeFalse();
		}

		[Test]
		[TestCase(IntervalType.Open, IntervalType.Closed)]
		[TestCase(IntervalType.Open, IntervalType.LeftOpen)]
		[TestCase(IntervalType.Open, IntervalType.RightOpen)]
		[TestCase(IntervalType.LeftOpen, IntervalType.Closed)]
		[TestCase(IntervalType.LeftOpen, IntervalType.RightOpen)]
		[TestCase(IntervalType.RightOpen, IntervalType.Closed)]
		[TestCase(IntervalType.RightOpen, IntervalType.LeftOpen)]
		public void IsInclude_WhenNotContainEndpoints_ReturnFalse(IntervalType outerType, IntervalType innerType)
		{
			var outer = Interval.New(0, 1, outerType);
			var inner = Interval.New(0, 1, innerType);

			var actual = outer.IsInclude(inner);

			actual.Should().BeFalse();
		}

		[Test]
		[TestCase(IntervalType.Open, IntervalType.Open)]
		[TestCase(IntervalType.Closed, IntervalType.Open)]
		[TestCase(IntervalType.Closed, IntervalType.Closed)]
		[TestCase(IntervalType.Closed, IntervalType.LeftOpen)]
		[TestCase(IntervalType.Closed, IntervalType.RightOpen)]
		[TestCase(IntervalType.LeftOpen, IntervalType.Open)]
		[TestCase(IntervalType.LeftOpen, IntervalType.LeftOpen)]
		[TestCase(IntervalType.RightOpen, IntervalType.Open)]
		[TestCase(IntervalType.RightOpen, IntervalType.RightOpen)]
		public void IsInclude_WhenContainEndpoints_ReturnTrue(IntervalType outerType, IntervalType innerType)
		{
			var outer = Interval.New(0, 1, outerType);
			var inner = Interval.New(0, 1, innerType);

			var actual = outer.IsInclude(inner);

			actual.Should().BeTrue();
		}

		[Test]
		public void IsInclude_WhenOuterIsBigger_ReturnTrue()
		{
			var outer = Interval.New(0, 5, IntervalType.Closed);
			var inner = Interval.New(1, 2, IntervalType.Closed);

			var actual = outer.IsInclude(inner);

			actual.Should().BeTrue();
		}
	}
}