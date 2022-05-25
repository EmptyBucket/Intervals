using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test;

public class SingleIntervalsOverlapData : IEnumerable
{
	public IEnumerator GetEnumerator()
	{
		yield return new TestCaseData(
				Interval.New(0, -1, IntervalInclusion.Closed),
				Interval.New(0, -1, IntervalInclusion.Closed),
				Array.Empty<IInterval<int>>())
			.SetName("AndBothAreEmpty_ReturnEmptyCollection");
		yield return new TestCaseData(
				Interval.New(0, 1, IntervalInclusion.Closed),
				Interval.New(0, -1, IntervalInclusion.Closed),
				Array.Empty<IInterval<int>>())
			.SetName("AndOneOfThemIsEmpty_ReturnEmptyCollection");
		yield return new TestCaseData(
				Interval.New(0, 1, IntervalInclusion.Closed),
				Interval.New(0, 1, IntervalInclusion.Closed),
				new[] {Interval.New(0, 1, IntervalInclusion.Closed)})
			.SetName("AndTheyAreEqual_ReturnIt");
		yield return new TestCaseData(
				Interval.New(0, 1, IntervalInclusion.Closed),
				Interval.New(1, 2, IntervalInclusion.Closed),
				new[] {Interval.New(1, 1, IntervalInclusion.Closed)})
			.SetName("AndTheyShareSameEndpointIncluded_ReturnPoint");
		yield return new TestCaseData(
				Interval.New(0, 1, IntervalInclusion.Opened),
				Interval.New(1, 2, IntervalInclusion.Opened),
				Array.Empty<Interval<int>>())
			.SetName("AndTheyShareSameEndpointExcluded_ReturnEmptyCollection");
		yield return new TestCaseData(
				Interval.New(0, 1, IntervalInclusion.LeftOpened),
				Interval.New(1, 2, IntervalInclusion.LeftOpened),
				Array.Empty<Interval<int>>())
			.SetName("AndTheyShareSameEndpointWithDifferentInclusion_ReturnEmptyCollection");
		yield return new TestCaseData(
				Interval.New(0, 2, IntervalInclusion.LeftOpened),
				Interval.New(1, 3, IntervalInclusion.RightOpened),
				new[] {Interval.New(1, 2, IntervalInclusion.Closed)})
			.SetName("AndTheyOverlapForSomeInterval_ReturnInterval");
		yield return new TestCaseData(
				Interval.New(0, 4, IntervalInclusion.LeftOpened),
				Interval.New(1, 3, IntervalInclusion.RightOpened),
				new[] {Interval.New(1, 3, IntervalInclusion.RightOpened)})
			.SetName("AndFirstIntervalContainsSecond_ReturnSecond");
		yield return new TestCaseData(
				Interval.New(0, 1, IntervalInclusion.Opened),
				Interval.New(2, 3, IntervalInclusion.Opened),
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
					Interval.New(0, 1, IntervalInclusion.Closed),
					Interval.New(2, 3, IntervalInclusion.Closed),
					Interval.New(4, 5, IntervalInclusion.Closed)
				},
				new[]
				{
					Interval.New(1, 2, IntervalInclusion.Closed),
					Interval.New(3, 3, IntervalInclusion.Closed)
				},
				new[]
				{
					Interval.New(1, 1, IntervalInclusion.Closed),
					Interval.New(2, 2, IntervalInclusion.Closed),
					Interval.New(3, 3, IntervalInclusion.Closed)
				})
			.SetName("AndAllMightOverlapByIncludedEndpointOnly_ReturnPointsCollection");
		yield return new TestCaseData(
				new[]
				{
					Interval.New(0, 1, IntervalInclusion.Opened),
					Interval.New(2, 3, IntervalInclusion.Opened),
					Interval.New(4, 5, IntervalInclusion.Opened)
				},
				new[]
				{
					Interval.New(1, 2, IntervalInclusion.Closed),
					Interval.New(3, 3, IntervalInclusion.Closed)
				},
				Array.Empty<Interval<int>>())
			.SetName("AndAllMightOverlapByExcludedEndpointOnly_ReturnEmptyCollection");
		yield return new TestCaseData(
				new[]
				{
					Interval.New(0, 5, IntervalInclusion.Opened),
					Interval.New(6, 11, IntervalInclusion.Opened)
				},
				new[]
				{
					Interval.New(4, 7, IntervalInclusion.Closed),
					Interval.New(8, 9, IntervalInclusion.LeftOpened),
					Interval.New(11, 15, IntervalInclusion.Closed)
				},
				new[]
				{
					Interval.New(4, 5, IntervalInclusion.RightOpened),
					Interval.New(6, 7, IntervalInclusion.LeftOpened),
					Interval.New(8, 9, IntervalInclusion.LeftOpened)
				})
			.SetName("AndMightOverlapBySomeIntervals_ReturnIntervalsCollection");
	}
}