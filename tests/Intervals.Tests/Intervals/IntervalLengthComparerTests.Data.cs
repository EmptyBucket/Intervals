using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class IntervalLengthComparerTests
{
    public static IEnumerable Compare_WhenDateTimeInterval_Data()
    {
        yield return new TestCaseData(null, null, 0)
            .SetName("WhenDateIntervalsAreNull_ReturnZero");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                0)
            .SetName("WhenIntervalsAreSame_ReturnZero");

        yield return new TestCaseData(
                null,
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                -1)
            .SetName("WhenFirstDateTimeIntervalIsNull_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                null,
                1)
            .SetName("WhenSecondDateTimeIntervalIsNull_ReturnOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2024, 1, 28), new DateTime(2023, 1, 28)),
                0)
            .SetName("WhenIntervalsAreEmpty_ReturnZero");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2022, 1, 28), new DateTime(2023, 1, 28)),
                -1)
            .SetName("WhenFirstIntervalIsEmpty_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                1)
            .SetName("WhenSecondIntervalIsEmpty_ReturnOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 27), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 20), new DateTime(2023, 1, 28)),
                -1)
            .SetName("WhenFirstIntervalIsSmaller_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 20), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 27), new DateTime(2023, 1, 28)),
                1)
            .SetName("WhenSecondIntervalIsSmaller_ReturnOne");
    }

    public static IEnumerable Compare_WhenIntInterval_Data()
    {
        yield return new TestCaseData(null, null, 0)
            .SetName("WhenIntIntervalsAreNull_ReturnZero");

        yield return new TestCaseData(
                new Interval<int>(20, 21),
                new Interval<int>(20, 21),
                0)
            .SetName("WhenIntervalsAreSame_ReturnZero");

        yield return new TestCaseData(
                null,
                new Interval<int>(20, 21),
                -1)
            .SetName("WhenFirstIntIntervalIsNull_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<int>(20, 21),
                null,
                1)
            .SetName("WhenSecondIntIntervalIsNull_ReturnOne");

        yield return new TestCaseData(
                new Interval<int>(20, 20),
                new Interval<int>(22, 21),
                0)
            .SetName("WhenIntervalsAreEmpty_ReturnZero");

        yield return new TestCaseData(
                new Interval<int>(20, 20),
                new Interval<int>(20, 21),
                -1)
            .SetName("WhenFirstIntervalIsEmpty_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<int>(20, 21),
                new Interval<int>(20, 20),
                1)
            .SetName("WhenSecondIntervalIsEmpty_ReturnOne");

        yield return new TestCaseData(
                new Interval<int>(20, 21),
                new Interval<int>(20, 28),
                -1)
            .SetName("WhenFirstIntervalIsSmaller_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<int>(20, 28),
                new Interval<int>(20, 21),
                1)
            .SetName("WhenSecondIntervalIsSmaller_ReturnOne");
    }

    public static IEnumerable Compare_WhenDateTimeIntervalWithCustomComparison_Data()
    {
        yield return new TestCaseData(null, null, 0)
            .SetName("WhenDateIntervalsAreNull_ReturnZero");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                0)
            .SetName("WhenIntervalsAreSame_ReturnZero");

        yield return new TestCaseData(
                null,
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                -1)
            .SetName("WhenFirstDateTimeIntervalIsNull_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                null,
                1)
            .SetName("WhenSecondDateTimeIntervalIsNull_ReturnOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2024, 1, 28), new DateTime(2023, 1, 28)),
                0)
            .SetName("WhenIntervalsAreEmpty_ReturnZero");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2022, 1, 28), new DateTime(2023, 1, 28)),
                -1)
            .SetName("WhenFirstIntervalIsEmpty_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 28), new DateTime(2023, 1, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 28), new DateTime(2023, 1, 28)),
                1)
            .SetName("WhenSecondIntervalIsEmpty_ReturnOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 29), new DateTime(2027, 1, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 27), new DateTime(2023, 2, 28)),
                -1)
            .SetName("WhenFirstIntervalIsSmaller_ReturnNegativeOne");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2023, 1, 27), new DateTime(2023, 2, 28)),
                new Interval<DateTime>(new DateTime(2023, 1, 29), new DateTime(2027, 1, 28)),
                1)
            .SetName("WhenSecondIntervalIsSmaller_ReturnOne");
    }
}