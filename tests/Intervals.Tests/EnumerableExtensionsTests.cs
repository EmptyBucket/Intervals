using System.Collections;
using FluentAssertions;
using Intervals.Utils;
using NUnit.Framework;

namespace Intervals.Tests;

public class EnumerableExtensionsTests
{
    private static IEnumerable WhenBothHasItemsData()
    {
        yield return new[]
        {
            new[] { 0, 2, 4, 6, 8 },
            new[] { 1, 3, 5, 7, 9 },
            new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        };
        yield return new[]
        {
            new[] { 0 },
            new[] { 1, 3, 5, 7, 9 },
            new[] { 0, 1, 3, 5, 7, 9 }
        };
        yield return new[]
        {
            new[] { 0, 2, 4, 6, 8 },
            new[] { 1 },
            new[] { 0, 1, 2, 4, 6, 8 }
        };
    }

    [Test]
    [TestCaseSource(nameof(WhenBothHasItemsData))]
    public void MergeAscending_WhenBothHasItems_ReturnMerged(IEnumerable<int> first, IEnumerable<int> second,
        IEnumerable<int> result)
    {
        var mergedOrdered = first.MergeAscending(second);

        mergedOrdered.Should().BeEquivalentTo(result);
    }

    [Test]
    public void MergeAscending_WhenBothEmpty_ReturnEmpty()
    {
        var first = Enumerable.Empty<int>();
        var second = Enumerable.Empty<int>();

        var mergedOrdered = first.MergeAscending(second);

        mergedOrdered.Should().BeEquivalentTo(Enumerable.Empty<int>());
    }

    [Test]
    public void MergeAscending_WhenFirstEmpty_ReturnSecond()
    {
        var first = Enumerable.Empty<int>();
        var second = new[] { 1, 3, 5, 7, 9 };

        var mergedOrdered = first.MergeAscending(second);

        mergedOrdered.Should().BeEquivalentTo(second);
    }

    [Test]
    public void MergeAscending_WhenSecondEmpty_ReturnFirst()
    {
        var first = new[] { 0, 2, 4, 6, 8 };
        var second = Enumerable.Empty<int>();

        var mergedOrdered = first.MergeAscending(second);

        mergedOrdered.Should().BeEquivalentTo(first);
    }
}