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

namespace Intervals.Test;

public class IntervalTests
{
	private Mock<IEndpoint<int>> _leftEndpoint = null!;
	private Mock<IEndpoint<int>> _rightEndpoint = null!;
	private Mock<Interval<int>> _firstInterval = null!;
	private Mock<Interval<int>> _secondInterval = null!;

	[SetUp]
	public void Init()
	{
		_leftEndpoint = new Mock<IEndpoint<int>>();
		_rightEndpoint = new Mock<IEndpoint<int>>();
		_firstInterval = new Mock<Interval<int>>(Point.Excluded(0), Point.Excluded(0)) { CallBase = true };
		_firstInterval.Setup(i => i.Left).Returns(() => _leftEndpoint.Object);
		_firstInterval.Setup(i => i.Right).Returns(() => _rightEndpoint.Object);
		_secondInterval = new Mock<Interval<int>>(Point.Excluded(0), Point.Excluded(0)) { CallBase = true };
		_secondInterval.Setup(i => i.Left).Returns(() => _leftEndpoint.Object);
		_secondInterval.Setup(i => i.Right).Returns(() => _rightEndpoint.Object);
	}

	[Test]
	public void New_WhenGivenPoints_ReturnIntervalWithPoints()
	{
		var leftPoint = new Mock<IPoint<int>>().Object;
		var rightPoint = new Mock<IPoint<int>>().Object;

		var actual = Interval.New(leftPoint, rightPoint);

		actual.Left.Location.Should().Be(EndpointLocation.Left);
		actual.Left.Should().BeEquivalentTo(leftPoint);
		actual.Right.Location.Should().Be(EndpointLocation.Right);
		actual.Right.Should().BeEquivalentTo(rightPoint);
	}

	[Test]
	public void New_WhenGivenValuesAndIntervalInclusion_ReturnIntervalWithValuesAndIntervalInclusion()
	{
		const int leftValue = 0;
		const int rightValue = 1;
		const IntervalInclusion intervalInclusion = IntervalInclusion.Opened;

		var actual = Interval.New(leftValue, rightValue, intervalInclusion);

		actual.Left.Location.Should().Be(EndpointLocation.Left);
		actual.Left.Value.Should().Be(leftValue);
		actual.Right.Location.Should().Be(EndpointLocation.Right);
		actual.Right.Value.Should().Be(rightValue);
		actual.Inclusion.Should().Be(intervalInclusion);
	}

	[Test]
	public void Equals_WhenHasEqualPoints_ReturnTrue()
	{
		_leftEndpoint.Setup(p => p.Equals(It.IsAny<IEndpoint<int>>())).Returns(true);
		_rightEndpoint.Setup(p => p.Equals(It.IsAny<IEndpoint<int>>())).Returns(true);

		var actual = ((IInterval<int>)_firstInterval.Object).Equals(_secondInterval.Object);

		actual.Should().BeTrue();
	}

	[Test]
	public void Equals_WhenHasNotEqualPoints_ReturnFalse()
	{
		_leftEndpoint.Setup(p => p.Equals(It.IsAny<IEndpoint<int>>())).Returns(false);
		_rightEndpoint.Setup(p => p.Equals(It.IsAny<IEndpoint<int>>())).Returns(false);

		var actual = _firstInterval.Object.Equals(_secondInterval.Object);

		actual.Should().BeFalse();
	}

	[Test]
	public void CompareTo_WhenLeftLess_ReturnLessZero()
	{
		_leftEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(-1);
		_rightEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(0);

		var actual = _firstInterval.Object.CompareTo(_secondInterval.Object);

		actual.Should().Be(-1);
	}

	[Test]
	public void CompareTo_WhenLeftMore_ReturnMoreZero()
	{
		_leftEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(1);
		_rightEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(0);

		var actual = _firstInterval.Object.CompareTo(_secondInterval.Object);

		actual.Should().Be(1);
	}

	[Test]
	public void CompareTo_WhenLeftEqualsAndRightLess_ReturnLessZero()
	{
		_leftEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(0);
		_rightEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(-1);

		var actual = _firstInterval.Object.CompareTo(_secondInterval.Object);

		actual.Should().Be(-1);
	}

	[Test]
	public void CompareTo_WhenLeftEqualsAndRightMore_ReturnMoreZero()
	{
		_leftEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(0);
		_rightEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(1);

		var actual = _firstInterval.Object.CompareTo(_secondInterval.Object);

		actual.Should().Be(1);
	}

	[Test]
	public void CompareTo_WhenLeftAndRightEquals_ReturnZero()
	{
		_leftEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(0);
		_rightEndpoint.Setup(p => p.CompareTo(It.IsAny<IEndpoint<int>>())).Returns(0);

		var actual = _firstInterval.Object.CompareTo(_secondInterval.Object);

		actual.Should().Be(0);
	}
}