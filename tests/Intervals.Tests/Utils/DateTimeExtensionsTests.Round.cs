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
    public void Round_WhenLessThanMidpoint_ReturnFloor()
    {
        var dateTime = new DateTime(2022, 1, 2, 11, 0, 0);

        var round = dateTime.Round(TimeSpan.FromDays(1));

        round.Should().Be(new DateTime(2022, 1, 2));
    }

    [Test]
    public void Round_WhenGreatThanMidpoint_ReturnCeiling()
    {
        var dateTime = new DateTime(2022, 1, 2, 13, 0, 0);

        var round = dateTime.Round(TimeSpan.FromDays(1));

        round.Should().Be(new DateTime(2022, 1, 3));
    }

    [Test]
    public void Round_WhenEqualsMidpointAndAwayFromZero_ReturnAwayFromZeroRound()
    {
        var dateTime = new DateTime(2022, 1, 2, 12, 0, 0);

        var round = dateTime.Round(TimeSpan.FromDays(1), MidpointRounding.AwayFromZero);

        round.Should().Be(new DateTime(2022, 1, 3));
    }

    [Test]
    public void Round_WhenEqualsMidpointAndToEven_ReturnToEvenRound()
    {
        var dateTime = new DateTime(2022, 1, 2, 12, 0, 0);

        var round = dateTime.Round(TimeSpan.FromDays(1), MidpointRounding.ToEven);

        round.Should().Be(new DateTime(2022, 1, 2));
    }
}