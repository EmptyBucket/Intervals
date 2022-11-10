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
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Points;

public partial class EndpointTests
{
    [Test]
    public void Left_WhenGivenPoint_ReturnLeftEndpoint()
    {
        var point = Point.Excluded(0);

        var endpoint = Endpoint.Left(point);

        endpoint.Value.Should().Be(point.Value);
        endpoint.Inclusion.Should().Be(point.Inclusion);
        endpoint.Location.Should().Be(EndpointLocation.Left);
    }

    [Test]
    public void Left_WhenGivenValueAndInclusion_ReturnLeftEndpoint()
    {
        const int value = 0;
        const Inclusion inclusion = Inclusion.Excluded;

        var endpoint = Endpoint.Left(value, inclusion);

        endpoint.Value.Should().Be(value);
        endpoint.Inclusion.Should().Be(inclusion);
        endpoint.Location.Should().Be(EndpointLocation.Left);
    }

    [Test]
    public void Right_WhenGivenPoint_ReturnRightEndpoint()
    {
        var point = Point.Excluded(0);

        var endpoint = Endpoint.Right(point);

        endpoint.Value.Should().Be(point.Value);
        endpoint.Inclusion.Should().Be(point.Inclusion);
        endpoint.Location.Should().Be(EndpointLocation.Right);
    }

    [Test]
    public void Right_WhenGivenValueAndInclusion_ReturnRightEndpoint()
    {
        const int value = 0;
        const Inclusion inclusion = Inclusion.Excluded;

        var endpoint = Endpoint.Right(value, inclusion);

        endpoint.Value.Should().Be(value);
        endpoint.Inclusion.Should().Be(inclusion);
        endpoint.Location.Should().Be(EndpointLocation.Right);
    }

    [Test]
    public void CompareTo_WhenHasLessValue_NegativeOne()
    {
        var first = Endpoint.Left(Point.Included(-1));
        var second = Endpoint.Left(Point.Included(0));

        var actual = first.CompareTo(second);

        actual.Should().Be(-1);
    }

    [TestCaseSource(nameof(CompareTo_WhenHasSameValue_Data))]
    public void CompareTo_WhenHasSameValue(Endpoint<int> first, Endpoint<int> second, int result)
    {
        var actual = first.CompareTo(second);

        actual.Should().Be(result);
    }

    [Test]
    public void CompareTo_WhenHasMoreValue_PositiveOne()
    {
        var first = Endpoint.Left(Point.Included(1));
        var second = Endpoint.Left(Point.Included(0));

        var actual = first.CompareTo(second);

        actual.Should().Be(1);
    }
}