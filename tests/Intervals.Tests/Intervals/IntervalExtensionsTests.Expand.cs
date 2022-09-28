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

public partial class IntervalExtensionsTests
{
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
}