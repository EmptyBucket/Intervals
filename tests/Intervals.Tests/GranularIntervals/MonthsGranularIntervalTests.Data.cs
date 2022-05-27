using System.Collections;
using NUnit.Framework;

namespace Intervals.Tests.GranularIntervals;

public partial class MonthGranularIntervalTests
{
    public static IEnumerable GetPrev_Data()
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

    public static IEnumerable GetNext_Data()
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