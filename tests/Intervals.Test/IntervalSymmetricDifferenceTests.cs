using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test;

public class IntervalSymmetricDifferenceTests
{
	[TestCaseSource(typeof(SingleIntervalsSymmetricDifferenceData))]
	public void SymmetricDifference_WhenSingleIntervals(IInterval<int> first, IInterval<int> second,
		IInterval<int>[] result)
	{
		var actual1 = first.SymmetricDifference(second);
		var actual2 = second.SymmetricDifference(first);

		actual1.Should().Equal(result);
		actual2.Should().Equal(result);
	}

	[TestCaseSource(typeof(MultipleIntervalsSymmetricDifferenceData))]
	public void SymmetricDifference_WhenMultipleIntervals(IInterval<int>[] first, IInterval<int>[] second,
		IInterval<int>[] result)
	{
		var actual1 = first.SymmetricDifference(second);
		var actual2 = second.SymmetricDifference(first);

		actual1.Should().Equal(result);
		actual2.Should().Equal(result);
	}
}