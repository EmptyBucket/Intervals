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
    [TestCaseSource(nameof(Split_Data))]
    public void Split_WhenByTwoSeconds(Interval<DateTime> interval, Interval<DateTime>[] expected)
    {
        var actual = interval.Split(TimeSpan.FromSeconds(2)).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByMonths_Data))]
    public void SplitByDays_WhenByTwoMonths(Interval<DateTime> interval, Interval<DateTime>[] expected)
    {
        var actual = interval.SplitByMonths(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByQuarters_Data))]
    public void SplitByDays_WhenByTwoQuarters(Interval<DateTime> interval, Interval<DateTime>[] expected)
    {
        var actual = interval.SplitByQuarters(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }

    [Test]
    [TestCaseSource(nameof(SplitByHalfYears_Data))]
    public void SplitByDays_WhenByTwoHalfYears(Interval<DateTime> interval, Interval<DateTime>[] expected)
    {
        var actual = interval.SplitByHalfYears(2).ToArray();

        actual.Should().BeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
    }
}