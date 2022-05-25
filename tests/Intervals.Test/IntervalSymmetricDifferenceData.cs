using System;
using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test
{
	public class SingleIntervalsSymmetricDifferenceData : IEnumerable
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
					new[] {Interval.New(0, 1, IntervalInclusion.Closed)})
				.SetName("AndOneOfThemIsEmpty_ReturnOther");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.Closed),
					Interval.New(0, 1, IntervalInclusion.Closed),
					Array.Empty<Interval<int>>())
				.SetName("AndTheyAreEqual_ReturnEmptyCollection");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.Closed),
					Interval.New(1, 2, IntervalInclusion.Closed),
					new[] {Interval.New(0, 1, IntervalInclusion.RightOpened), Interval.New(1, 2, IntervalInclusion.LeftOpened)})
				.SetName("AndTheyShareSameEndpointIncluded_ReturnIntervalsWithoutThePoint");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.Opened),
					Interval.New(1, 2, IntervalInclusion.Opened),
					new[] {Interval.New(0, 1, IntervalInclusion.Opened), Interval.New(1, 2, IntervalInclusion.Opened)})
				.SetName("AndTheyShareSameEndpointExcluded_ReturnIntervalsCollection");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.RightOpened),
					Interval.New(1, 2, IntervalInclusion.Closed),
					new[] {Interval.New(0, 2, IntervalInclusion.Closed)})
				.SetName("AndTheyShareSameEndpointWithDifferentInclusion_ReturnConnectedInterval");
			yield return new TestCaseData(
					Interval.New(0, 2, IntervalInclusion.LeftOpened),
					Interval.New(1, 3, IntervalInclusion.RightOpened),
					new[] {Interval.New(0, 1, IntervalInclusion.Opened), Interval.New(2, 3, IntervalInclusion.Opened)})
				.SetName("AndTheyOverlapForSomeInterval_ReturnIntervalsCollection");
			yield return new TestCaseData(
					Interval.New(0, 4, IntervalInclusion.LeftOpened),
					Interval.New(1, 3, IntervalInclusion.RightOpened),
					new[] {Interval.New(0, 1, IntervalInclusion.Opened), Interval.New(3, 4, IntervalInclusion.Closed)})
				.SetName("AndFirstIntervalContainsSecond_ReturnDifference");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.Opened),
					Interval.New(2, 3, IntervalInclusion.Opened),
					new[] {Interval.New(0, 1, IntervalInclusion.Opened), Interval.New(2, 3, IntervalInclusion.Opened)})
				.SetName("AndTheyDontIntersect_ReturnThisIntervalsCollection");
		}
	}

	public class MultipleIntervalsSymmetricDifferenceData : IEnumerable
	{
		public IEnumerator GetEnumerator()
		{
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 1, IntervalInclusion.RightOpened),
						Interval.New(2, 3, IntervalInclusion.Closed),
						Interval.New(4, 5, IntervalInclusion.RightOpened)
					},
					new[]
					{
						Interval.New(1, 2, IntervalInclusion.Closed),
						Interval.New(3, 4, IntervalInclusion.Closed)
					},
					new[]
					{
						Interval.New(0, 2, IntervalInclusion.RightOpened),
						Interval.New(2, 3, IntervalInclusion.Opened),
						Interval.New(3, 4, IntervalInclusion.Opened),
						Interval.New(4, 5, IntervalInclusion.Opened) 
					})
				.SetName("AndAllMightOverlapByOneEndpointOnly_ReturnSingleIntervalCollection");
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 1, IntervalInclusion.Opened),
						Interval.New(2, 3, IntervalInclusion.Opened),
						Interval.New(4, 5, IntervalInclusion.Opened)
					},
					new[]
					{
						Interval.New(1, 2, IntervalInclusion.Opened),
						Interval.New(3, 4, IntervalInclusion.Opened)
					},
					new[]
					{
						Interval.New(0, 1, IntervalInclusion.Opened),
						Interval.New(1, 2, IntervalInclusion.Opened),
						Interval.New(2, 3, IntervalInclusion.Opened),
						Interval.New(3, 4, IntervalInclusion.Opened),
						Interval.New(4, 5, IntervalInclusion.Opened)
					})
				.SetName("AndTheyDontIntersect_ReturnAllIntervalsCollection");
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
						Interval.New(0, 1, IntervalInclusion.RightOpened),
						Interval.New(1, 2, IntervalInclusion.Opened),
						Interval.New(2, 3, IntervalInclusion.Opened),
						Interval.New(4, 5, IntervalInclusion.Closed)
					})
				.SetName("AndAllMightOverlapByIncludedEndpointOnly_ReturnIntervalsCollectionWithoutSomeEndpoints");
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
					new[]
					{
						Interval.New(0, 3, IntervalInclusion.LeftOpened),
						Interval.New(4, 5, IntervalInclusion.Opened)
					})
				.SetName("AndAllMightOverlapByExcludedEndpointOnly_ReturnIntervalsCollectionUnchanged");
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
						Interval.New(0, 4, IntervalInclusion.Opened),
						Interval.New(5, 6, IntervalInclusion.Closed),
						Interval.New(7, 8, IntervalInclusion.LeftOpened),
						Interval.New(9, 15, IntervalInclusion.LeftOpened)
					})
				.SetName("AndMightOverlapBySomeIntervals_ReturnIntervalsCollection");
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 5, IntervalInclusion.RightOpened),
						Interval.New(6, 10, IntervalInclusion.Closed)
					},
					new[]
					{
						Interval.New(4, 5, IntervalInclusion.Closed),
						Interval.New(6, 8, IntervalInclusion.LeftOpened)
					},
					new[]
					{
						Interval.New(0, 4, IntervalInclusion.RightOpened),
						Interval.New(5, 5, IntervalInclusion.Closed),
						Interval.New(6, 6, IntervalInclusion.Closed),
						Interval.New(8, 10, IntervalInclusion.LeftOpened)
					})
				.SetName("AndMightOverlapBySomeDividedIntervalAndHaveACommonGap_ReturnIntervalCollection");
		}
	}
}