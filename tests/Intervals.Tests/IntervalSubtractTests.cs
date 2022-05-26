using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests;

public class IntervalSubtractTests
{
	[TestCaseSource(typeof(SingleIntervalsSubtractData))]
	public void Subtract_WhenSingleIntervals(IInterval<int> first, IInterval<int> second, IInterval<int>[] result)
	{
		var actual = first.Subtract(second);

		actual.Should().Equal(result);
	}

	[TestCaseSource(typeof(MultipleIntervalsSubtractData))]
	public void Subtract_WhenMultipleIntervals(IInterval<int>[] first, IInterval<int>[] second, IInterval<int>[] result)
	{
		var actual = first.Subtract(second);

		actual.Should().Equal(result);
	}
}