using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test;

public class IntervalOverlapTests
{
	[TestCaseSource(typeof(SingleIntervalsOverlapData))]
	public void Overlap_WhenSingleIntervals(IInterval<int> first, IInterval<int> second, IInterval<int>[] result)
	{
		var actual1 = first.Overlap(second);
		var actual2 = second.Overlap(first);

		actual1.Should().Equal(result);
		actual2.Should().Equal(result);
	}

	[TestCaseSource(typeof(MultipleIntervalsOverlapData))]
	public void Overlap_WhenMultipleIntervals(IInterval<int>[] first, IInterval<int>[] second, IInterval<int>[] result)
	{
		var actual1 = first.Overlap(second);
		var actual2 = second.Overlap(first);

		actual1.Should().Equal(result);
		actual2.Should().Equal(result);
	}
}