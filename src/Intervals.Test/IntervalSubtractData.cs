// MIT License
// 
// Copyright (c) 2021 Alexey Politov, Yevgeny Khoroshavin
// https://github.com/EmptyBucket/Intervals
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test
{
	public class SingleIntervalsSubtractData : IEnumerable
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
					new[] {Interval.New(0, 1, IntervalInclusion.RightOpen)})
				.SetName("AndTheyShareSameEndpointIncluded_ReturnIntervalWithoutThePointCollection");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.Open),
					Interval.New(1, 2, IntervalInclusion.Open),
					new[] {Interval.New(0, 1, IntervalInclusion.Open)})
				.SetName("AndTheyShareSameEndpointExcluded_ReturnIntervalCollection");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.LeftOpen),
					Interval.New(1, 2, IntervalInclusion.LeftOpen),
					new[] {Interval.New(0, 1, IntervalInclusion.LeftOpen)})
				.SetName("AndTheyShareSameEndpointWithDifferentInclusion_ReturnIntervalCollection");
			yield return new TestCaseData(
					Interval.New(0, 2, IntervalInclusion.LeftOpen),
					Interval.New(1, 3, IntervalInclusion.RightOpen),
					new[] {Interval.New(0, 1, IntervalInclusion.Open)})
				.SetName("AndTheyOverlapForSomeInterval_ReturnConnectedDifferenceWithRightInclusions");
			yield return new TestCaseData(
					Interval.New(0, 4, IntervalInclusion.LeftOpen),
					Interval.New(1, 3, IntervalInclusion.RightOpen),
					new[] {Interval.New(0, 1, IntervalInclusion.Open), Interval.New(3, 4, IntervalInclusion.Closed)})
				.SetName("AndFirstIntervalContainsSecond_ReturnNonConnectedDifferenceWithRightInclusions");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalInclusion.Closed),
					Interval.New(3, 4, IntervalInclusion.Closed),
					new[] {Interval.New(0, 1, IntervalInclusion.Closed)})
				.SetName("AndTheyDontIntersect_ReturnFirstInterval");
		}
	}

	public class MultipleIntervalsSubtractData : IEnumerable
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
						Interval.New(3, 4, IntervalInclusion.Closed)
					},
					new[]
					{
						Interval.New(0, 1, IntervalInclusion.RightOpen),
						Interval.New(2, 3, IntervalInclusion.Open),
						Interval.New(4, 5, IntervalInclusion.LeftOpen)
					})
				.SetName("AndAllMightOverlapByIncludedEndpointOnly_ReturnIntervalsCollectionWithoutSomeEndpoints");
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 1, IntervalInclusion.Open),
						Interval.New(2, 3, IntervalInclusion.Open),
						Interval.New(4, 5, IntervalInclusion.Open)
					},
					new[]
					{
						Interval.New(1, 2, IntervalInclusion.Closed),
						Interval.New(3, 4, IntervalInclusion.Closed)
					},
					new[]
					{
						Interval.New(0, 1, IntervalInclusion.Open),
						Interval.New(2, 3, IntervalInclusion.Open),
						Interval.New(4, 5, IntervalInclusion.Open)
					})
				.SetName("AndAllMightOverlapByExcludedEndpointOnly_ReturnFirstIntervalsCollection");
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 5, IntervalInclusion.Open),
						Interval.New(6, 11, IntervalInclusion.Open)
					},
					new[]
					{
						Interval.New(4, 7, IntervalInclusion.Closed),
						Interval.New(8, 9, IntervalInclusion.LeftOpen),
						Interval.New(11, 15, IntervalInclusion.Closed)
					},
					new[]
					{
						Interval.New(0, 4, IntervalInclusion.Open),
						Interval.New(7, 8, IntervalInclusion.LeftOpen),
						Interval.New(9, 11, IntervalInclusion.Open)
					})
				.SetName("AndMightOverlapBySomeIntervals_ReturnIntervalsCollectionWithRightInclusion");
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 5, IntervalInclusion.RightOpen),
						Interval.New(5, 10, IntervalInclusion.Closed)
					},
					new[]
					{
						Interval.New(4, 6, IntervalInclusion.Closed)
					},
					new[]
					{
						Interval.New(0, 4, IntervalInclusion.RightOpen),
						Interval.New(6, 10, IntervalInclusion.LeftOpen)
					})
				.SetName("AndMightOverlapBySomeDividedInterval_ReturnIntervalsCollection");
		}
	}
}