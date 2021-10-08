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

using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PeriodNet.Intervals;

namespace PeriodNet.Test
{
	public class MonthsGranularIntervalTests
	{
		private Mock<IPoint<DateTime>> _leftPoint = null!;
		private Mock<IPoint<DateTime>> _rightPoint = null!;

		[SetUp]
		public void Init()
		{
			_leftPoint = new Mock<IPoint<DateTime>>();
			_leftPoint.Setup(p => p.Inclusion).Returns(Inclusion.Excluded);
			_rightPoint = new Mock<IPoint<DateTime>>();
			_rightPoint.Setup(p => p.Inclusion).Returns(Inclusion.Excluded);
		}

		[TestCaseSource(typeof(MonthsGranularIntervalGetNextData))]
		public void GetNext(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
			DateTime expectedRightValue)
		{
			_leftPoint.Setup(p => p.Value).Returns(leftValue);
			_rightPoint.Setup(p => p.Value).Returns(rightValue);
			var fooInterval = new FooInterval(_leftPoint.Object, _rightPoint.Object);

			var actual = fooInterval.GetNext();

			actual.Left.Value.Should().Be(expectedLeftValue);
			actual.Right.Value.Should().Be(expectedRightValue);
		}

		[TestCaseSource(typeof(MonthsGranularIntervalGetPrevData))]
		public void GetPrev(DateTime leftValue, DateTime rightValue, DateTime expectedLeftValue,
			DateTime expectedRightValue)
		{
			_leftPoint.Setup(p => p.Value).Returns(leftValue);
			_rightPoint.Setup(p => p.Value).Returns(rightValue);
			var fooInterval = new FooInterval(_leftPoint.Object, _rightPoint.Object);

			var actual = fooInterval.GetPrev();

			actual.Left.Value.Should().Be(expectedLeftValue);
			actual.Right.Value.Should().Be(expectedRightValue);
		}
	}
}