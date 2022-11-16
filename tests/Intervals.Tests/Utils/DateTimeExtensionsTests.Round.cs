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
using Intervals.Utils;
using NUnit.Framework;

namespace Intervals.Tests.Utils;

public partial class DateTimeExtensionsTests
{
    [Test]
    public void Round_WhenHasNotRest_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 1, 2, 0, 0, 0);

        var actual = dateTime.Round(TimeSpan.FromDays(1));

        actual.Should().Be(new DateTime(2022, 1, 2));
    }

    [Test]
    public void Round_WhenHasRestWhichLessThanMidpoint_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 1, 2, 11, 0, 0);

        var actual = dateTime.Round(TimeSpan.FromDays(1));

        actual.Should().Be(new DateTime(2022, 1, 2));
    }

    [Test]
    public void Round_WhenHasRestWhichEqualsMidpointAndToEven_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 1, 2, 12, 0, 0);

        var actual = dateTime.Round(TimeSpan.FromDays(1), MidpointRounding.ToEven);

        actual.Should().Be(new DateTime(2022, 1, 2));
    }

    [Test]
    public void Round_WhenHasRestWhichEqualsMidpointAndAwayFromZero_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 1, 2, 12, 0, 0);

        var actual = dateTime.Round(TimeSpan.FromDays(1), MidpointRounding.AwayFromZero);

        actual.Should().Be(new DateTime(2022, 1, 3));
    }

    [Test]
    public void Round_WhenHasRestWhichGreatThanMidpoint_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 1, 2, 13, 0, 0);

        var actual = dateTime.Round(TimeSpan.FromDays(1));

        actual.Should().Be(new DateTime(2022, 1, 3));
    }

    [Test]
    public void RoundToMonth_WhenHasNotModulo_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 2, 1, 0, 0, 0);

        var actual = dateTime.RoundToMonth();

        actual.Should().Be(new DateTime(2022, 2, 1));
    }

    [Test]
    public void RoundToMonth_WhenHasModuloWhichLessThanMidpoint_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 2, 14, 0, 0, 0);

        var actual = dateTime.RoundToMonth();

        actual.Should().Be(new DateTime(2022, 2, 1));
    }

    [Test]
    public void RoundToMonth_WhenHasModuloWhichEqualsMidpointAndToEven_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 2, 15, 0, 0, 0);

        var actual = dateTime.RoundToMonth(MidpointRounding.ToEven);

        actual.Should().Be(new DateTime(2022, 2, 1));
    }

    [Test]
    public void RoundToMonth_WhenHasModuloWhichEqualsMidpointAndAwayFromZero_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 2, 15, 0, 0, 0);

        var actual = dateTime.RoundToMonth(MidpointRounding.AwayFromZero);

        actual.Should().Be(new DateTime(2022, 3, 1));
    }

    [Test]
    public void RoundToMonth_WhenHasModuloWhichGreatThanMidpoint_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 2, 16, 0, 0, 0);

        var actual = dateTime.RoundToMonth();

        actual.Should().Be(new DateTime(2022, 3, 1));
    }

    [Test]
    public void RoundToQuarter_WhenHasNotModulo_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 1, 1, 0, 0, 0);

        var actual = dateTime.RoundToQuarter();

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToQuarter_WhenHasModuloWhichLessThanMidpoint_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 2, 14, 0, 0, 0);

        var actual = dateTime.RoundToQuarter();

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToQuarter_WhenHasModuloWhichEqualsMidpointAndToEven_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 2, 15, 0, 0, 0);

        var actual = dateTime.RoundToQuarter(MidpointRounding.ToEven);

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToQuarter_WhenHasModuloWhichEqualsMidpointAndAwayFromZero_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 2, 15, 0, 0, 0);

        var actual = dateTime.RoundToQuarter(MidpointRounding.AwayFromZero);

        actual.Should().Be(new DateTime(2022, 4, 1));
    }

    [Test]
    public void RoundToQuarter_WhenHasModuloWhichGreatThanMidpoint_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 2, 16, 0, 0, 0);

        var actual = dateTime.RoundToQuarter();

        actual.Should().Be(new DateTime(2022, 4, 1));
    }

    [Test]
    public void RoundToHalfYear_WhenHasNotModulo_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 1, 1, 0, 0, 0);

        var actual = dateTime.RoundToHalfYear();

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToHalfYear_WhenHasModuloWhichLessThanMidpoint_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 4, 1, 11, 0, 0);

        var actual = dateTime.RoundToHalfYear();

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToHalfYear_WhenHasModuloWhichEqualsMidpointAndToEven_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 4, 1, 12, 0, 0);

        var actual = dateTime.RoundToHalfYear(MidpointRounding.ToEven);

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToHalfYear_WhenHasModuloWhichEqualsMidpointAndAwayFromZero_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 4, 1, 12, 0, 0);

        var actual = dateTime.RoundToHalfYear(MidpointRounding.AwayFromZero);

        actual.Should().Be(new DateTime(2022, 7, 1));
    }

    [Test]
    public void RoundToHalfYear_WhenHasModuloWhichGreatThanMidpoint_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 4, 1, 13, 0, 0);

        var actual = dateTime.CeilingToHalfYear();

        actual.Should().Be(new DateTime(2022, 7, 1));
    }

    [Test]
    public void RoundToYear_WhenHasNotModulo_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 1, 1, 0, 0, 0);

        var actual = dateTime.RoundToYear();

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToYear_WhenHasModuloWhichLessThanMidpoint_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 7, 2, 11, 0, 0);

        var actual = dateTime.RoundToYear();

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToYear_WhenHasModuloWhichEqualsMidpointAndToEven_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 7, 2, 12, 0, 0);

        var actual = dateTime.RoundToYear(MidpointRounding.ToEven);

        actual.Should().Be(new DateTime(2022, 1, 1));
    }

    [Test]
    public void RoundToYear_WhenHasModuloWhichEqualsMidpointAndAwayFromZero_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 7, 2, 12, 0, 0);

        var actual = dateTime.RoundToYear(MidpointRounding.AwayFromZero);

        actual.Should().Be(new DateTime(2023, 1, 1));
    }

    [Test]
    public void RoundToYear_WhenHasModuloWhichGreatThanMidpoint_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 7, 2, 13, 0, 0);

        var actual = dateTime.CeilingToYear();

        actual.Should().Be(new DateTime(2023, 1, 1));
    }
}