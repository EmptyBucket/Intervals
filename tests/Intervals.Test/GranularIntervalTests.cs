using FluentAssertions;
using Intervals.GranularIntervals;
using Intervals.Intervals;
using Moq;
using NUnit.Framework;

namespace Intervals.Test;

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
		var fooInterval = new GranularInterval(_leftPoint.Object, _rightPoint.Object);

		var actual = fooInterval.GetNext();

		actual.Left.Value.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
		actual.Right.Value.Should().Be(new DateTime(2023, 1, 7, 9, 11, 13));
	}

	[Test]
	public void GetPrev__ReturnPrev()
	{
		_leftPoint.Setup(p => p.Value).Returns(new DateTime(2022, 1, 4, 5, 6, 7));
		_rightPoint.Setup(p => p.Value).Returns(new DateTime(2023, 1, 7, 9, 11, 13));
		var fooInterval = new GranularInterval(_leftPoint.Object, _rightPoint.Object);

		var actual = fooInterval.GetPrev();

		actual.Left.Value.Should().Be(new DateTime(2021, 1, 1, 1, 1, 1));
		actual.Right.Value.Should().Be(new DateTime(2022, 1, 4, 5, 6, 7));
	}
}