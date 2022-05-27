using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class CombineEnumerableTests
{
    public static IEnumerable Combine_WhenTwoIntervals_Data()
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
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyAreEqual_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(1, 2, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 2, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointIncluded_ReturnCombined");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                    new Interval<int>(1, 2, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointExcluded_ReturnBoth");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                new[] { new Interval<int>(0, 2, IntervalInclusion.LeftOpened) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointWithDifferentInclusion_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(0, 3, IntervalInclusion.Opened) })
            .SetName("WhenTwoIntervalsAndTheyOverlapForSomeInterval_ReturnCombined");
        yield return new TestCaseData(
                new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(0, 4, IntervalInclusion.LeftOpened) })
            .SetName("WhenTwoIntervalsAndFirstIntervalContainsSecond_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(2, 3, IntervalInclusion.Opened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened)
                })
            .SetName("WhenTwoIntervalsAndTheyDontIntersect_ReturnBoth");
    }

    public static IEnumerable Combine_WhenManyIntervals_Data()
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
                    new Interval<int>(0, 5, IntervalInclusion.RightOpened)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByOneEndpointOnly_ReturnCombined");
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
                    new Interval<int>(0, 10, IntervalInclusion.Opened),
                    new Interval<int>(2, 11, IntervalInclusion.Opened)
                },
                new[]
                {
                    new Interval<int>(0, 7, IntervalInclusion.Closed),
                    new Interval<int>(8, 9, IntervalInclusion.LeftOpened),
                    new Interval<int>(18, 19, IntervalInclusion.LeftOpened)
                },
                new[]
                {
                    new Interval<int>(0, 11, IntervalInclusion.RightOpened),
                    new Interval<int>(18, 19, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeIntervals_ReturnCombined");
    }
}