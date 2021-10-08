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
using Moq;
using NUnit.Framework;

namespace Intervals.Test
{
	public class EndpointTests
	{
		private Mock<IPoint<int>> _firstPoint = null!;
		private Mock<IPoint<int>> _secondPoint = null!;

		[SetUp]
		public void Init()
		{
			_firstPoint = new Mock<IPoint<int>>();
			_secondPoint = new Mock<IPoint<int>>();
		}

		[Test]
		[TestCase(EndpointLocation.Left)]
		[TestCase(EndpointLocation.Right)]
		public void New_WhenGivenLocation_ReturnEndpointWithLocation(EndpointLocation endpointLocation)
		{
			var point = _firstPoint.Object;

			var actual = Endpoint.New(point, endpointLocation);

			actual.Should().BeEquivalentTo(point);
			actual.Location.Should().Be(endpointLocation);
		}

		[Test]
		public void Equals_WhenHasSameMembers_ReturnTrue()
		{
			_firstPoint.Setup(p => p.Equals(It.IsAny<IPoint<int>>())).Returns(true);
			var first = Endpoint.New(_firstPoint.Object, EndpointLocation.Left);
			var second = Endpoint.New(_secondPoint.Object, EndpointLocation.Left);

			var actual = first.Equals(second);

			actual.Should().BeTrue();
		}

		[Test]
		public void Equals_WhenHasOtherLocation_ReturnFalse()
		{
			_firstPoint.Setup(p => p.Equals(It.IsAny<IPoint<int>>())).Returns(true);
			var first = Endpoint.New(_firstPoint.Object, EndpointLocation.Left);
			var second = Endpoint.New(_secondPoint.Object, EndpointLocation.Right);

			var actual = first.Equals(second);

			actual.Should().BeFalse();
		}

		[Test]
		public void Equals_WhenHasOtherPoint_ReturnFalse()
		{
			_firstPoint.Setup(p => p.Equals(It.IsAny<IPoint<int>>())).Returns(false);
			var first = Endpoint.New(_firstPoint.Object, EndpointLocation.Left);
			var second = Endpoint.New(_secondPoint.Object, EndpointLocation.Left);

			var actual = first.Equals(second);

			actual.Should().BeFalse();
		}

		[Test]
		public void CompareTo_WhenHasLessValue_ReturnResult()
		{
			_firstPoint.Setup(p => p.Value).Returns(-1);
			_secondPoint.Setup(p => p.Value).Returns(0);
			var first = Endpoint.New(_firstPoint.Object, EndpointLocation.Left);
			var second = Endpoint.New(_secondPoint.Object, EndpointLocation.Left);

			var actual = first.CompareTo(second);

			actual.Should().Be(-1);
		}

		[TestCaseSource(typeof(EndpointCompareToSameValueData))]
		public void CompareTo_WhenHasSameValue(IEndpoint<int> first, IEndpoint<int> second, int result)
		{
			var actual = first.CompareTo(second);

			actual.Should().Be(result);
		}

		[Test]
		public void CompareTo_WhenHasMoreValue_ReturnResult()
		{
			_firstPoint.Setup(p => p.Value).Returns(1);
			_secondPoint.Setup(p => p.Value).Returns(0);
			var first = Endpoint.New(_firstPoint.Object, EndpointLocation.Left);
			var second = Endpoint.New(_secondPoint.Object, EndpointLocation.Left);

			var actual = first.CompareTo(second);

			actual.Should().Be(1);
		}
	}
}