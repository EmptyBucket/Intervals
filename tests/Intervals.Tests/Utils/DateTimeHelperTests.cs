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

public class DateTimeHelperTests
{
    [Test]
    [TestCase(1, 1)]
    [TestCase(2, 4)]
    [TestCase(3, 7)]
    [TestCase(4, 10)]
    public void QuarterToMonth_WhenGivenQuarter_ReturnExpectedMonth(int quarter, int month)
    {
        var actual = DateTimeHelper.QuarterToMonth(quarter);

        actual.Should().Be(month);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(5)]
    public void QuarterToMonth_WhenGivenQuarter_ThrowArgumentException(int quarter)
    {
        var action = new Action(() => DateTimeHelper.QuarterToMonth(quarter));

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(2, 7)]
    public void HalfYearToMonth_WhenGivenHalfYear_ReturnExpectedMonth(int halfYear, int month)
    {
        var actual = DateTimeHelper.HalfYearToMonth(halfYear);

        actual.Should().Be(month);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(13)]
    public void HalfYearToMonth_WhenGivenHalfYear_ThrowArgumentException(int halfYear)
    {
        var action = new Action(() => DateTimeHelper.QuarterToMonth(halfYear));

        action.Should().Throw<ArgumentException>();
    }
}