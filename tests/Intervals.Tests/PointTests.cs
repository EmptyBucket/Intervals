using FluentAssertions;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests;

public class PointTests
{
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
        var first = new Point<int>(0, Inclusion.Excluded);
        var second = new Point<int>(0, Inclusion.Excluded);

        var actual = first.Equals(second);

        actual.Should().BeTrue();
    }

    [Test]
    public void Equals_WhenHasOtherValue_ReturnFalse()
    {
        var first = new Point<int>(0, Inclusion.Excluded);
        var second = new Point<int>(1, Inclusion.Excluded);

        var actual = first.Equals(second);

        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WhenHasOtherInclusion_ReturnFalse()
    {
        var first = new Point<int>(0, Inclusion.Excluded);
        var second = new Point<int>(0, Inclusion.Included);

        var actual = first.Equals(second);

        actual.Should().BeFalse();
    }
}