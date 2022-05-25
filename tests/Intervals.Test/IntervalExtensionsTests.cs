using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test;

public class IntervalExtensionsTests
{
	[Test]
	[TestCase(IntervalInclusion.Opened)]
	[TestCase(IntervalInclusion.Closed)]
	[TestCase(IntervalInclusion.LeftOpened)]
	[TestCase(IntervalInclusion.RightOpened)]
	public void IsEmpty_WhenLeftLessThanRight_ReturnTrue(IntervalInclusion inclusion)
	{
		var interval = (IInterval<int>)new Interval<int>(0, -1, inclusion);

		var actual = interval.IsEmpty();

		actual.Should().BeTrue();
	}

	[Test]
	public void IsEmpty_WhenClosedIntervalWithEqualEndpoints_ReturnFalse()
	{
		var interval = (IInterval<int>)new Interval<int>(0, 0, IntervalInclusion.Closed);

		var actual = interval.IsEmpty();

		actual.Should().BeFalse();
	}

	[Test]
	[TestCase(IntervalInclusion.Opened)]
	[TestCase(IntervalInclusion.LeftOpened)]
	[TestCase(IntervalInclusion.RightOpened)]
	public void IsEmpty_WhenNonClosedIntervalWithEqualEndpoints_ReturnTrue(IntervalInclusion inclusion)
	{
		var interval = (IInterval<int>)new Interval<int>(0, 0, inclusion);

		var actual = interval.IsEmpty();

		actual.Should().BeTrue();
	}

	[Test]
	[TestCase(IntervalInclusion.Opened)]
	[TestCase(IntervalInclusion.Closed)]
	[TestCase(IntervalInclusion.LeftOpened)]
	[TestCase(IntervalInclusion.RightOpened)]
	public void IsEmpty_WhenIntervalWithNonEqualEndpoints_ReturnFalse(IntervalInclusion inclusion)
	{
		var interval = (IInterval<int>)new Interval<int>(0, 1, inclusion);

		var actual = interval.IsEmpty();

		actual.Should().BeFalse();
	}

	[Test]
	public void IsOverlap_WhenIntervalsNotIntersect_ReturnFalse()
	{
		var left = (IInterval<int>)new Interval<int>(0, 1, IntervalInclusion.Closed);
		var right = (IInterval<int>)new Interval<int>(2, 3, IntervalInclusion.Closed);

		var actual1 = left.IsOverlap(right);
		var actual2 = right.IsOverlap(left);

		actual1.Should().BeFalse();
		actual2.Should().BeFalse();
	}

	[Test]
	public void IsOverlap_WhenOpenedIntervalsHaveSameEndpointWithDifferentLocation_ReturnFalse()
	{
		var left = (IInterval<int>)new Interval<int>(0, 1, IntervalInclusion.Opened);
		var right = (IInterval<int>)new Interval<int>(1, 2, IntervalInclusion.Opened);

		var actual1 = left.IsOverlap(right);
		var actual2 = right.IsOverlap(left);

		actual1.Should().BeFalse();
		actual2.Should().BeFalse();
	}

	[Test]
	public void IsOverlap_WhenClosedIntervalsHaveSameEndpointWithDifferentLocation_ReturnTrue()
	{
		var left = (IInterval<int>)new Interval<int>(0, 1, IntervalInclusion.Closed);
		var right = (IInterval<int>)new Interval<int>(1, 2, IntervalInclusion.Closed);

		var actual1 = left.IsOverlap(right);
		var actual2 = right.IsOverlap(left);

		actual1.Should().BeTrue();
		actual2.Should().BeTrue();
	}

	[Test]
	public void IsOverlap_WhenIntervalsIntersect_ReturnTrue()
	{
		var left = (IInterval<int>)new Interval<int>(0, 2, IntervalInclusion.Closed);
		var right = (IInterval<int>)new Interval<int>(1, 3, IntervalInclusion.Closed);

		var actual1 = left.IsOverlap(right);
		var actual2 = right.IsOverlap(left);

		actual1.Should().BeTrue();
		actual2.Should().BeTrue();
	}

	[Test]
	public void IsInclude_WhenIntervalsNotIntersect_ReturnFalse()
	{
		var left = (IInterval<int>)new Interval<int>(0, 1, IntervalInclusion.Closed);
		var right = (IInterval<int>)new Interval<int>(2, 3, IntervalInclusion.Closed);

		var actual = left.IsInclude(right);

		actual.Should().BeFalse();
	}

	[Test]
	public void IsInclude_WhenIntervalsIntersectButNotContain_ReturnFalse()
	{
		var left = (IInterval<int>)new Interval<int>(0, 2, IntervalInclusion.Closed);
		var right = (IInterval<int>)new Interval<int>(1, 3, IntervalInclusion.Closed);

		var actual = left.IsInclude(right);

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
		var outer = (IInterval<int>)new Interval<int>(0, 1, outerInclusion);
		var inner = (IInterval<int>)new Interval<int>(0, 1, innerInclusion);

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
		var outer = (IInterval<int>)new Interval<int>(0, 1, outerInclusion);
		var inner = (IInterval<int>)new Interval<int>(0, 1, innerInclusion);

		var actual = outer.IsInclude(inner);

		actual.Should().BeTrue();
	}

	[Test]
	public void IsInclude_WhenOuterIsBigger_ReturnTrue()
	{
		var outer = (IInterval<int>)new Interval<int>(0, 5, IntervalInclusion.Closed);
		var inner = (IInterval<int>)new Interval<int>(1, 2, IntervalInclusion.Closed);

		var actual = outer.IsInclude(inner);

		actual.Should().BeTrue();
	}
}