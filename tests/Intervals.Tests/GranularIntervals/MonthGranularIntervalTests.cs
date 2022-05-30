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
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class MonthGranularIntervalTests
{
    [TestCaseSource(nameof(MoveForwardData))]
    public void MoveForward(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
        DateTime expectedRightValue)
    {
        var left = Point.Excluded(leftValue);
        var right = Point.Excluded(rightValue);
        var interval = new MonthGranularInterval(left, right);

        var actual = interval.Move();

        actual.Left.Value.Should().Be(expectedLeftValue);
        actual.Right.Value.Should().Be(expectedRightValue);
    }

    [TestCaseSource(nameof(MoveBackwardData))]
    public void MoveBackward(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
        DateTime expectedRightValue)
    {
        var left = Point.Excluded(leftValue);
        var right = Point.Excluded(rightValue);
        var interval = new MonthGranularInterval(left, right);

        var actual = interval.Move(-1);

        actual.Left.Value.Should().Be(expectedLeftValue);
        actual.Right.Value.Should().Be(expectedRightValue);
    }
}