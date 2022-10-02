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
    public void Ceiling_WhenBothLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var actual = interval.Ceiling(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Ceiling_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var actual = interval.Ceiling(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Ceiling_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var actual = interval.Ceiling(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Ceiling_WhenBothGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var actual = interval.Ceiling(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void CeilingToMonth_WhenBothLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.CeilingToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToMonth_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.CeilingToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToMonth_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.CeilingToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToMonth_WhenBothGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.CeilingToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToQuarter_WhenBothLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.CeilingToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToQuarter_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.CeilingToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToQuarter_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.CeilingToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToQuarter_WhenBothGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.CeilingToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToHalfYear_WhenBothLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 11, 0, 0), new DateTime(2022, 4, 1, 11, 0, 0));

        var actual = interval.CeilingToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToHalfYear_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 11, 0, 0), new DateTime(2022, 4, 1, 13, 0, 0));

        var actual = interval.CeilingToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToHalfYear_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 13, 0, 0), new DateTime(2022, 4, 1, 11, 0, 0));

        var actual = interval.CeilingToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
    }

    [Test]
    public void CeilingToHalfYear_WhenBothGreatThanMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 13, 0, 0), new DateTime(2022, 4, 1, 13, 0, 0));

        var actual = interval.CeilingToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
    }
}