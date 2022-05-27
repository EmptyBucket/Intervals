using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class SubtractEnumerableTests
{
    public static IEnumerable Subtract_WhenTwoIntervals_Data()
    {
        yield return new TestCaseData(
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                Array.Empty<IInterval<int>>())
            .SetName("WhenTwoIntervalsAndBothAreEmpty_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndSecondIsEmpty_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                Array.Empty<Interval<int>>())
            .SetName("WhenTwoIntervalsAndTheyAreEqual_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(1, 2, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.RightOpened) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointIncluded_ReturnFirstRightOpened");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(1, 2, IntervalInclusion.Opened),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Opened) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointExcluded_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                new[] { new Interval<int>(0, 1, IntervalInclusion.LeftOpened) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointWithDifferentInclusion_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Opened) })
            .SetName("WhenTwoIntervalsAndTheyOverlapForSome_ReturnSubtract");
        yield return new TestCaseData(
                new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened), new Interval<int>(3, 4, IntervalInclusion.Closed)
                })
            .SetName("WhenTwoIntervalsAndFirstContainsSecond_ReturnSubtract");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(3, 4, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyDontIntersect_ReturnFirst");
    }

    public static IEnumerable Subtract_WhenManyIntervals_Data()
    {
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Closed),
                    new Interval<int>(2, 3, IntervalInclusion.Closed),
                    new Interval<int>(4, 5, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(1, 2, IntervalInclusion.Closed),
                    new Interval<int>(3, 4, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByIncludedEndpointOnly_ReturnSubtracted");
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.Opened)
                },
                new[]
                {
                    new Interval<int>(1, 2, IntervalInclusion.Closed),
                    new Interval<int>(3, 4, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.Opened)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByExcludedEndpointOnly_ReturnFirst");
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 5, IntervalInclusion.Opened),
                    new Interval<int>(6, 11, IntervalInclusion.Opened)
                },
                new[]
                {
                    new Interval<int>(4, 7, IntervalInclusion.Closed),
                    new Interval<int>(8, 9, IntervalInclusion.LeftOpened),
                    new Interval<int>(11, 15, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 4, IntervalInclusion.Opened),
                    new Interval<int>(7, 8, IntervalInclusion.LeftOpened),
                    new Interval<int>(9, 11, IntervalInclusion.Opened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeIntervals_ReturnSubtracted");
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 5, IntervalInclusion.RightOpened),
                    new Interval<int>(5, 10, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(4, 6, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 4, IntervalInclusion.RightOpened),
                    new Interval<int>(6, 10, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeDividedInterval_ReturnSubtracted");
    }
}