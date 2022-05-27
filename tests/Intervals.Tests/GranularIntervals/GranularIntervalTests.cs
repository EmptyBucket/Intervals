using FluentAssertions;
using Intervals.GranularIntervals;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public class GranularIntervalTests
{
    [Test]
    public void GetNext__ReturnNext()
    {
        var left = Point.Excluded(new DateTime(2021, 1, 1, 1, 1, 1));
        var right = Point.Excluded(new DateTime(2022, 1, 4, 5, 6, 7));
        var fooInterval = new GranularInterval(left, right);

        var actual = fooInterval.GetNext();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.Right.Value.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void GetPrev__ReturnPrev()
    {
        var left = Point.Excluded(new DateTime(2022, 1, 4, 5, 6, 7));
        var right = Point.Excluded(new DateTime(2023, 1, 7, 9, 11, 13));
        var fooInterval = new GranularInterval(left, right);

        var actual = fooInterval.GetPrev();

        actual.Left.Value.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
    }
}