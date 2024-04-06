using FluentAssertions;
using Intervals.GranularIntervals;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class MonthGranularIntervalTests
{
    [Test]
    public void MoveByLength_WhenOne_ReturnNext()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, 1);

        var actual = interval.MoveByLength(1);

        actual.LeftValue.Should().Be(new DateTime(2023, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2024, 1, 1, 2, 3, 4));
    }

    [Test]
    public void MoveByLength_WhenNegativeOne_ReturnPrev()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, 1);

        var actual = interval.MoveByLength(-1);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 1, 2, 3, 4));
    }

    [Test]
    public void MoveByLength_WhenNegativeOneAndZero_ReturnIntervalWithLeftAddition()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, 1);

        var actual = interval.MoveByLength(-1, 0);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 1, 2, 3, 4));
    }

    [Test]
    public void MoveByLength_WhenOneAndZero_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, 1);

        var actual = interval.MoveByLength(1, 0);

        actual.LeftValue.Should().Be(new DateTime(2023, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 1, 2, 3, 4));
    }

    [Test]
    public void MoveByLength_WhenZeroAndNegativeOne_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, 1);

        var actual = interval.MoveByLength(0, -1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 1, 2, 3, 4));
    }

    [Test]
    public void MoveByLength_WhenZeroAndOne_ReturnIntervalWithRightAddition()
    {
        var leftValue = new DateTime(2022, 1, 1, 2, 3, 4);
        var rightValue = new DateTime(2023, 1, 1, 2, 3, 4);
        var interval = new MonthGranularInterval(leftValue, rightValue, 1);

        var actual = interval.MoveByLength(0, 1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 1, 2, 3, 4));
        actual.RightValue.Should().Be(new DateTime(2024, 1, 1, 2, 3, 4));
    }
}