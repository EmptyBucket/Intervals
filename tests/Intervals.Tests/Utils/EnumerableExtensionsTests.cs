using FluentAssertions;
using Intervals.Utils;
using NUnit.Framework;

namespace Intervals.Tests.Utils;

public partial class EnumerableExtensionsTests
{
    [Test]
    [TestCaseSource(nameof(MergeAscending_WhenBothHasItems_Data))]
    public void MergeAscending_WhenBothHasItems_ReturnMerged(IEnumerable<int> first, IEnumerable<int> second,
        IEnumerable<int> result)
    {
        var mergedOrdered = first.Merge(second, i => i);

        mergedOrdered.Should().BeEquivalentTo(result);
    }

    [Test]
    public void MergeAscending_WhenBothEmpty_ReturnEmpty()
    {
        var first = Enumerable.Empty<int>();
        var second = Enumerable.Empty<int>();

        var mergedOrdered = first.Merge(second, i => i);

        mergedOrdered.Should().BeEquivalentTo(Enumerable.Empty<int>());
    }

    [Test]
    public void MergeAscending_WhenFirstEmpty_ReturnSecond()
    {
        var first = Enumerable.Empty<int>();
        var second = new[] { 1, 3, 5, 7, 9 };

        var mergedOrdered = first.Merge(second, i => i);

        mergedOrdered.Should().BeEquivalentTo(second);
    }

    [Test]
    public void MergeAscending_WhenSecondEmpty_ReturnFirst()
    {
        var first = new[] { 0, 2, 4, 6, 8 };
        var second = Enumerable.Empty<int>();

        var mergedOrdered = first.Merge(second, i => i);

        mergedOrdered.Should().BeEquivalentTo(first);
    }
}