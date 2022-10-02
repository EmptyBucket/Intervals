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

public class GenericMathTests
{
    [Test]
    public void Max_WhenFirstMoreThanSecond_ReturnFirst()
    {
        var first = new DateTime(2022, 1, 2);
        var second = new DateTime(2022, 1, 1);

        var actual = GenericMath.Max(first, second);

        actual.Should().Be(first);
    }

    [Test]
    public void Max_WhenFirstEqualsSecond_ReturnFirst()
    {
        var first = new DateTime(2022, 1, 1);
        var second = new DateTime(2022, 1, 1);

        var actual = GenericMath.Max(first, second);

        actual.Should().Be(first);
    }

    [Test]
    public void Max_WhenFirstLessThanSecond_ReturnSecond()
    {
        var first = new DateTime(2022, 1, 1);
        var second = new DateTime(2022, 1, 2);

        var actual = GenericMath.Max(first, second);

        actual.Should().Be(second);
    }

    [Test]
    public void Min_WhenFirstMoreThanSecond_ReturnSecond()
    {
        var first = new DateTime(2022, 1, 2);
        var second = new DateTime(2022, 1, 1);

        var actual = GenericMath.Min(first, second);

        actual.Should().Be(second);
    }

    [Test]
    public void Min_WhenFirstEqualsSecond_ReturnFirst()
    {
        var first = new DateTime(2022, 1, 1);
        var second = new DateTime(2022, 1, 1);

        var actual = GenericMath.Min(first, second);

        actual.Should().Be(first);
    }

    [Test]
    public void Min_WhenFirstLessThanSecond_ReturnFirst()
    {
        var first = new DateTime(2022, 1, 1);
        var second = new DateTime(2022, 1, 2);

        var actual = GenericMath.Min(first, second);

        actual.Should().Be(first);
    }
}