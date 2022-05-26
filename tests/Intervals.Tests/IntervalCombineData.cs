using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Tests;

public class SingleIntervalsCombineData : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new TestCaseData(
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                Array.Empty<IInterval<int>>())
            .SetName("AndBothAreEmpty_ReturnEmptyCollection");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("AndOneOfThemIsEmpty_ReturnOther");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("AndTheyAreEqual_ReturnIt");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(1, 2, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 2, IntervalInclusion.Closed) })
            .SetName("AndTheyShareSameEndpointIncluded_ReturnConnectedInterval");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                    new Interval<int>(1, 2, IntervalInclusion.LeftOpened)
                })
            .SetName("AndTheyShareSameEndpointExcluded_ReturnIntervalsCollection");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                new[] { new Interval<int>(0, 2, IntervalInclusion.LeftOpened) })
            .SetName("AndTheyShareSameEndpointWithDifferentInclusion_ReturnEmptyCollection");
        yield return new TestCaseData(
                new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(0, 3, IntervalInclusion.Opened) })
            .SetName("AndTheyOverlapForSomeInterval_ReturnInterval");
        yield return new TestCaseData(
                new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(0, 4, IntervalInclusion.LeftOpened) })
            .SetName("AndFirstIntervalContainsSecond_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(2, 3, IntervalInclusion.Opened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened)
                })
            .SetName("AndTheyDontIntersect_ReturnThisIntervalsCollection");
    }
}

public class MultipleIntervalsCombineData : IEnumerable
{
    public IEnumerator GetEnumerator()
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
            .SetName("AndAllMightOverlapByOneEndpointOnly_ReturnSingleIntervalCollection");
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
            .SetName("AndTheyDontIntersect_ReturnAllIntervalsCollection");
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
            .SetName("AndMightOverlapBySomeIntervals_ReturnIntervalsCollection");
    }
}