using FluentAssertions;
using Intervals.GranularIntervals;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class TimeGranularIntervalTests
{
    [Test]
    public void Convert_WhenOpenedToOpened_ReturnOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Opened);

        var convertedInterval = interval.Convert(IntervalInclusion.Opened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Opened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenOpenedToLeftOpened_ReturnLeftOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Opened);

        var convertedInterval = interval.Convert(IntervalInclusion.LeftOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.LeftOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }

    [Test]
    public void Convert_WhenOpenedToRightOpened_ReturnRightOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Opened);

        var convertedInterval = interval.Convert(IntervalInclusion.RightOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.RightOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenOpenedToClosed_ReturnClosedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Opened);

        var convertedInterval = interval.Convert(IntervalInclusion.Closed);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Closed);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }

    [Test]
    public void Convert_WhenLeftOpenedToOpened_ReturnOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.LeftOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.Opened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Opened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenLeftOpenedToLeftOpened_ReturnLeftOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.LeftOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.LeftOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.LeftOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }

    [Test]
    public void Convert_WhenLeftOpenedToRightOpened_ReturnRightOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.LeftOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.RightOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.RightOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenLeftOpenedToClosed_ReturnClosedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.LeftOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.Closed);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Closed);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }

    [Test]
    public void Convert_WhenRightOpenedToOpened_ReturnOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.RightOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.Opened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Opened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenRightOpenedToLeftOpened_ReturnLeftOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.RightOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.LeftOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.LeftOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }

    [Test]
    public void Convert_WhenRightOpenedToRightOpened_ReturnRightOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.RightOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.RightOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.RightOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenRightOpenedToClosed_ReturnClosedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 12);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.RightOpened);

        var convertedInterval = interval.Convert(IntervalInclusion.Closed);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Closed);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }

    [Test]
    public void Convert_WhenClosedToOpened_ReturnOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Closed);

        var convertedInterval = interval.Convert(IntervalInclusion.Opened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Opened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenClosedToLeftOpened_ReturnLeftOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Closed);

        var convertedInterval = interval.Convert(IntervalInclusion.LeftOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.LeftOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 1));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }

    [Test]
    public void Convert_WhenClosedToRightOpened_ReturnRightOpenedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Closed);

        var convertedInterval = interval.Convert(IntervalInclusion.RightOpened);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.RightOpened);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 12));
    }

    [Test]
    public void Convert_WhenClosedToClosed_ReturnClosedWithSameLength()
    {
        var leftValue = new DateTime(2022, 1, 2);
        var rightValue = new DateTime(2022, 1, 11);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.Closed);

        var convertedInterval = interval.Convert(IntervalInclusion.Closed);

        convertedInterval.Inclusion.Should().Be(IntervalInclusion.Closed);
        convertedInterval.Length.Should().Be(interval.Length);
        convertedInterval.LeftValue.Should().Be(new DateTime(2022, 1, 2));
        convertedInterval.RightValue.Should().Be(new DateTime(2022, 1, 11));
    }
}