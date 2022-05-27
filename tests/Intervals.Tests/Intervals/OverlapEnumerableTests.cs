using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class OverlapEnumerableTests
{
    [TestCaseSource(nameof(Overlap_WhenTwoIntervals_Data))]
    public void Overlap_WhenTwoIntervals(IInterval<int> first, IInterval<int> second, IInterval<int>[] result)
    {
        var actual1 = first.Overlap(second);
        var actual2 = second.Overlap(first);

        actual1.Should().Equal(result);
        actual2.Should().Equal(result);
    }

    [TestCaseSource(nameof(Overlap_WhenManyIntervals_Data))]
    public void Overlap_WhenManyIntervals(IInterval<int>[] first, IInterval<int>[] second, IInterval<int>[] result)
    {
        var actual1 = first.Overlap(second);
        var actual2 = second.Overlap(first);

        actual1.Should().Equal(result);
        actual2.Should().Equal(result);
    }
}