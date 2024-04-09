using FluentAssertions;
using Intervals.GranularIntervals;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class TimeGranularIntervalTests
{
    [Test]
    public void MoveByGranule_WhenOne_ReturnNext()
    {
        var leftValue = new DateTime(2021, 1, 1, 1, 1, 1);
        var rightValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var actual = interval.MoveByGranule(1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void MoveByGranule_WhenNegativeOne_ReturnPrev()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var actual = interval.MoveByGranule(-1);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
    }

    [Test]
    public void MoveByGranule_WhenNegativeOneAndZero_ReturnIntervalWithLeftAddition()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var actual = interval.MoveByGranule(-1, 0);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void MoveByGranule_WhenOneAndZero_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var actual = interval.MoveByGranule(1, 0);

        actual.LeftValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void MoveByGranule_WhenZeroAndNegativeOne_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var actual = interval.MoveByGranule(0, -1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
    }

    [Test]
    public void MoveByGranule_WhenZeroAndOne_ReturnIntervalWithRightAddition()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var actual = interval.MoveByGranule(0, 1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2024, 1, 10, 13, 16, 19));
    }
}