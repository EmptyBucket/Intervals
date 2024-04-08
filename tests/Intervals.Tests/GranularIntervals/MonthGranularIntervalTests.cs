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

namespace Intervals.Tests.GranularIntervals;

public partial class MonthGranularIntervalTests
{
    [Test]
    public void New_WhenNegativeOneMonthsCount_ThrowArgumentException()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2021, 12, 1);

        var action = new Action(() => new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1)));

        action.Should().Throw<ArgumentException>().And.Message.Should().Contain("must not be less or equal zero");
    }

    [Test]
    public void New_WhenZeroMonthsCount_ThrowArgumentException()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 1);

        var action = new Action(() => new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1)));

        action.Should().Throw<ArgumentException>().And.Message.Should().Contain("must not be less or equal zero");
    }

    [Test]
    public void New_WhenNotAlignedToGranuleLength_ThrowArgumentException()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 2, 1, 1, 1, 1);

        var action = new Action(() => new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1)));

        action.Should().Throw<ArgumentException>().And.Message.Should().Contain("must be aligned");
    }

    [Test]
    public void New_WhenLeftOpenedAndLeftIsFirstDayOfMonth_ThrowArgumentException()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 2, 1);

        var action = new Action(() =>
            new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.LeftOpened));

        action.Should().Throw<ArgumentException>().And.Message.Should()
            .ContainAny("must be first day of month", "must be last day of month");
    }

    [Test]
    public void New_WhenLeftOpenedAndRightIsFirstDayOfMonth_ThrowArgumentException()
    {
        var leftValue = new DateTime(2021, 12, 31);
        var rightValue = new DateTime(2022, 2, 1);

        var action = new Action(() =>
            new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.LeftOpened));

        action.Should().Throw<ArgumentException>().And.Message.Should()
            .ContainAny("must be first day of month", "must be last day of month");
    }

    [Test]
    public void New_WhenRightOpenedAndRightIsLastDayOfMonth_ThrowArgumentException()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 2, 28);

        var action = new Action(() =>
            new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.RightOpened));

        action.Should().Throw<ArgumentException>().And.Message.Should()
            .ContainAny("must be first day of month", "must be last day of month");
    }

    [Test]
    public void New_WhenRightOpenedAndLeftIsLastDayOfMonth_ThrowArgumentException()
    {
        var leftValue = new DateTime(2021, 12, 31);
        var rightValue = new DateTime(2022, 2, 1);

        var action = new Action(() =>
            new MonthGranularInterval(leftValue, rightValue, TimeSpan.FromDays(1), IntervalInclusion.RightOpened));

        action.Should().Throw<ArgumentException>().And.Message.Should()
            .ContainAny("must be first day of month", "must be last day of month");
    }

    [Test]
    public void New_WhenOpened_ReturnIntervalWithGranularRight()
    {
        var leftValue = new DateTime(2021, 12, 31);

        var interval = new MonthGranularInterval(leftValue, TimeSpan.FromDays(1), 1, IntervalInclusion.Opened);

        interval.RightValue.Should().Be(new DateTime(2022, 2, 1));
    }

    [Test]
    public void New_WhenLeftOpened_ReturnIntervalWithGranularRight()
    {
        var leftValue = new DateTime(2021, 12, 31);

        var interval = new MonthGranularInterval(leftValue, TimeSpan.FromDays(1), 1, IntervalInclusion.LeftOpened);

        interval.RightValue.Should().Be(new DateTime(2022, 1, 31));
    }

    [Test]
    public void New_WhenRightOpened_ReturnIntervalWithGranularRight()
    {
        var leftValue = new DateTime(2022, 1, 1);

        var interval = new MonthGranularInterval(leftValue, TimeSpan.FromDays(1), 1, IntervalInclusion.RightOpened);

        interval.RightValue.Should().Be(new DateTime(2022, 2, 1));
    }

    [Test]
    public void New_WhenClosed_ReturnIntervalWithGranularRight()
    {
        var leftValue = new DateTime(2022, 1, 1);

        var interval = new MonthGranularInterval(leftValue, TimeSpan.FromDays(1), 1, IntervalInclusion.Closed);

        interval.RightValue.Should().Be(new DateTime(2022, 1, 31));
    }
}