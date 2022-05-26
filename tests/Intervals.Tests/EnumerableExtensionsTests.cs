// MIT License
// 
// Copyright (c) 2021 Alexey Politov, Yevgeny Khoroshavin
// https://github.com/EmptyBucket/Intervals
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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