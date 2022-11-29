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
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    public void IsOverlap_WhenFirstToLeftOfSecond_ReturnFalse(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 1, firstIntervalInclusion);
        var second = new Interval<int>(2, 3, secondIntervalInclusion);

        var actual = first.IsOverlap(second);

        actual.Should().BeFalse();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    public void IsOverlap_WhenHaveSameValueAndAnyOfThemOpened_ReturnFalse(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 1, firstIntervalInclusion);
        var second = new Interval<int>(1, 2, secondIntervalInclusion);

        var actual = first.IsOverlap(second);

        actual.Should().BeFalse();
    }

    [Test]
    public void IsOverlap_WhenHaveSameValueAndBothClosed_ReturnTrue()
    {
        var first = new Interval<int>(0, 1, IntervalInclusion.Closed);
        var second = new Interval<int>(1, 2, IntervalInclusion.Closed);

        var actual = first.IsOverlap(second);

        actual.Should().BeTrue();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    public void IsOverlap_WhenHaveSameValues_ReturnTrue(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 1, firstIntervalInclusion);
        var second = new Interval<int>(0, 1, secondIntervalInclusion);

        var actual = first.IsOverlap(second);

        actual.Should().BeTrue();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    public void IsOverlap_WhenAnyOfThemHasMiddleValue_ReturnTrue(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 2, firstIntervalInclusion);
        var second = new Interval<int>(1, 3, secondIntervalInclusion);

        var actual = first.IsOverlap(second);

        actual.Should().BeTrue();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    public void IsOverlap_WhenSecondHasMiddleValues_ReturnTrue(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 3, firstIntervalInclusion);
        var second = new Interval<int>(1, 2, secondIntervalInclusion);

        var actual = first.IsOverlap(second);

        actual.Should().BeTrue();
    }


    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    public void IsInclude_WhenFirstToLeftOfSecond_ReturnFalse(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 1, firstIntervalInclusion);
        var second = new Interval<int>(2, 3, secondIntervalInclusion);

        var actual = first.IsInclude(second);

        actual.Should().BeFalse();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    public void IsInclude_WhenHaveSameValue_ReturnFalse(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 1, firstIntervalInclusion);
        var second = new Interval<int>(1, 2, secondIntervalInclusion);

        var actual = first.IsInclude(second);

        actual.Should().BeFalse();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    public void IsInclude_WhenHaveSameValuesAndFirstNotOpened_ReturnTrue(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 1, firstIntervalInclusion);
        var second = new Interval<int>(0, 1, secondIntervalInclusion);

        var actual = first.IsInclude(second);

        actual.Should().BeTrue();
    }

    [Test]
    public void IsInclude_WhenHaveSameValuesAndFirstOpenedAndSecondClosed_ReturnFalse()
    {
        var first = new Interval<int>(0, 1, IntervalInclusion.Opened);
        var second = new Interval<int>(0, 1, IntervalInclusion.Closed);

        var actual = first.IsInclude(second);

        actual.Should().BeFalse();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    public void IsInclude_WhenAnyOfThemHasMiddleValue_ReturnFalse(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 2, firstIntervalInclusion);
        var second = new Interval<int>(1, 3, secondIntervalInclusion);

        var actual = first.IsInclude(second);

        actual.Should().BeFalse();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.Opened, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed, IntervalInclusion.Closed)]
    public void IsInclude_WhenSecondHasMiddleValues_ReturnTrue(IntervalInclusion firstIntervalInclusion,
        IntervalInclusion secondIntervalInclusion)
    {
        var first = new Interval<int>(0, 3, firstIntervalInclusion);
        var second = new Interval<int>(1, 2, secondIntervalInclusion);

        var actual = first.IsOverlap(second);

        actual.Should().BeTrue();
    }
}