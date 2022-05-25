using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test;

public class PointTests
{
	[TestCase(-1)]
	[TestCase(0)]
	[TestCase(1)]
	public void New_WhenGivenValue_ReturnPointWithValue(int value)
	{
		var actual = Point.New(value, 0);

		actual.Value.Should().Be(value);
	}

	[TestCase(Inclusion.Included)]
	[TestCase(Inclusion.Excluded)]
	public void New_WhenGivenInclusion_ReturnPointWithInclusion(Inclusion inclusion)
	{
		var actual = Point.New(0, inclusion);

		actual.Inclusion.Should().Be(inclusion);
	}

	[Test]
	public void Included__ReturnIncludedPoint()
	{
		var actual = Point.Included(0);

		actual.Inclusion.Should().Be(Inclusion.Included);
	}

	[Test]
	public void Excluded__ReturnExcludedPoint()
	{
		var actual = Point.Excluded(0);

		actual.Inclusion.Should().Be(Inclusion.Excluded);
	}

	[Test]
	public void Equals_WhenHasSameMembers_ReturnTrue()
	{
		var first = Point.New(0, Inclusion.Excluded);
		var second = Point.New(0, Inclusion.Excluded);

		var actual = first.Equals(second);

		actual.Should().BeTrue();
	}

	[Test]
	public void Equals_WhenHasOtherValue_ReturnFalse()
	{
		var first = Point.New(0, Inclusion.Excluded);
		var second = Point.New(1, Inclusion.Excluded);

		var actual = first.Equals(second);

		actual.Should().BeFalse();
	}

	[Test]
	public void Equals_WhenHasOtherInclusion_ReturnFalse()
	{
		var first = Point.New(0, Inclusion.Excluded);
		var second = Point.New(0, Inclusion.Included);

		var actual = first.Equals(second);

		actual.Should().BeFalse();
	}
}