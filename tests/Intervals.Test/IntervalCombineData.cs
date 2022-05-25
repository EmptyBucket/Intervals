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

using System.Collections;
using Intervals.Intervals;
using NUnit.Framework;

namespace Intervals.Test;

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
				new[] { (IInterval<int>)new Interval<int>(0, 1, IntervalInclusion.Closed) })
			.SetName("AndOneOfThemIsEmpty_ReturnOther");
		yield return new TestCaseData(
				new Interval<int>(0, 1, IntervalInclusion.Closed),
				new Interval<int>(0, 1, IntervalInclusion.Closed),
				new[] { (IInterval<int>)new Interval<int>(0, 1, IntervalInclusion.Closed) })
			.SetName("AndTheyAreEqual_ReturnIt");
		yield return new TestCaseData(
				new Interval<int>(0, 1, IntervalInclusion.Closed),
				new Interval<int>(1, 2, IntervalInclusion.Closed),
				new[] { (IInterval<int>)new Interval<int>(0, 2, IntervalInclusion.Closed) })
			.SetName("AndTheyShareSameEndpointIncluded_ReturnConnectedInterval");
		yield return new TestCaseData(
				new Interval<int>(0, 1, IntervalInclusion.RightOpened),
				new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
				new[] {new Interval<int>(0, 1, IntervalInclusion.RightOpened), (IInterval<int>)new Interval<int>(1, 2, IntervalInclusion.LeftOpened)})
			.SetName("AndTheyShareSameEndpointExcluded_ReturnIntervalsCollection");
		yield return new TestCaseData(
				new Interval<int>(0, 1, IntervalInclusion.LeftOpened),
				new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
				new[] { (IInterval<int>)new Interval<int>(0, 2, IntervalInclusion.LeftOpened) })
			.SetName("AndTheyShareSameEndpointWithDifferentInclusion_ReturnEmptyCollection");
		yield return new TestCaseData(
				new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
				new Interval<int>(1, 3, IntervalInclusion.RightOpened),
				new[] { (IInterval<int>)new Interval<int>(0, 3, IntervalInclusion.Opened) })
			.SetName("AndTheyOverlapForSomeInterval_ReturnInterval");
		yield return new TestCaseData(
				new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
				new Interval<int>(1, 3, IntervalInclusion.RightOpened),
				new[] { (IInterval<int>)new Interval<int>(0, 4, IntervalInclusion.LeftOpened) })
			.SetName("AndFirstIntervalContainsSecond_ReturnFirst");
		yield return new TestCaseData(
				new Interval<int>(0, 1, IntervalInclusion.Opened),
				new Interval<int>(2, 3, IntervalInclusion.Opened),
				new[] {new Interval<int>(0, 1, IntervalInclusion.Opened), (IInterval<int>)new Interval<int>(2, 3, IntervalInclusion.Opened)})
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
					(IInterval<int>)new Interval<int>(4, 5, IntervalInclusion.RightOpened)
				},
				new[]
				{
					new Interval<int>(1, 2, IntervalInclusion.Closed),
					(IInterval<int>)new Interval<int>(3, 4, IntervalInclusion.Closed)
				},
				new[]
				{
					(IInterval<int>)new Interval<int>(0, 5, IntervalInclusion.RightOpened)
				})
			.SetName("AndAllMightOverlapByOneEndpointOnly_ReturnSingleIntervalCollection");
		yield return new TestCaseData(
				new[]
				{
					new Interval<int>(0, 1, IntervalInclusion.Opened),
					new Interval<int>(2, 3, IntervalInclusion.Opened),
					(IInterval<int>)new Interval<int>(4, 5, IntervalInclusion.Opened)
				},
				new[]
				{
					new Interval<int>(1, 2, IntervalInclusion.Opened),
					(IInterval<int>)new Interval<int>(3, 4, IntervalInclusion.Opened)
				},
				new[]
				{
					new Interval<int>(0, 1, IntervalInclusion.Opened),
					new Interval<int>(1, 2, IntervalInclusion.Opened),
					new Interval<int>(2, 3, IntervalInclusion.Opened),
					new Interval<int>(3, 4, IntervalInclusion.Opened),
					(IInterval<int>)new Interval<int>(4, 5, IntervalInclusion.Opened)
				})
			.SetName("AndTheyDontIntersect_ReturnAllIntervalsCollection");
		yield return new TestCaseData(
				new[]
				{
					new Interval<int>(0, 10, IntervalInclusion.Opened),
					(IInterval<int>)new Interval<int>(2, 11, IntervalInclusion.Opened)
				},
				new[]
				{
					new Interval<int>(0, 7, IntervalInclusion.Closed),
					new Interval<int>(8, 9, IntervalInclusion.LeftOpened),
					(IInterval<int>)new Interval<int>(18, 19, IntervalInclusion.LeftOpened)
				},
				new[]
				{
					new Interval<int>(0, 11, IntervalInclusion.RightOpened),
					(IInterval<int>)new Interval<int>(18, 19, IntervalInclusion.LeftOpened)
				})
			.SetName("AndMightOverlapBySomeIntervals_ReturnIntervalsCollection");
	}
}