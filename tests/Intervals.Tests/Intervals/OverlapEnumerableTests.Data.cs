using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class OverlapEnumerableTests
{
    public static IEnumerable Overlap_WhenTwoIntervals_Data()
    {
        yield return new TestCaseData(
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                Array.Empty<IInterval<int>>())
            .SetName("WhenTwoIntervalsAndBothAreEmpty_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                Array.Empty<IInterval<int>>())
            .SetName("WhenTwoIntervalsAndSecondIsEmpty_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyAreEqual_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(1, 2, IntervalInclusion.Closed),
                new[] { new Interval<int>(1, 1, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointIncluded_ReturnPoint");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(1, 2, IntervalInclusion.Opened),
                Array.Empty<Interval<int>>())
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointExcluded_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                Array.Empty<Interval<int>>())
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointWithDifferentInclusion_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(1, 2, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyOverlapForSome_ReturnOverlap");
        yield return new TestCaseData(
                new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(1, 3, IntervalInclusion.RightOpened) })
            .SetName("WhenTwoIntervalsAndFirstContainsSecond_ReturnSecond");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(2, 3, IntervalInclusion.Opened),
                Array.Empty<Interval<int>>())
            .SetName("WhenTwoIntervalsAndTheyDontIntersect_ReturnEmpty");
    }

    public static IEnumerable Overlap_WhenManyIntervals_Data()
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
                    new Interval<int>(3, 3, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(1, 1, IntervalInclusion.Closed),
                    new Interval<int>(2, 2, IntervalInclusion.Closed),
                    new Interval<int>(3, 3, IntervalInclusion.Closed)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByIncludedEndpointOnly_ReturnOverlapped");
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
                Array.Empty<Interval<int>>())
            .SetName("WhenManyIntervalsAndAllMightOverlapByExcludedEndpointOnly_ReturnEmpty");
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
                    new Interval<int>(4, 5, IntervalInclusion.RightOpened),
                    new Interval<int>(6, 7, IntervalInclusion.LeftOpened),
                    new Interval<int>(8, 9, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeIntervals_ReturnOverlapped");
    }
}