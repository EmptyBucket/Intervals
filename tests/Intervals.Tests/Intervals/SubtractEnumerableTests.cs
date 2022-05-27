using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class SubtractEnumerableTests
{
    [TestCaseSource(nameof(Subtract_WhenTwoIntervals_Data))]
    public void Subtract_WhenTwoIntervals(IInterval<int> first, IInterval<int> second, IInterval<int>[] result)
    {
        var actual = first.Subtract(second);

        actual.Should().Equal(result);
    }

    [TestCaseSource(nameof(Subtract_WhenManyIntervals_Data))]
    public void Subtract_WhenManyIntervals(IInterval<int>[] first, IInterval<int>[] second, IInterval<int>[] result)
    {
        var actual = first.Subtract(second);

        actual.Should().Equal(result);
    }
}