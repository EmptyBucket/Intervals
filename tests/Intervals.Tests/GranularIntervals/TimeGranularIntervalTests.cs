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
using Newtonsoft.Json;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Intervals.Tests.GranularIntervals;

public class TimeGranularIntervalTests
{
    [Test]
    public void Move_WhenOne_ReturnNext()
    {
        var leftValue = new DateTime(2021, 1, 1, 1, 1, 1);
        var rightValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var interval = new TimeGranularInterval(leftValue, rightValue);

        var actual = interval.MoveByLength(1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void Move_WhenNegativeOne_ReturnPrev()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue);

        var actual = interval.MoveByLength(-1);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
    }

    [Test]
    public void ExpandLeft_WhenOne_ReturnIntervalWithLeftAddition()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue);

        var actual = interval.MoveLeft(1);

        actual.LeftValue.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void ExpandLeft_WhenNegativeOne_ReturnEmpty()
    {
        var leftValue = new DateTime(2022, 1, 4, 5, 6, 7);
        var rightValue = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(leftValue, rightValue);

        var actual = interval.MoveLeft(-1);

        actual.LeftValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
        actual.RightValue.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
    }

    [Test]
    public void ExpandRight_WhenOne_ReturnIntervalWithRightAddition()
    {
        var left = new DateTime(2022, 1, 4, 5, 6, 7);
        var right = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(left, right);

        var actual = interval.MoveByGranule(1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2024, 1, 10, 13, 16, 19));
    }

    [Test]
    public void ExpandRight_WhenNegativeOne_ReturnEmpty()
    {
        var left = new DateTime(2022, 1, 4, 5, 6, 7);
        var right = new DateTime(2023, 1, 7, 9, 11, 13);
        var interval = new TimeGranularInterval(left, right);

        var actual = interval.MoveByGranule(-1);

        actual.LeftValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
        actual.RightValue.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
    }

    [Test]
    public void Serialize_WhenSystemTextJson_ShouldNotThrowException()
    {
        var interval = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1));

        var action = new Action(() => JsonSerializer.Serialize(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenSystemTextJson_ShouldNotThrowException()
    {
        const string str =
            "{\"LeftValue\":\"2022-01-01T00:00:00\",\"RightValue\":\"2023-01-01T00:00:00\",\"Inclusion\":2}";

        var action = new Action(() => JsonSerializer.Deserialize<TimeGranularInterval>(str));

        action.Should().NotThrow();
    }

    [Test]
    public void Serialize_WhenNewtonsoftJson_ShouldNotThrowException()
    {
        var interval = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1));

        var action = new Action(() => JsonConvert.SerializeObject(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenNewtonsoftJson_ShouldNotThrowException()
    {
        const string str =
            "{\"LeftValue\":\"2022-01-01T00:00:00\",\"RightValue\":\"2023-01-01T00:00:00\",\"Inclusion\":2}";

        var action = new Action(() => JsonConvert.DeserializeObject<TimeGranularInterval>(str));

        action.Should().NotThrow();
    }

    [Test]
    public void Serialize_WhenSystemTextJsonAndExplicitGranuleSize_ShouldNotThrowException()
    {
        var interval =
            new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1), TimeSpan.FromDays(3));

        var action = new Action(() => JsonSerializer.Serialize(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenSystemTextJsonAndExplicitGranuleSize_ShouldNotThrowException()
    {
        const string str =
            "{\"GranuleSize\":\"3.00:00:00\",\"LeftValue\":\"2022-01-01T00:00:00\",\"RightValue\":\"2023-01-01T00:00:00\",\"Inclusion\":2}";

        var action = new Action(() => JsonSerializer.Deserialize<TimeGranularInterval>(str));

        action.Should().NotThrow();
    }

    [Test]
    public void Serialize_WhenNewtonsoftJsonAndExplicitGranuleSize_ShouldNotThrowException()
    {
        var interval =
            new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1), TimeSpan.FromDays(3));

        var action = new Action(() => JsonConvert.SerializeObject(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenNewtonsoftJsonAndExplicitGranuleSize_ShouldNotThrowException()
    {
        const string str =
            "{\"GranuleSize\":\"3.00:00:00\",\"LeftValue\":\"2022-01-01T00:00:00\",\"RightValue\":\"2023-01-01T00:00:00\",\"Inclusion\":2}";

        var action = new Action(() => JsonConvert.DeserializeObject<TimeGranularInterval>(str));

        action.Should().NotThrow();
    }
}