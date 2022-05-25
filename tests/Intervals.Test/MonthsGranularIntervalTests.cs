using FluentAssertions;
using Intervals.Intervals;
using Intervals.Points;
using Moq;
using NUnit.Framework;

namespace Intervals.Test;

public class MonthsGranularIntervalTests
{
	[TestCaseSource(typeof(MonthsGranularIntervalGetNextData))]
	public void GetNext(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
		DateTime expectedRightValue)
	{
		var left = Point.Excluded(leftValue);
		var right = Point.Excluded(rightValue);
		var fooInterval = new FooMonthsGranularInterval(left, right);

		var actual = fooInterval.GetNext();

		actual.Left.Value.Should().Be(expectedLeftValue);
		actual.Right.Value.Should().Be(expectedRightValue);
	}

	[TestCaseSource(typeof(MonthsGranularIntervalGetPrevData))]
	public void GetPrev(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
		DateTime expectedRightValue)
	{
		var left = Point.Excluded(leftValue);
		var right = Point.Excluded(rightValue);
		var fooInterval = new FooMonthsGranularInterval(left, right);

		var actual = fooInterval.GetPrev();

		actual.Left.Value.Should().Be(expectedLeftValue);
		actual.Right.Value.Should().Be(expectedRightValue);
	}
}