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
    public void Round_WhenLeftLessMidpointAndRightLessMidpoint_ReturnBothFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var round = interval.Round(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Round_WhenLeftLessMidpointAndRightGreatMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var round = interval.Round(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Round_WhenLeftGreatMidpointAndRightLessMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var round = interval.Round(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Round_WhenLeftGreatMidpointAndRightGreatMidpoint_ReturnBothCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var round = interval.Round(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Ceiling_WhenLeftLessMidpointAndRightLessMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var round = interval.Ceiling(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Ceiling_WhenLeftLessMidpointAndRightGreatMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var round = interval.Ceiling(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Ceiling_WhenLeftGreatMidpointAndRightLessMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var round = interval.Ceiling(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Ceiling_WhenLeftGreatMidpointAndRightGreatMidpoint_ReturnLeftFloorAndRightCeiling()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var round = interval.Ceiling(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 2, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 3, 0, 0, 0));
    }

    [Test]
    public void Floor_WhenLeftLessMidpointAndRightLessMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var round = interval.Floor(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Floor_WhenLeftLessMidpointAndRightGreatMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 11, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var round = interval.Floor(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Floor_WhenLeftGreatMidpointAndRightLessMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 11, 0, 0));

        var round = interval.Floor(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }

    [Test]
    public void Floor_WhenLeftGreatMidpointAndRightGreatMidpoint_ReturnLeftCeilingAndRightFloor()
    {
        var interval = new Interval<DateTime>(new DateTime(2022, 1, 2, 13, 0, 0), new DateTime(2022, 2, 2, 13, 0, 0));

        var round = interval.Floor(TimeSpan.FromDays(1));

        round.Left.Value.Should().Be(new DateTime(2022, 1, 3, 0, 0, 0));
        round.Right.Value.Should().Be(new DateTime(2022, 2, 2, 0, 0, 0));
    }
}