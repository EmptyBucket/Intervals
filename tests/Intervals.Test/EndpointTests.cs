using FluentAssertions;
using Intervals.Intervals;
using Intervals.Points;
using Moq;
using NUnit.Framework;

namespace Intervals.Test;

public class EndpointTests
{
	[Test]
	public void Equals_WhenHasSameMembers_ReturnTrue()
	{
		var first = new Endpoint<int>(new Point<int>(), EndpointLocation.Left);
		var second = new Endpoint<int>(new Point<int>(), EndpointLocation.Left);

		var actual = first.Equals(second);

		actual.Should().BeTrue();
	}

	[Test]
	public void Equals_WhenHasOtherLocation_ReturnFalse()
	{
		var first = new Endpoint<int>(new Point<int>(), EndpointLocation.Left);
		var second = new Endpoint<int>(new Point<int>(), EndpointLocation.Right);

		var actual = first.Equals(second);

		actual.Should().BeFalse();
	}

	[Test]
	public void Equals_WhenHasOtherPoint_ReturnFalse()
	{
		var first = new Endpoint<int>(Point.Included(0), EndpointLocation.Left);
		var second = new Endpoint<int>(Point.Included(1), EndpointLocation.Left);

		var actual = first.Equals(second);

		actual.Should().BeFalse();
	}

	[Test]
	public void CompareTo_WhenHasLessValue_ReturnResult()
	{
		var first = new Endpoint<int>(Point.Included(-1), EndpointLocation.Left);
		var second = new Endpoint<int>(Point.Included(0), EndpointLocation.Left);

		var actual = first.CompareTo(second);

		actual.Should().Be(-1);
	}

	[TestCaseSource(typeof(EndpointCompareToSameValueData))]
	public void CompareTo_WhenHasSameValue(Endpoint<int> first, Endpoint<int> second, int result)
	{
		var actual = first.CompareTo(second);

		actual.Should().Be(result);
	}

	[Test]
	public void CompareTo_WhenHasMoreValue_ReturnResult()
	{
		var first = new Endpoint<int>(Point.Included(1), EndpointLocation.Left);
		var second = new Endpoint<int>(Point.Included(0), EndpointLocation.Left);

		var actual = first.CompareTo(second);

		actual.Should().Be(1);
	}
}