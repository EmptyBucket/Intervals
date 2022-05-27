using FluentAssertions;
using Intervals.GranularIntervals;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class MonthGranularIntervalTests
{
    [TestCaseSource(nameof(GetNext_Data))]
    public void GetNext(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
        DateTime expectedRightValue)
    {
        var left = Point.Excluded(leftValue);
        var right = Point.Excluded(rightValue);
        var fooInterval = new FooMonthGranularInterval(left, right);

        var actual = fooInterval.GetNext();

        actual.Left.Value.Should().Be(expectedLeftValue);
        actual.Right.Value.Should().Be(expectedRightValue);
    }

    [TestCaseSource(nameof(GetPrev_Data))]
    public void GetPrev(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
        DateTime expectedRightValue)
    {
        var left = Point.Excluded(leftValue);
        var right = Point.Excluded(rightValue);
        var fooInterval = new FooMonthGranularInterval(left, right);

        var actual = fooInterval.GetPrev();

        actual.Left.Value.Should().Be(expectedLeftValue);
        actual.Right.Value.Should().Be(expectedRightValue);
    }

    private class FooMonthGranularInterval : MonthGranularIntervalBase<FooMonthGranularInterval>
    {
        public FooMonthGranularInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right)
        {
        }

        protected override FooMonthGranularInterval Create(Point<DateTime> left, Point<DateTime> right) =>
            new(left, right);
    }
}