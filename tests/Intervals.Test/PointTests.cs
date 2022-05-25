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

namespace Intervals.Test;

public class PointTests
{
    [Test]
    public void Included__ReturnIncludedPoint()
    {
        var actual = Point.Included(0);

        actual.Inclusion.Should().Be(Inclusion.Included);
    }

    [Test]
    public void Excluded__ReturnExcludedPoint()
    {
        var actual = Point.Excluded(0);

        actual.Inclusion.Should().Be(Inclusion.Excluded);
    }

    [Test]
    public void Equals_WhenHasSameMembers_ReturnTrue()
    {
        var first = new Point<int>(0, Inclusion.Excluded);
        var second = new Point<int>(0, Inclusion.Excluded);

        var actual = first.Equals(second);

        actual.Should().BeTrue();
    }

    [Test]
    public void Equals_WhenHasOtherValue_ReturnFalse()
    {
        var first = new Point<int>(0, Inclusion.Excluded);
        var second = new Point<int>(1, Inclusion.Excluded);

        var actual = first.Equals(second);

        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WhenHasOtherInclusion_ReturnFalse()
    {
        var first = new Point<int>(0, Inclusion.Excluded);
        var second = new Point<int>(0, Inclusion.Included);

        var actual = first.Equals(second);

        actual.Should().BeFalse();
    }
}