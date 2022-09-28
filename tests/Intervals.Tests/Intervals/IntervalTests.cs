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
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public class IntervalTests
{
    [Test]
    public void Equals_WhenHasEqualEndpoints_ReturnTrue()
    {
        var first = new Interval<int>(Point.Included(0), Point.Included(0));
        var second = new Interval<int>(Point.Included(0), Point.Included(0));

        var actual = first.Equals(second);

        actual.Should().BeTrue();
    }

    [Test]
    public void Equals_WhenHasNotEqualEndpoints_ReturnFalse()
    {
        var first = new Interval<int>(Point.Included(0), Point.Included(0));
        var second = new Interval<int>(Point.Included(1), Point.Included(1));

        var actual = first.Equals(second);

        actual.Should().BeFalse();
    }

    [Test]
    public void CompareTo_WhenLeftLess_ReturnLessZero()
    {
        var first = new Interval<int>(Point.Included(0), Point.Included(0));
        var second = new Interval<int>(Point.Included(1), Point.Included(1));

        var actual = first.CompareTo(second);

        actual.Should().Be(-1);
    }

    [Test]
    public void CompareTo_WhenLeftMore_ReturnMoreZero()
    {
        var first = new Interval<int>(Point.Included(1), Point.Included(1));
        var second = new Interval<int>(Point.Included(0), Point.Included(0));

        var actual = first.CompareTo(second);

        actual.Should().Be(1);
    }

    [Test]
    public void CompareTo_WhenLeftEqualsAndRightLess_ReturnLessZero()
    {
        var first = new Interval<int>(Point.Included(0), Point.Included(0));
        var second = new Interval<int>(Point.Included(0), Point.Included(1));

        var actual = first.CompareTo(second);

        actual.Should().Be(-1);
    }

    [Test]
    public void CompareTo_WhenLeftEqualsAndRightMore_ReturnMoreZero()
    {
        var first = new Interval<int>(Point.Included(0), Point.Included(1));
        var second = new Interval<int>(Point.Included(0), Point.Included(0));

        var actual = first.CompareTo(second);

        actual.Should().Be(1);
    }

    [Test]
    public void CompareTo_WhenLeftAndRightEquals_ReturnZero()
    {
        var first = new Interval<int>(Point.Included(0), Point.Included(0));
        var second = new Interval<int>(Point.Included(0), Point.Included(0));

        var actual = first.CompareTo(second);

        actual.Should().Be(0);
    }

    [Test]
    [TestCase(0, Inclusion.Excluded)]
    [TestCase(0, Inclusion.Included)]
    public void ExpandLeft_WhenGivenLeft_ReturnIntervalWithGivenLeft(int value, Inclusion inclusion)
    {
        var left = new Point<int>(value, inclusion);
        var interval = new Interval<int>(5, 10);

        var actual = interval.ExpandLeft(left);

        actual.Left.Value.Should().Be(value);
        actual.Left.Inclusion.Should().Be(inclusion);
        actual.Right.Value.Should().Be(10);
        actual.Right.Inclusion.Should().Be(Inclusion.Excluded);
    }

    [Test]
    [TestCase(15, Inclusion.Excluded)]
    [TestCase(15, Inclusion.Included)]
    public void ExpandRight_WhenGivenRight_ReturnIntervalWithGivenRight(int value, Inclusion inclusion)
    {
        var right = new Point<int>(value, inclusion);
        var interval = new Interval<int>(5, 10);

        var actual = interval.ExpandRight(right);

        actual.Left.Value.Should().Be(5);
        actual.Left.Inclusion.Should().Be(Inclusion.Included);
        actual.Right.Value.Should().Be(value);
        actual.Right.Inclusion.Should().Be(inclusion);
    }

    [Test]
    [TestCase(IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.LeftOpened)]
    [TestCase(IntervalInclusion.RightOpened)]
    public void IsEmpty_WhenLeftLessThanRight_ReturnTrue(IntervalInclusion inclusion)
    {
        var interval = new Interval<int>(0, -1, inclusion);

        var actual = interval.IsEmpty();

        actual.Should().BeTrue();
    }

    [Test]
    public void IsEmpty_WhenClosedIntervalWithEqualEndpoints_ReturnFalse()
    {
        var interval = new Interval<int>(0, 0, IntervalInclusion.Closed);

        var actual = interval.IsEmpty();

        actual.Should().BeFalse();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.LeftOpened)]
    [TestCase(IntervalInclusion.RightOpened)]
    public void IsEmpty_WhenNonClosedIntervalWithEqualEndpoints_ReturnTrue(IntervalInclusion inclusion)
    {
        var interval = new Interval<int>(0, 0, inclusion);

        var actual = interval.IsEmpty();

        actual.Should().BeTrue();
    }

    [Test]
    [TestCase(IntervalInclusion.Opened)]
    [TestCase(IntervalInclusion.Closed)]
    [TestCase(IntervalInclusion.LeftOpened)]
    [TestCase(IntervalInclusion.RightOpened)]
    public void IsEmpty_WhenIntervalWithNonEqualEndpoints_ReturnFalse(IntervalInclusion inclusion)
    {
        var interval = new Interval<int>(0, 1, inclusion);

        var actual = interval.IsEmpty();

        actual.Should().BeFalse();
    }
}