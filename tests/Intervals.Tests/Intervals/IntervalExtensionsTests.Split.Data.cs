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
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Intervals;

public partial class IntervalExtensionsTests
{
    public static IEnumerable Split_Data()
    {
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 0, 1)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 1)))
                })
            .SetName("Split_WhenAlignedOneSecondByTwoSeconds_ReturnAlignedOneSecond");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 0, 2)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 2)))
                })
            .SetName("Split_WhenAlignedTwoSecondsByTwoSeconds_ReturnAlignedTwoSeconds");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 0, 3)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 3))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 3)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 4)))
                })
            .SetName("Split_WhenAlignedThreeSecondsByTwoSeconds_ReturnAlignedTwoAndOneSecond");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0, 500), new DateTime(2022, 1, 1, 0, 0, 1, 500)),
                new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0, 500)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 1, 500)))
                })
            .SetName("Split_OneSecondByTwoSeconds_ReturnOneSecond");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0, 500), new DateTime(2022, 1, 1, 0, 0, 2, 500)),
                new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0, 500)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 2, 500)))
                })
            .SetName("Split_TwoSecondsByTwoSeconds_ReturnTwoSeconds");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0, 500), new DateTime(2022, 1, 1, 0, 0, 3, 500)),
                new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0, 500)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 3, 500))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 3, 500)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 4, 500)))
                })
            .SetName("Split_ThreeSecondsByTwoSeconds_ReturnTwoAndOneSecond");
    }

    public static IEnumerable SplitByMonths_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 2, 1)))
            })
            .SetName("SplitByMonths_WhenAlignedOneMonthByTwoMonths_ReturnAlignedOneMonth");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 3, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 3, 1)))
            })
            .SetName("SplitByMonths_WhenAlignedTwoMonthsByTwoMonths_ReturnAlignedTwoMonths");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 4, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 3, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 3, 1)), Point.Excluded(new DateTime(2022, 4, 1)))
            })
            .SetName("SplitByMonths_WhenAlignedThreeMonthsByTwoMonths_ReturnAlignedTwoAndOneMonth");

        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2022, 2, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)),
                        Point.Excluded(new DateTime(2022, 2, 1, 12, 0, 0)))
                })
            .SetName("SplitByMonths_WhenOneMonthByTwoMonths_ReturnOneMonth");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2022, 3, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)), Point.Excluded(new DateTime(2022, 3, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 3, 1)), Point.Excluded(new DateTime(2022, 3, 1, 12, 0, 0)))
                })
            .SetName("SplitByMonths_WhenTwoMonthsByTwoMonths_ReturnTwoPartsOfTwoMonths");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2022, 5, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)), Point.Excluded(new DateTime(2022, 3, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 3, 1)), Point.Excluded(new DateTime(2022, 5, 1, 1, 0, 0))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 5, 1)), Point.Excluded(new DateTime(2022, 5, 1, 12, 0, 0)))
                })
            .SetName("SplitByMonths_WhenThreeMonthsByTwoMonths_ReturnTwoPartsOfTwoMonthsAndTwoMonths");
    }

    public static IEnumerable SplitByQuarters_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 4, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 4, 1)))
            })
            .SetName("SplitByQuarters_WhenAlignedOneQuarterByTwoQuarters_ReturnAlignedOneQuarter");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 7, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 7, 1)))
            })
            .SetName("SplitByQuarters_WhenAlignedTwoQuartersByTwoQuarters_ReturnAlignedTwoQuarters");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 10, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 7, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 7, 1)), Point.Excluded(new DateTime(2022, 10, 1)))
            })
            .SetName("SplitByQuarters_WhenAlignedThreeQuartersByTwoQuarters_ReturnAlignedTwoAndOneQuarter");


        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2022, 4, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)),
                        Point.Excluded(new DateTime(2022, 4, 1, 12, 0, 0)))
                })
            .SetName("SplitByQuarters_WhenOneQuarterByTwoQuarters_ReturnOneQuarter");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2022, 7, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)), Point.Excluded(new DateTime(2022, 7, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 7, 1)), Point.Excluded(new DateTime(2022, 7, 1, 12, 0, 0)))
                })
            .SetName("SplitByQuarters_WhenTwoQuartersByTwoQuarters_ReturnTwoPartsOfTwoQuarters");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2023, 1, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)), Point.Excluded(new DateTime(2022, 7, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 7, 1)), Point.Excluded(new DateTime(2022, 10, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 10, 1)), Point.Excluded(new DateTime(2023, 1, 1, 12, 0, 0)))
                })
            .SetName("SplitByQuarters_WhenThreeQuartersByTwoQuarters_ReturnTwoPartsOfTwoQuartersAndTwoQuarters");
    }

    public static IEnumerable SplitByHalfYears_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 7, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 7, 1)))
            })
            .SetName("SplitByHalfYears_WhenAlignedOneHalfYearByTwoHalfYears_ReturnAlignedOneHalfYear");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2023, 1, 1)))
            })
            .SetName("SplitByHalfYears_WhenAlignedTwoHalfYearsByTwoHalfYears_ReturnAlignedTwoHalfYears");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2023, 7, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2023, 1, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2023, 1, 1)), Point.Excluded(new DateTime(2023, 7, 1)))
            })
            .SetName("SplitByHalfYears_WhenAlignedThreeHalfYearsByTwoHalfYears_ReturnAlignedTwoAndOneHalfYear");


        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2022, 7, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)),
                        Point.Excluded(new DateTime(2022, 7, 1, 12, 0, 0)))
                })
            .SetName("SplitByHalfYears_WhenOneHalfYearByTwoHalfYears_ReturnOneHalfYear");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2023, 1, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)), Point.Excluded(new DateTime(2023, 1, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2023, 1, 1)), Point.Excluded(new DateTime(2023, 1, 1, 12, 0, 0)))
                })
            .SetName("SplitByHalfYears_WhenTwoHalfYearsByTwoHalfYears_ReturnTwoPartsOfTwoHalfYears");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 12, 0, 0), new DateTime(2024, 1, 1, 12, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 12, 0, 0)), Point.Excluded(new DateTime(2023, 1, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2023, 1, 1)), Point.Excluded(new DateTime(2023, 7, 1))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2023, 7, 1)), Point.Excluded(new DateTime(2024, 1, 1, 12, 0, 0)))
                })
            .SetName("SplitByHalfYears_WhenThreeHalfYearsByTwoHalfYears_ReturnTwoPartsOfTwoHalfYearsAndTwoHalfYears");
    }
}