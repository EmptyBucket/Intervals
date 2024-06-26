using FluentAssertions;
using Intervals.GranularIntervals;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class MonthlyIntervalTests
{
    [Test]
    public void Length_WhenOpened_ReturnGranulesCount()
    {
        var leftValue = new DateTime(2021, 12, 31);
        var rightValue = new DateTime(2022, 2, 1);
        var interval = new MonthlyInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.Opened);

        var length = interval.Length;

        length.Should().Be(TimeSpan.FromDays(31));
    }

    [Test]
    public void Length_WhenLeftOpened_ReturnGranulesCount()
    {
        var leftValue = new DateTime(2021, 12, 31);
        var rightValue = new DateTime(2022, 1, 31);
        var interval = new MonthlyInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.LeftOpened);

        var length = interval.Length;

        length.Should().Be(TimeSpan.FromDays(31));
    }

    [Test]
    public void Length_WhenRightOpened_ReturnGranulesCount()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 2, 1);
        var interval = new MonthlyInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.RightOpened);

        var length = interval.Length;

        length.Should().Be(TimeSpan.FromDays(31));
    }

    [Test]
    public void Length_WhenClosed_ReturnGranulesCount()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 31);
        var interval = new MonthlyInterval(leftValue, rightValue, TimeSpan.FromDays(1),
            IntervalInclusion.Closed);

        var length = interval.Length;

        length.Should().Be(TimeSpan.FromDays(31));
    }
}