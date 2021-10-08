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