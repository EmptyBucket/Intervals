using System.Collections;
using NUnit.Framework;

namespace Intervals.Tests;

public class MonthsGranularIntervalGetPrevData : IEnumerable
{
	public IEnumerator GetEnumerator()
	{
		yield return new TestCaseData(
				new DateTime(2021, 2, 1), new DateTime(2021, 3, 1),
				new DateTime(2021, 1, 1), new DateTime(2021, 2, 1))
			.SetName("WhenMonth_ReturnPrevMonth");
		yield return new TestCaseData(
				new DateTime(2021, 3, 1), new DateTime(2021, 5, 1),
				new DateTime(2021, 1, 1), new DateTime(2021, 3, 1))
			.SetName("WhenMonths_ReturnPrevMonths");
		yield return new TestCaseData(
				new DateTime(2022, 1, 1), new DateTime(2023, 1, 1),
				new DateTime(2021, 1, 1), new DateTime(2022, 1, 1))
			.SetName("WhenYear_ReturnPrevYear");
		yield return new TestCaseData(
				new DateTime(2022, 3, 1), new DateTime(2023, 5, 1),
				new DateTime(2021, 1, 1), new DateTime(2022, 3, 1))
			.SetName("WhenYearAndMonths_ReturnPrevYearAndMonths");
		yield return new TestCaseData(
				new DateTime(2022, 3, 1), new DateTime(2024, 5, 1),
				new DateTime(2020, 1, 1), new DateTime(2022, 3, 1))
			.SetName("WhenYearsAndMonths_ReturnPrevYearsAndMonths");
	}
}