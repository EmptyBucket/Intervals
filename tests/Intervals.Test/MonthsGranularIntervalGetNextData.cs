using System;
using System.Collections;
using NUnit.Framework;

namespace Intervals.Test
{
	public class MonthsGranularIntervalGetNextData : IEnumerable
	{
		public IEnumerator GetEnumerator()
		{
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2021, 2, 1),
					new DateTime(2021, 2, 1), new DateTime(2021, 3, 1))
				.SetName("WhenMonth_ReturnNextMonth");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2021, 3, 1),
					new DateTime(2021, 3, 1), new DateTime(2021, 5, 1))
				.SetName("WhenMonths_ReturnNextMonths");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2022, 1, 1),
					new DateTime(2022, 1, 1), new DateTime(2023, 1, 1))
				.SetName("WhenYear_ReturnNextYear");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2022, 3, 1),
					new DateTime(2022, 3, 1), new DateTime(2023, 5, 1))
				.SetName("WhenYearAndMonths_ReturnNextYearAndMonths");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2023, 3, 1),
					new DateTime(2023, 3, 1), new DateTime(2025, 5, 1))
				.SetName("WhenYearsAndMonths_ReturnNextYearsAndMonths");
		}
	}
}