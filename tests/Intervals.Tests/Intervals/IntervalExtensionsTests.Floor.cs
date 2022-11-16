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
using Intervals.GranularIntervals;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class IntervalExtensionsTests
{
    [Test]
    public void Floor__ReturnTimeGranularInterval()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var actual = interval.Floor(TimeSpan.FromDays(1));

        actual.Should().BeOfType<TimeGranularInterval>();
    }

    [Test]
    public void Floor_WhenBothLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var actual = interval.Floor(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Floor_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var actual = interval.Floor(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Floor_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var actual = interval.Floor(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Floor_WhenBothGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var actual = interval.Floor(TimeSpan.FromDays(1));

        actual.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void FloorToMonth__ReturnMonthGranularInterval()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.FloorToMonth();

        actual.Should().BeOfType<MonthGranularInterval>();
    }

    [Test]
    public void FloorToMonth_WhenBothLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.FloorToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToMonth_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.FloorToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToMonth_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.FloorToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToMonth_WhenBothGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.FloorToMonth();

        actual.Left.Value.Should().Be(new DateTime(2022, 3, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 2, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToQuarter__ReturnMonthGranularInterval()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.FloorToQuarter();

        actual.Should().BeOfType<MonthGranularInterval>();
    }

    [Test]
    public void FloorToQuarter_WhenBothLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.FloorToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToQuarter_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 14, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.FloorToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToQuarter_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 14, 0, 0, 0));

        var actual = interval.FloorToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToQuarter_WhenBothGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 2, 16, 0, 0, 0), new DateTime(2022, 2, 16, 0, 0, 0));

        var actual = interval.FloorToQuarter();

        actual.Left.Value.Should().Be(new DateTime(2022, 4, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToHalfYear__ReturnMonthGranularInterval()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 11, 0, 0), new DateTime(2022, 4, 1, 11, 0, 0));

        var actual = interval.FloorToHalfYear();

        actual.Should().BeOfType<MonthGranularInterval>();
    }

    [Test]
    public void FloorToHalfYear_WhenBothLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 11, 0, 0), new DateTime(2022, 4, 1, 11, 0, 0));

        var actual = interval.FloorToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToHalfYear_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 11, 0, 0), new DateTime(2022, 4, 1, 13, 0, 0));

        var actual = interval.FloorToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToHalfYear_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 13, 0, 0), new DateTime(2022, 4, 1, 11, 0, 0));

        var actual = interval.FloorToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToHalfYear_WhenBothGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 4, 1, 13, 0, 0), new DateTime(2022, 4, 1, 13, 0, 0));

        var actual = interval.FloorToHalfYear();

        actual.Left.Value.Should().Be(new DateTime(2022, 7, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToYear__ReturnMonthGranularInterval()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 7, 2, 11, 0, 0), new DateTime(2022, 7, 2, 11, 0, 0));

        var actual = interval.FloorToYear();

        actual.Should().BeOfType<MonthGranularInterval>();
    }

    [Test]
    public void FloorToYear_WhenBothLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 7, 2, 11, 0, 0), new DateTime(2022, 7, 2, 11, 0, 0));

        var actual = interval.FloorToYear();

        actual.Left.Value.Should().Be(new DateTime(2023, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToYear_WhenLeftLessThanMidpointAndRightGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 7, 2, 11, 0, 0), new DateTime(2022, 7, 2, 13, 0, 0));

        var actual = interval.FloorToYear();

        actual.Left.Value.Should().Be(new DateTime(2023, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToYear_WhenLeftGreatThanMidpointAndRightLessThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 7, 2, 13, 0, 0), new DateTime(2022, 7, 2, 11, 0, 0));

        var actual = interval.FloorToYear();

        actual.Left.Value.Should().Be(new DateTime(2023, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }

    [Test]
    public void FloorToYear_WhenBothGreatThanMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 7, 2, 13, 0, 0), new DateTime(2022, 7, 2, 13, 0, 0));

        var actual = interval.FloorToYear();

        actual.Left.Value.Should().Be(new DateTime(2023, 1, 1, 0, 0, 0));
        actual.Right.Value.Should().Be(new DateTime(2022, 1, 1, 0, 0, 0));
    }
}