using FluentAssertions;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Points;

public partial class EndpointTests
{
    [Test]
    public void Left__ReturnLeftEndpoint()
    {
        var point = Point.Excluded(0);

        var endpoint = Endpoint.Left(point);

        ((Point<int>)endpoint).Should().Be(point);
        endpoint.Location.Should().Be(EndpointLocation.Left);
    }

    [Test]
    public void Right__ReturnRightEndpoint()
    {
        var point = Point.Excluded(0);

        var endpoint = Endpoint.Right(point);

        ((Point<int>)endpoint).Should().Be(point);
        endpoint.Location.Should().Be(EndpointLocation.Right);
    }

    [Test]
    public void CompareTo_WhenHasLessValue_NegativeOne()
    {
        var first = new Endpoint<int>(Point.Included(-1), EndpointLocation.Left);
        var second = new Endpoint<int>(Point.Included(0), EndpointLocation.Left);

        var actual = first.CompareTo(second);

        actual.Should().Be(-1);
    }

    [TestCaseSource(nameof(CompareTo_WhenHasSameValue_Data))]
    public void CompareTo_WhenHasSameValue(Endpoint<int> first, Endpoint<int> second, int result)
    {
        var actual = first.CompareTo(second);

        actual.Should().Be(result);
    }

    [Test]
    public void CompareTo_WhenHasMoreValue_PositiveOne()
    {
        var first = new Endpoint<int>(Point.Included(1), EndpointLocation.Left);
        var second = new Endpoint<int>(Point.Included(0), EndpointLocation.Left);

        var actual = first.CompareTo(second);

        actual.Should().Be(1);
    }
}