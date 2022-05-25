using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test;

public class SingleIntervalsOverlapData : IEnumerable
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
                Array.Empty<IInterval<int>>())
            .SetName("AndOneOfThemIsEmpty_ReturnEmptyCollection");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("AndTheyAreEqual_ReturnIt");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(1, 2, IntervalInclusion.Closed),
                new[] { new Interval<int>(1, 1, IntervalInclusion.Closed) })
            .SetName("AndTheyShareSameEndpointIncluded_ReturnPoint");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(1, 2, IntervalInclusion.Opened),
                Array.Empty<Interval<int>>())
            .SetName("AndTheyShareSameEndpointExcluded_ReturnEmptyCollection");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                Array.Empty<Interval<int>>())
            .SetName("AndTheyShareSameEndpointWithDifferentInclusion_ReturnEmptyCollection");
        yield return new TestCaseData(
                new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(1, 2, IntervalInclusion.Closed) })
            .SetName("AndTheyOverlapForSomeInterval_ReturnInterval");
        yield return new TestCaseData(
                new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(1, 3, IntervalInclusion.RightOpened) })
            .SetName("AndFirstIntervalContainsSecond_ReturnSecond");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(2, 3, IntervalInclusion.Opened),
                Array.Empty<Interval<int>>())
            .SetName("AndTheyDontIntersect_ReturnEmptyCollection");
    }
}

public class MultipleIntervalsOverlapData : IEnumerable
{
    public IEnumerator GetEnumerator()
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
            .SetName("AndAllMightOverlapByIncludedEndpointOnly_ReturnPointsCollection");
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
            .SetName("AndAllMightOverlapByExcludedEndpointOnly_ReturnEmptyCollection");
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
            .SetName("AndMightOverlapBySomeIntervals_ReturnIntervalsCollection");
    }
}