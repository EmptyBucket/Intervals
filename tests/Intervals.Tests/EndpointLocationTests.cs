using FluentAssertions;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests;

public class EndpointLocationTests
{
	[Test]
	public void Invert_WhenLeft_ReturnRight()
	{
		const EndpointLocation endpointLocation = EndpointLocation.Left;

		var actual = endpointLocation.Invert();

		actual.Should().Be(EndpointLocation.Right);
	}

	[Test]
	public void Invert_WhenRight_ReturnLeft()
	{
		const EndpointLocation endpointLocation = EndpointLocation.Right;

		var actual = endpointLocation.Invert();

		actual.Should().Be(EndpointLocation.Left);
	}
}