using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class IntervalExtensionsTests
{
    [Test]
    public void GetLength_WhenLeftDateTimePointIsGreaterThanRight_ReturnsTimeSpanZero()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 3, 11, 0, 0), new DateTime(2022, 1, 2, 11, 0, 0));

        var actual = interval.GetDiff();

        actual.Should().Be(TimeSpan.Zero);
    }

    [Test]
    public void GetLength_WhenLeftDateTimePointIsEqualThanRight_ReturnsTimeSpanZero()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 3, 11, 0, 0), new DateTime(2022, 1, 3, 11, 0, 0));

        var actual = interval.GetDiff();

        actual.Should().Be(TimeSpan.Zero);
    }

    [Test]
    public void GetLength_WhenIntervalIsOneDay_ReturnsOneDayTimeSpan()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 1, 3, 11, 0, 0));

        var actual = interval.GetDiff();

        actual.Should().Be(TimeSpan.FromDays(1));
    }

    [Test]
    public void GetLength_WhenIntervalIs31Day_Returns31DaysTimeSpan()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var actual = interval.GetDiff();

        actual.Should().Be(TimeSpan.FromDays(31));
    }

    [Test]
    public void GetLength_WhenLeftIntPointIsGreaterThanRight_ReturnsZero()
    {
        var interval = new Interval<int>(10, 1);

        var actual = interval.GetDiff<int, int>();

        actual.Should().Be(0);
    }

    [Test]
    public void GetLength_WhenLeftIntPointIsEqualToRight_ReturnsZero()
    {
        var interval = new Interval<int>(10, 10);

        var actual = interval.GetDiff<int, int>();

        actual.Should().Be(0);
    }

    [Test]
    public void GetLength_WhenLeftIntPointIsLessThanRight_ReturnsActualResult()
    {
        var interval = new Interval<int>(10, 30);

        var actual = interval.GetDiff<int, int>();

        actual.Should().Be(20);
    }
}