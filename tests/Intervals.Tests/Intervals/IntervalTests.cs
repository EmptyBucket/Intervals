using FluentAssertions;
using Intervals.Intervals;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public class IntervalTests
{
    [Test]
    public void Equals_WhenHasEqualEndpoints_ReturnTrue()
    {
        var first = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));
        var second = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));

        var actual = first.Equals(second);

        actual.Should().BeTrue();
    }

    [Test]
    public void Equals_WhenHasNotEqualEndpoints_ReturnFalse()
    {
        var first = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));
        var second = new Interval<int>(Endpoint.Left(Point.Included(1)), Endpoint.Right(Point.Included(1)));

        var actual = first.Equals(second);

        actual.Should().BeFalse();
    }

    [Test]
    public void CompareTo_WhenLeftLess_ReturnLessZero()
    {
        var first = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));
        var second = new Interval<int>(Endpoint.Left(Point.Included(1)), Endpoint.Right(Point.Included(1)));

        var actual = first.CompareTo(second);

        actual.Should().Be(-1);
    }

    [Test]
    public void CompareTo_WhenLeftMore_ReturnMoreZero()
    {
        var first = new Interval<int>(Endpoint.Left(Point.Included(1)), Endpoint.Right(Point.Included(1)));
        var second = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));

        var actual = first.CompareTo(second);

        actual.Should().Be(1);
    }

    [Test]
    public void CompareTo_WhenLeftEqualsAndRightLess_ReturnLessZero()
    {
        var first = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));
        var second = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(1)));

        var actual = first.CompareTo(second);

        actual.Should().Be(-1);
    }

    [Test]
    public void CompareTo_WhenLeftEqualsAndRightMore_ReturnMoreZero()
    {
        var first = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(1)));
        var second = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));

        var actual = first.CompareTo(second);

        actual.Should().Be(1);
    }

    [Test]
    public void CompareTo_WhenLeftAndRightEquals_ReturnZero()
    {
        var first = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));
        var second = new Interval<int>(Endpoint.Left(Point.Included(0)), Endpoint.Right(Point.Included(0)));

        var actual = first.CompareTo(second);

        actual.Should().Be(0);
    }
}