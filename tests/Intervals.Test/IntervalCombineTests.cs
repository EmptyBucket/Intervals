using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test
{
	public class IntervalCombineTests
	{
		[TestCaseSource(typeof(SingleIntervalsCombineData))]
		public void Combine_WhenSingleIntervals_ReturnResult(IInterval<int> first, IInterval<int> second,
			IInterval<int>[] result)
		{
			var actual1 = first.Combine(second);
			var actual2 = second.Combine(first);

			actual1.Should().Equal(result);
			actual2.Should().Equal(result);
		}

		[TestCaseSource(typeof(MultipleIntervalsCombineData))]
		public void Combine_WhenMultipleIntervals_ReturnResult(IInterval<int>[] first, IInterval<int>[] second,
			IInterval<int>[] result)
		{
			var actual1 = first.Combine(second);
			var actual2 = second.Combine(first);

			actual1.Should().Equal(result);
			actual2.Should().Equal(result);
		}
	}
}