using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class SymmetricDifferenceEnumerableTests
{
    public static IEnumerable SymmetricDifference_WhenTwoIntervals_Data()
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
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                    new Interval<int>(1, 2, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointIncluded_ReturnBothWithoutPoint");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(1, 2, IntervalInclusion.Opened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened), new Interval<int>(1, 2, IntervalInclusion.Opened)
                })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointExcluded_ReturnBoth");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                new Interval<int>(1, 2, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 2, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointWithDifferentInclusion_ReturnConnected");
        yield return new TestCaseData(
                new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened), new Interval<int>(2, 3, IntervalInclusion.Opened)
                })
            .SetName("WhenTwoIntervalsAndTheyOverlapForSomeInterval_ReturnDifference");
        yield return new TestCaseData(
                new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened), new Interval<int>(3, 4, IntervalInclusion.Closed)
                })
            .SetName("WhenTwoIntervalsAndFirstIntervalContainsSecond_ReturnDifference");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(2, 3, IntervalInclusion.Opened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened), new Interval<int>(2, 3, IntervalInclusion.Opened)
                })
            .SetName("WhenTwoIntervalsAndTheyDontIntersect_ReturnBoth");
    }

    public static IEnumerable SymmetricDifference_WhenManyIntervals_Data()
    {
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                    new Interval<int>(2, 3, IntervalInclusion.Closed),
                    new Interval<int>(4, 5, IntervalInclusion.RightOpened)
                },
                new[]
                {
                    new Interval<int>(1, 2, IntervalInclusion.Closed),
                    new Interval<int>(3, 4, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 2, IntervalInclusion.RightOpened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(3, 4, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.Opened)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByOneEndpointOnly_ReturnSymmetricDifference");
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.Opened)
                },
                new[]
                {
                    new Interval<int>(1, 2, IntervalInclusion.Opened),
                    new Interval<int>(3, 4, IntervalInclusion.Opened)
                },
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened),
                    new Interval<int>(1, 2, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(3, 4, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.Opened)
                })
            .SetName("WhenManyIntervalsAndTheyDontIntersect_ReturnAll");
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
                    new Interval<int>(3, 3, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                    new Interval<int>(1, 2, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.Closed)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByIncludedEndpointOnly_ReturnSymmetricDifference");
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
                    new Interval<int>(3, 3, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 3, IntervalInclusion.LeftOpened),
                    new Interval<int>(4, 5, IntervalInclusion.Opened)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByExcludedEndpointOnly_ReturnSymmetricDifference");
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
                    new Interval<int>(5, 6, IntervalInclusion.Closed),
                    new Interval<int>(7, 8, IntervalInclusion.LeftOpened),
                    new Interval<int>(9, 15, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeIntervals_ReturnSymmetricDifference");
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 5, IntervalInclusion.RightOpened),
                    new Interval<int>(6, 10, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(4, 5, IntervalInclusion.Closed),
                    new Interval<int>(6, 8, IntervalInclusion.LeftOpened)
                },
                new[]
                {
                    new Interval<int>(0, 4, IntervalInclusion.RightOpened),
                    new Interval<int>(5, 5, IntervalInclusion.Closed),
                    new Interval<int>(6, 6, IntervalInclusion.Closed),
                    new Interval<int>(8, 10, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeDividedIntervalAndHaveACommonGap_ReturnSymmetricDifference");
    }
}