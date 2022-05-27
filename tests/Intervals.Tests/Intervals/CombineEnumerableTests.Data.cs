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