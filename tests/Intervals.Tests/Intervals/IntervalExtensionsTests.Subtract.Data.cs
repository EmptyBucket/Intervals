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

public partial class IntervalExtensionsTests
{
    public static IEnumerable Subtract_WhenTwoIntervals_Data()
    {
        yield return new TestCaseData(
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                Array.Empty<Interval<int>>())
            .SetName("WhenTwoIntervalsAndBothAreEmpty_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, -1, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndSecondIsEmpty_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                Array.Empty<Interval<int>>())
            .SetName("WhenTwoIntervalsAndTheyAreEqual_ReturnEmpty");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(1, 2, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.RightOpened) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointIncluded_ReturnFirstRightOpened");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Opened),
                new Interval<int>(1, 2, IntervalInclusion.Opened),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Opened) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointExcluded_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 2, IntervalInclusion.LeftOpened),
                new[] { new Interval<int>(0, 1, IntervalInclusion.LeftOpened) })
            .SetName("WhenTwoIntervalsAndTheyShareSameEndpointWithDifferentInclusion_ReturnFirst");
        yield return new TestCaseData(
                new Interval<int>(0, 2, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Opened) })
            .SetName("WhenTwoIntervalsAndTheyOverlapForSome_ReturnSubtract");
        yield return new TestCaseData(
                new Interval<int>(0, 4, IntervalInclusion.LeftOpened),
                new Interval<int>(1, 3, IntervalInclusion.RightOpened),
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened), new Interval<int>(3, 4, IntervalInclusion.Closed)
                })
            .SetName("WhenTwoIntervalsAndFirstContainsSecond_ReturnSubtract");
        yield return new TestCaseData(
                new Interval<int>(0, 1, IntervalInclusion.Closed),
                new Interval<int>(3, 4, IntervalInclusion.Closed),
                new[] { new Interval<int>(0, 1, IntervalInclusion.Closed) })
            .SetName("WhenTwoIntervalsAndTheyDontIntersect_ReturnFirst");
    }

    public static IEnumerable Subtract_WhenManyIntervals_Data()
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
                    new Interval<int>(3, 4, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.RightOpened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByIncludedEndpointOnly_ReturnSubtracted");
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
                    new Interval<int>(3, 4, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 1, IntervalInclusion.Opened),
                    new Interval<int>(2, 3, IntervalInclusion.Opened),
                    new Interval<int>(4, 5, IntervalInclusion.Opened)
                })
            .SetName("WhenManyIntervalsAndAllMightOverlapByExcludedEndpointOnly_ReturnFirst");
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
                    new Interval<int>(0, 4, IntervalInclusion.Opened),
                    new Interval<int>(7, 8, IntervalInclusion.LeftOpened),
                    new Interval<int>(9, 11, IntervalInclusion.Opened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeIntervals_ReturnSubtracted");
        yield return new TestCaseData(
                new[]
                {
                    new Interval<int>(0, 5, IntervalInclusion.RightOpened),
                    new Interval<int>(5, 10, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(4, 6, IntervalInclusion.Closed)
                },
                new[]
                {
                    new Interval<int>(0, 4, IntervalInclusion.RightOpened),
                    new Interval<int>(6, 10, IntervalInclusion.LeftOpened)
                })
            .SetName("WhenManyIntervalsAndMightOverlapBySomeDividedInterval_ReturnSubtracted");
    }
}