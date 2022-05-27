using FluentAssertions;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Points;

public class PointTests
{
    [Test]
    public void Included__ReturnIncludedPoint()
    {
        const int value = 0;

        var actual = Point.Included(value);

        actual.Value.Should().Be(value);
        actual.Inclusion.Should().Be(Inclusion.Included);
    }

    [Test]
    public void Excluded__ReturnExcludedPoint()
    {
        const int value = 0;

        var actual = Point.Excluded(value);

        actual.Value.Should().Be(value);
        actual.Inclusion.Should().Be(Inclusion.Excluded);
    }
}