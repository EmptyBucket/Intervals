using FluentAssertions;
using NUnit.Framework;
using PeriodNet.Intervals;

namespace PeriodNet.Test
{
	public class InclusionTests
	{
		[Test]
		public void Invert_WhenIncluded_ReturnExcluded()
		{
			const Inclusion inclusion = Inclusion.Included;

			var actual = inclusion.Invert();

			actual.Should().Be(Inclusion.Excluded);
		}

		[Test]
		public void Invert_WhenExcluded_ReturnIncluded()
		{
			const Inclusion inclusion = Inclusion.Excluded;

			var actual = inclusion.Invert();

			actual.Should().Be(Inclusion.Included);
		}
	}
}