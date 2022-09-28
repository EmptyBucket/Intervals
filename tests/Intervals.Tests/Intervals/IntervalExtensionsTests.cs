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

namespace Intervals.Tests.Intervals;

public partial class IntervalExtensionsTests
{
	[Test]
	public void IsOverlap_WhenIntervalsNotIntersect_ReturnFalse()
	{
		var first = new Interval<int>(0, 1, IntervalInclusion.Closed);
		var second = new Interval<int>(2, 3, IntervalInclusion.Closed);

		var actual1 = first.IsOverlap(second);
		var actual2 = second.IsOverlap(first);

		actual1.Should().BeFalse();
		actual2.Should().BeFalse();
	}

	[Test]
	public void IsOverlap_WhenOpenedIntervalsHaveSameEndpointWithDifferentLocation_ReturnFalse()
	{
		var first = new Interval<int>(0, 1, IntervalInclusion.Opened);
		var second = new Interval<int>(1, 2, IntervalInclusion.Opened);

		var actual1 = first.IsOverlap(second);
		var actual2 = second.IsOverlap(first);

		actual1.Should().BeFalse();
		actual2.Should().BeFalse();
	}

	[Test]
	public void IsOverlap_WhenClosedIntervalsHaveSameEndpointWithDifferentLocation_ReturnTrue()
	{
		var first = new Interval<int>(0, 1, IntervalInclusion.Closed);
		var second = new Interval<int>(1, 2, IntervalInclusion.Closed);

		var actual1 = first.IsOverlap(second);
		var actual2 = second.IsOverlap(first);

		actual1.Should().BeTrue();
		actual2.Should().BeTrue();
	}

	[Test]
	public void IsOverlap_WhenIntervalsIntersect_ReturnTrue()
	{
		var first = new Interval<int>(0, 2, IntervalInclusion.Closed);
		var second = new Interval<int>(1, 3, IntervalInclusion.Closed);

		var actual1 = first.IsOverlap(second);
		var actual2 = second.IsOverlap(first);

		actual1.Should().BeTrue();
		actual2.Should().BeTrue();
	}

	[Test]
	public void IsInclude_WhenIntervalsNotIntersect_ReturnFalse()
	{
		var first = new Interval<int>(0, 1, IntervalInclusion.Closed);
		var second = new Interval<int>(2, 3, IntervalInclusion.Closed);

		var actual = first.IsInclude(second);

		actual.Should().BeFalse();
	}

	[Test]
	public void IsInclude_WhenIntervalsIntersectButNotContain_ReturnFalse()
	{
		var first = new Interval<int>(0, 2, IntervalInclusion.Closed);
		var second = new Interval<int>(1, 3, IntervalInclusion.Closed);

		var actual = first.IsInclude(second);

		actual.Should().BeFalse();
	}

	[Test]
	[TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
	[TestCase(IntervalInclusion.Opened, IntervalInclusion.LeftOpened)]
	[TestCase(IntervalInclusion.Opened, IntervalInclusion.RightOpened)]
	[TestCase(IntervalInclusion.LeftOpened, IntervalInclusion.Closed)]
	[TestCase(IntervalInclusion.LeftOpened, IntervalInclusion.RightOpened)]
	[TestCase(IntervalInclusion.RightOpened, IntervalInclusion.Closed)]
	[TestCase(IntervalInclusion.RightOpened, IntervalInclusion.LeftOpened)]
	public void IsInclude_WhenNotContainEndpoints_ReturnFalse(IntervalInclusion outerInclusion, IntervalInclusion innerInclusion)
	{
		var outer = new Interval<int>(0, 1, outerInclusion);
		var inner = new Interval<int>(0, 1, innerInclusion);

		var actual = outer.IsInclude(inner);

		actual.Should().BeFalse();
	}

	[Test]
	[TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
	[TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
	[TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
	[TestCase(IntervalInclusion.Closed, IntervalInclusion.LeftOpened)]
	[TestCase(IntervalInclusion.Closed, IntervalInclusion.RightOpened)]
	[TestCase(IntervalInclusion.LeftOpened, IntervalInclusion.Opened)]
	[TestCase(IntervalInclusion.LeftOpened, IntervalInclusion.LeftOpened)]
	[TestCase(IntervalInclusion.RightOpened, IntervalInclusion.Opened)]
	[TestCase(IntervalInclusion.RightOpened, IntervalInclusion.RightOpened)]
	public void IsInclude_WhenContainEndpoints_ReturnTrue(IntervalInclusion outerInclusion, IntervalInclusion innerInclusion)
	{
		var outer = new Interval<int>(0, 1, outerInclusion);
		var inner = new Interval<int>(0, 1, innerInclusion);

		var actual = outer.IsInclude(inner);

		actual.Should().BeTrue();
	}

	[Test]
	public void IsInclude_WhenOuterIsBigger_ReturnTrue()
	{
		var outer = new Interval<int>(0, 5, IntervalInclusion.Closed);
		var inner = new Interval<int>(1, 2, IntervalInclusion.Closed);

		var actual = outer.IsInclude(inner);

		actual.Should().BeTrue();
	}
}