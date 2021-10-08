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
using Intervals.Intervals;
using Moq;
using NUnit.Framework;

namespace Intervals.Test
{
	public class GranularIntervalTests
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

		[Test]
		public void GetNext__ReturnNext()
		{
			_leftPoint.Setup(p => p.Value).Returns(new DateTime(2021, 1, 1, 1, 1, 1));
			_rightPoint.Setup(p => p.Value).Returns(new DateTime(2022, 1, 4, 5, 6, 7));
			var fooInterval = new FooGranularInterval(_leftPoint.Object, _rightPoint.Object);

			var actual = fooInterval.GetNext();

			actual.Left.Value.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
			actual.Right.Value.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
		}

		[Test]
		public void GetPrev__ReturnPrev()
		{
			_leftPoint.Setup(p => p.Value).Returns(new DateTime(2022, 1, 4, 5, 6, 7));
			_rightPoint.Setup(p => p.Value).Returns(new DateTime(2023, 1, 7, 9, 11, 13));
			var fooInterval = new FooGranularInterval(_leftPoint.Object, _rightPoint.Object);

			var actual = fooInterval.GetPrev();

			actual.Left.Value.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
			actual.Right.Value.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
		}
	}
}