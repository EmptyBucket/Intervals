using FluentAssertions;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Points;

public class PointExtensionsTests
{
    [Test]
    public void HasGap_WhenValuesNotEqual_ReturnTrue()
    {
        var first = Point.Excluded(0);
        var second = Point.Excluded(1);

        var hasGap = first.HasGap(second);

        hasGap.Should().BeTrue();
    }

    [Test]
    public void HasGap_WhenValuesEqualAndBothExcluded_ReturnTrue()
    {
        var first = Point.Excluded(0);
        var second = Point.Excluded(0);

        var hasGap = first.HasGap(second);

        hasGap.Should().BeTrue();
    }

    [Test]
    public void HasGap_WhenValuesEqualAndOneIncluded_ReturnFalse()
    {
        var first = Point.Included(0);
        var second = Point.Excluded(0);

        var hasGap = first.HasGap(second);

        hasGap.Should().BeFalse();
    }

    [Test]
    public void HasGap_WhenValuesEqualAndBothIncluded_ReturnFalse()
    {
        var first = Point.Included(0);
        var second = Point.Included(0);

        var hasGap = first.HasGap(second);

        hasGap.Should().BeFalse();
    }
}