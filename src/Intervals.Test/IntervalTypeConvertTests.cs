using FluentAssertions;
using NUnit.Framework;
using PeriodNet.Intervals;

namespace PeriodNet.Test
{
	public class IntervalTypeConvertTests
	{
		[TestCase(Inclusion.Excluded, Inclusion.Excluded, IntervalType.Open)]
		[TestCase(Inclusion.Excluded, Inclusion.Included, IntervalType.LeftOpen)]
		[TestCase(Inclusion.Included, Inclusion.Excluded, IntervalType.RightOpen)]
		[TestCase(Inclusion.Included, Inclusion.Included, IntervalType.Closed)]
		public void FromInclusion_WhenGivenInclusions_ReturnIntervalType(Inclusion left, Inclusion right, IntervalType result)
		{
			var actual = IntervalTypeConvert.FromInclusions(left, right);

			actual.Should().Be(result);
		}

		[TestCase(IntervalType.Open, Inclusion.Excluded, Inclusion.Excluded)]
		[TestCase(IntervalType.LeftOpen, Inclusion.Excluded, Inclusion.Included)]
		[TestCase(IntervalType.RightOpen, Inclusion.Included, Inclusion.Excluded)]
		[TestCase(IntervalType.Closed, Inclusion.Included, Inclusion.Included)]
		public void ToInclusions_WhenGivenIntervalType_ReturnInclusions(IntervalType intervalType,
			Inclusion left, Inclusion right)
		{
			var (actualLeft, actualRight) = IntervalTypeConvert.ToInclusions(intervalType);

			actualLeft.Should().Be(left);
			actualRight.Should().Be(right);
		}
	}
}