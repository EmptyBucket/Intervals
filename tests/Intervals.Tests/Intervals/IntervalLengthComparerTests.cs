using FluentAssertions;
using Intervals.Intervals;
using Intervals.Utils;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class IntervalLengthComparerTests
{
    [TestCaseSource(nameof(Compare_WhenDateTimeInterval_Data))]
    public void Compare_WhenDateTimeInterval(Interval<DateTime>? first, Interval<DateTime>? second, int result)
    {
        var defaultDateTimeIntervalLengthComparer = new DefaultDateTimeIntervalLengthComparer();

        var compareResult = defaultDateTimeIntervalLengthComparer.Compare(first, second);

        compareResult.Should().Be(result);
    }

    [TestCaseSource(nameof(Compare_WhenIntInterval_Data))]
    public void Compare_WhenIntInterval(Interval<int>? first, Interval<int>? second, int result)
    {
        var defaultIntervalLengthComparer = new DefaultIntervalLengthComparer<int, int>();

        var compareResult = defaultIntervalLengthComparer.Compare(first, second);

        compareResult.Should().Be(result);
    }

    [TestCaseSource(nameof(Compare_WhenDateTimeIntervalWithCustomComparison_Data))]
    public void Compare_WhenDateTimeIntervalWithCustomComparison(Interval<DateTime>? first, Interval<DateTime>? second, int result)
    {
        var intervalLengthComparer = new IntervalLengthComparer<DateTime, int>(GetWorkingDays);

        var compareResult = intervalLengthComparer.Compare(first, second);

        compareResult.Should().Be(result);
    }

    // imitates some logic of computation length
    private static int GetWorkingDays(Interval<DateTime> interval)
    {
        if (interval.IsEmpty()) return 0;

        var (start, end, _) = interval;
        var year = start.Year;
        var month = start.Month;
        var endOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        var endOfWorkingPeriod = GenericMath.Min(endOfMonth, end);
        return (endOfWorkingPeriod - start).Days;
    }
}