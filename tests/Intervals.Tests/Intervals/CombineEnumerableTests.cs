using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class CombineEnumerableTests
{
    [TestCaseSource(nameof(Combine_WhenTwoIntervals_Data))]
    public void Combine_WhenTwoIntervals(IInterval<int> first, IInterval<int> second, IInterval<int>[] result)
    {
        var actual1 = first.Combine(second);
        var actual2 = second.Combine(first);

        actual1.Should().Equal(result);
        actual2.Should().Equal(result);
    }

    [TestCaseSource(nameof(Combine_WhenManyIntervals_Data))]
    public void Combine_WhenManyIntervals(IInterval<int>[] first, IInterval<int>[] second, IInterval<int>[] result)
    {
        var actual1 = first.Combine(second);
        var actual2 = second.Combine(first);

        actual1.Should().Equal(result);
        actual2.Should().Equal(result);
    }
}