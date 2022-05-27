using FluentAssertions;
using Intervals.Intervals;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public class IntervalInclusionConvertTests
{
	[TestCase(Inclusion.Excluded, Inclusion.Excluded, IntervalInclusion.Opened)]
	[TestCase(Inclusion.Excluded, Inclusion.Included, IntervalInclusion.LeftOpened)]
	[TestCase(Inclusion.Included, Inclusion.Excluded, IntervalInclusion.RightOpened)]
	[TestCase(Inclusion.Included, Inclusion.Included, IntervalInclusion.Closed)]
	public void FromInclusion_WhenGivenInclusions_ReturnIntervalInclusion(Inclusion left, Inclusion right,
		IntervalInclusion result)
	{
		var actual = IntervalInclusionConvert.FromInclusions(left, right);

		actual.Should().Be(result);
	}

	[TestCase(IntervalInclusion.Opened, Inclusion.Excluded, Inclusion.Excluded)]
	[TestCase(IntervalInclusion.LeftOpened, Inclusion.Excluded, Inclusion.Included)]
	[TestCase(IntervalInclusion.RightOpened, Inclusion.Included, Inclusion.Excluded)]
	[TestCase(IntervalInclusion.Closed, Inclusion.Included, Inclusion.Included)]
	public void ToInclusions_WhenGivenIntervalInclusion_ReturnInclusions(IntervalInclusion intervalInclusion,
		Inclusion left, Inclusion right)
	{
		var (actualLeft, actualRight) = IntervalInclusionConvert.ToInclusions(intervalInclusion);

		actualLeft.Should().Be(left);
		actualRight.Should().Be(right);
	}
}