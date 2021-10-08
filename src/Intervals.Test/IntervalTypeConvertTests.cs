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

namespace Intervals.Test
{
	public class IntervalTypeConvertTests
	{
		[TestCase(Inclusion.Excluded, Inclusion.Excluded, IntervalType.Open)]
		[TestCase(Inclusion.Excluded, Inclusion.Included, IntervalType.LeftOpen)]
		[TestCase(Inclusion.Included, Inclusion.Excluded, IntervalType.RightOpen)]
		[TestCase(Inclusion.Included, Inclusion.Included, IntervalType.Closed)]
		public void FromInclusion_WhenGivenInclusions_ReturnIntervalType(Inclusion left, Inclusion right, IntervalType result)
		{
			var actual = IntervalTypeConvert.FromInclusions(left, right);

			actual.Should().Be(result);
		}

		[TestCase(IntervalType.Open, Inclusion.Excluded, Inclusion.Excluded)]
		[TestCase(IntervalType.LeftOpen, Inclusion.Excluded, Inclusion.Included)]
		[TestCase(IntervalType.RightOpen, Inclusion.Included, Inclusion.Excluded)]
		[TestCase(IntervalType.Closed, Inclusion.Included, Inclusion.Included)]
		public void ToInclusions_WhenGivenIntervalType_ReturnInclusions(IntervalType intervalType,
			Inclusion left, Inclusion right)
		{
			var (actualLeft, actualRight) = IntervalTypeConvert.ToInclusions(intervalType);

			actualLeft.Should().Be(left);
			actualRight.Should().Be(right);
		}
	}
}