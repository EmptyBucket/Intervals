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
	public class SingleIntervalsCombineData : IEnumerable
	{
		public IEnumerator GetEnumerator()
		{
			yield return new TestCaseData(
					Interval.New(0, -1, IntervalType.Closed),
					Interval.New(0, -1, IntervalType.Closed),
					Array.Empty<IInterval<int>>())
				.SetName("AndBothAreEmpty_ReturnEmptyCollection");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalType.Closed),
					Interval.New(0, -1, IntervalType.Closed),
					new[] {Interval.New(0, 1, IntervalType.Closed)})
				.SetName("AndOneOfThemIsEmpty_ReturnOther");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalType.Closed),
					Interval.New(0, 1, IntervalType.Closed),
					new[] {Interval.New(0, 1, IntervalType.Closed)})
				.SetName("AndTheyAreEqual_ReturnIt");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalType.Closed),
					Interval.New(1, 2, IntervalType.Closed),
					new[] {Interval.New(0, 2, IntervalType.Closed)})
				.SetName("AndTheyShareSameEndpointIncluded_ReturnConnectedInterval");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalType.RightOpen),
					Interval.New(1, 2, IntervalType.LeftOpen),
					new[] {Interval.New(0, 1, IntervalType.RightOpen), Interval.New(1, 2, IntervalType.LeftOpen)})
				.SetName("AndTheyShareSameEndpointExcluded_ReturnIntervalsCollection");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalType.LeftOpen),
					Interval.New(1, 2, IntervalType.LeftOpen),
					new[] {Interval.New(0, 2, IntervalType.LeftOpen)})
				.SetName("AndTheyShareSameEndpointWithDifferentInclusion_ReturnEmptyCollection");
			yield return new TestCaseData(
					Interval.New(0, 2, IntervalType.LeftOpen),
					Interval.New(1, 3, IntervalType.RightOpen),
					new[] {Interval.New(0, 3, IntervalType.Open)})
				.SetName("AndTheyOverlapForSomeInterval_ReturnInterval");
			yield return new TestCaseData(
					Interval.New(0, 4, IntervalType.LeftOpen),
					Interval.New(1, 3, IntervalType.RightOpen),
					new[] {Interval.New(0, 4, IntervalType.LeftOpen)})
				.SetName("AndFirstIntervalContainsSecond_ReturnFirst");
			yield return new TestCaseData(
					Interval.New(0, 1, IntervalType.Open),
					Interval.New(2, 3, IntervalType.Open),
					new[] {Interval.New(0, 1, IntervalType.Open), Interval.New(2, 3, IntervalType.Open)})
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
						Interval.New(0, 1, IntervalType.RightOpen),
						Interval.New(2, 3, IntervalType.Closed),
						Interval.New(4, 5, IntervalType.RightOpen)
					},
					new[]
					{
						Interval.New(1, 2, IntervalType.Closed),
						Interval.New(3, 4, IntervalType.Closed)
					},
					new[]
					{
						Interval.New(0, 5, IntervalType.RightOpen)
					})
				.SetName("AndAllMightOverlapByOneEndpointOnly_ReturnSingleIntervalCollection");
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 1, IntervalType.Open),
						Interval.New(2, 3, IntervalType.Open),
						Interval.New(4, 5, IntervalType.Open)
					},
					new[]
					{
						Interval.New(1, 2, IntervalType.Open),
						Interval.New(3, 4, IntervalType.Open)
					},
					new[]
					{
						Interval.New(0, 1, IntervalType.Open),
						Interval.New(1, 2, IntervalType.Open),
						Interval.New(2, 3, IntervalType.Open),
						Interval.New(3, 4, IntervalType.Open),
						Interval.New(4, 5, IntervalType.Open)
					})
				.SetName("AndTheyDontIntersect_ReturnAllIntervalsCollection");
			yield return new TestCaseData(
					new[]
					{
						Interval.New(0, 10, IntervalType.Open),
						Interval.New(2, 11, IntervalType.Open)
					},
					new[]
					{
						Interval.New(0, 7, IntervalType.Closed),
						Interval.New(8, 9, IntervalType.LeftOpen),
						Interval.New(18, 19, IntervalType.LeftOpen)
					},
					new[]
					{
						Interval.New(0, 11, IntervalType.RightOpen),
						Interval.New(18, 19, IntervalType.LeftOpen)
					})
				.SetName("AndMightOverlapBySomeIntervals_ReturnIntervalsCollection");
		}
	}
}