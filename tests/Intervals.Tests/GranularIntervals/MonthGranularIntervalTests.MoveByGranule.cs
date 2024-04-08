using FluentAssertions;
using Intervals.GranularIntervals;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class MonthGranularIntervalTests
{
    [Test]
    public void MoveByGranule_WhenOne_ReturnNext()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1));

        var actual = interval.MoveByGranule(1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 2, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 2, 2, 3, 4));
    }

    [Test]
    public void MoveByGranule_WhenNegativeOne_ReturnPrev()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1));

        var actual = interval.MoveByGranule(-1);

        actual.LeftValue.Should().Be(new DateTime(2021, 12, 31, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2022, 12, 31, 2, 3, 4));
    }

    [Test]
    public void MoveByGranule_WhenNegativeOneAndZero_ReturnIntervalWithLeftAddition()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1));

        var actual = interval.MoveByGranule(-1, 0);

        actual.LeftValue.Should().Be(new DateTime(2021, 12, 31, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 1, 2, 3, 4));
    }

    [Test]
    public void MoveByGranule_WhenOneAndZero_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1));

        var actual = interval.MoveByGranule(1, 0);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 2, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 1, 2, 3, 4));
    }

    [Test]
    public void MoveByGranule_WhenZeroAndNegativeOne_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1));

        var actual = interval.MoveByGranule(0, -1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2022, 12, 31, 2, 3, 4));
    }

    [Test]
    public void MoveByGranule_WhenZeroAndOne_ReturnIntervalWithRightAddition()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1));

        var actual = interval.MoveByGranule(0, 1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 2, 2, 3, 4));
    }
}