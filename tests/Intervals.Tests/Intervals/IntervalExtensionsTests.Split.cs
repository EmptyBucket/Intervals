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

using FluentAssertions;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class IntervalExtensionsTests
{
    [Test]
    [TestCaseSource(nameof(SplitBySeconds_Data))]
    public void SplitBy_WhenByTwoSeconds(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitBy(TimeSpan.FromSeconds(2)).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitBySeconds_Data))]
    public void SplitBySeconds_WhenByTwoSeconds(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitBySeconds(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByMinutes_Data))]
    public void SplitByMinutes_WhenByTwoMinutes(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitByMinutes(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByHours_Data))]
    public void SplitByHours_WhenByTwoHours(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitByHours(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByDays_Data))]
    public void SplitByDays_WhenByTwoDays(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitByDays(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByMonths_Data))]
    public void SplitByDays_WhenByTwoMonths(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitByMonths(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByQuarters_Data))]
    public void SplitByDays_WhenByTwoQuarters(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitByQuarters(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByHalfYears_Data))]
    public void SplitByDays_WhenByTwoHalfYears(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitByHalfYears(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByYears_Data))]
    public void SplitByDays_WhenByTwoYears(IInterval<DateTime> interval, IInterval<DateTime>[] expected)
    {
        var actual = interval.SplitByYears(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }
}