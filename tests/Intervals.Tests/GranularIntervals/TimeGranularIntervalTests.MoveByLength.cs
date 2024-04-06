using FluentAssertions;
using Intervals.GranularIntervals;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class TimeGranularIntervalTests
{
    [Test]
    public void MoveByLength_WhenOne_ReturnNext()
    {
        var leftValue = new DateTime(2021, 1, 1, 1, 1, 1);
        var rightValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromSeconds(1));

        var actual = interval.MoveByLength(1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void MoveByLength_WhenNegativeOne_ReturnPrev()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromSeconds(1));

        var actual = interval.MoveByLength(-1);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
    }

    [Test]
    public void MoveByLength_WhenNegativeOneAndZero_ReturnIntervalWithLeftAddition()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromSeconds(1));

        var actual = interval.MoveByLength(-1, 0);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void MoveByLength_WhenOneAndZero_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromSeconds(1));

        var actual = interval.MoveByLength(1, 0);

        actual.LeftValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void MoveByLength_WhenZeroAndNegativeOne_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromSeconds(1));

        var actual = interval.MoveByLength(0, -1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
    }

    [Test]
    public void MoveByLength_WhenZeroAndOne_ReturnIntervalWithRightAddition()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue, TimeSpan.FromSeconds(1));

        var actual = interval.MoveByLength(0, 1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2024, 1, 10, 13, 16, 19));
    }
}