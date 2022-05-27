using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class SymmetricDifferenceEnumerableTests
{
    [TestCaseSource(nameof(SymmetricDifference_WhenTwoIntervals_Data))]
    public void SymmetricDifference_WhenTwoIntervals(IInterval<int> first, IInterval<int> second,
        IInterval<int>[] result)
    {
        var actual1 = first.SymmetricDifference(second);
        var actual2 = second.SymmetricDifference(first);

        actual1.Should().Equal(result);
        actual2.Should().Equal(result);
    }

    [TestCaseSource(nameof(SymmetricDifference_WhenManyIntervals_Data))]
    public void SymmetricDifference_WhenManyIntervals(IInterval<int>[] first, IInterval<int>[] second,
        IInterval<int>[] result)
    {
        var actual1 = first.SymmetricDifference(second);
        var actual2 = second.SymmetricDifference(first);

        actual1.Should().Equal(result);
        actual2.Should().Equal(result);
    }
}