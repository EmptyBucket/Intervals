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

public class DateTimeExtensionsTests
{
    [Test]
    [TestCase(1, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 2)]
    [TestCase(6, 2)]
    [TestCase(7, 3)]
    [TestCase(9, 3)]
    [TestCase(10, 4)]
    [TestCase(12, 4)]
    public void GetQuarter_WhenGivenMonth_ReturnExpectedQuarter(int month, int quarter)
    {
        var dateTime = new DateTime(2022, month, 1);

        var actual = dateTime.GetQuarter();

        actual.Should().Be(quarter);
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(6, 1)]
    [TestCase(7, 2)]
    [TestCase(12, 2)]
    public void GetHalfYear_WhenGivenMonth_ReturnExpectedHalfYear(int month, int halfYear)
    {
        var dateTime = new DateTime(2022, month, 1);

        var actual = dateTime.GetHalfYear();

        actual.Should().Be(halfYear);
    }
}