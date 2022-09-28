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
    public static IEnumerable SplitBySeconds_Data()
    {
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 0, 1)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 1)))
                })
            .SetName("SplitBySeconds_WhenOneSecondByTwoSeconds_ReturnOneSecond");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 0, 2)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 2)))
                })
            .SetName("SplitBySeconds_WhenTwoSecondsByTwoSeconds_ReturnTwoSeconds");
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
            .SetName("SplitBySeconds_WhenThreeSecondsByTwoSeconds_ReturnTwoAndOneSecond");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 0, 4)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 2))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 2)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 0, 4)))
                })
            .SetName("SplitBySeconds_WhenFourSecondsByTwoSeconds_ReturnTwoAndTwoSeconds");
    }

    public static IEnumerable SplitByMinutes_Data()
    {
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 1, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 1, 0)))
                })
            .SetName("SplitByMinutes_WhenOneMinuteByTwoMinutes_ReturnOneMinute");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 2, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 2, 0)))
                })
            .SetName("SplitByMinutes_WhenTwoMinutesByTwoMinutes_ReturnTwoMinutes");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 3, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 3, 0))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 3, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 4, 0)))
                })
            .SetName("SplitByMinutes_WhenThreeMinutesByTwoMinutes_ReturnTwoAndOneMinute");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 0, 4, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 2, 0))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 2, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 0, 4, 0)))
                })
            .SetName("SplitByMinutes_WhenFourMinutesByTwoMinutes_ReturnTwoAndTwoMinutes");
    }

    public static IEnumerable SplitByHours_Data()
    {
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 1, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 1, 0, 0)))
                })
            .SetName("SplitByHours_WhenOneHourByTwoHours_ReturnOneHour");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 2, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 2, 0, 0)))
                })
            .SetName("SplitByHours_WhenTwoHoursByTwoHours_ReturnTwoHours");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 3, 0, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 2, 0, 0))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 2, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 4, 0, 0)))
                })
            .SetName("SplitByHours_WhenThreeHoursByTwoHours_ReturnTwoAndOneHour");
        yield return new TestCaseData(
                new Interval<DateTime>(new DateTime(2022, 1, 1, 0, 0, 0), new DateTime(2022, 1, 1, 4, 0, 0)), new[]
                {
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 0, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 2, 0, 0))),
                    new Interval<DateTime>(
                        Point.Included(new DateTime(2022, 1, 1, 2, 0, 0)),
                        Point.Excluded(new DateTime(2022, 1, 1, 4, 0, 0)))
                })
            .SetName("SplitByHours_WhenFourHoursByTwoHours_ReturnTwoAndTwoHours");
    }

    public static IEnumerable SplitByDays_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 1, 2)))
            })
            .SetName("SplitByDays_WhenOneDayByTwoDays_ReturnOneDay");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 3)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 1, 3)))
            })
            .SetName("SplitByDays_WhenTwoDaysByTwoDays_ReturnTwoDays");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 4)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 1, 3))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 3)), Point.Excluded(new DateTime(2022, 1, 4)))
            })
            .SetName("SplitByDays_WhenThreeDaysByTwoDays_ReturnTwoAndOneDay");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 5)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 1, 3))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 3)), Point.Excluded(new DateTime(2022, 1, 5)))
            })
            .SetName("SplitByDays_WhenFourDaysByTwoDays_ReturnTwoAndTwoDays");
    }

    public static IEnumerable SplitByMonths_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 2, 1)))
            })
            .SetName("SplitByMonths_WhenOneMonthByTwoMonths_ReturnOneMonth");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 3, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 3, 1)))
            })
            .SetName("SplitByMonths_WhenTwoMonthsByTwoMonths_ReturnTwoMonths");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 4, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 3, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 3, 1)), Point.Excluded(new DateTime(2022, 4, 1)))
            })
            .SetName("SplitByMonths_WhenThreeMonthsByTwoMonths_ReturnTwoAndOneMonth");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 5, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 3, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 3, 1)), Point.Excluded(new DateTime(2022, 5, 1)))
            })
            .SetName("SplitByMonths_WhenFourMonthsByTwoMonths_ReturnTwoAndTwoMonths");
    }

    public static IEnumerable SplitByQuarters_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 4, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 4, 1)))
            })
            .SetName("SplitByQuarters_WhenOneQuarterByTwoQuarters_ReturnOneQuarter");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 7, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 7, 1)))
            })
            .SetName("SplitByQuarters_WhenTwoQuartersByTwoQuarters_ReturnTwoQuarters");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 10, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 7, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 7, 1)), Point.Excluded(new DateTime(2022, 10, 1)))
            })
            .SetName("SplitByQuarters_WhenThreeQuartersByTwoQuarters_ReturnTwoAndOneQuarter");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 7, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 7, 1)), Point.Excluded(new DateTime(2023, 1, 1)))
            })
            .SetName("SplitByQuarters_WhenFourQuartersByTwoQuarters_ReturnTwoAndTwoQuarters");
    }

    public static IEnumerable SplitByHalfYears_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 7, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2022, 7, 1)))
            })
            .SetName("SplitByHalfYears_WhenOneHalfYearByTwoHalfYears_ReturnOneHalfYear");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2023, 1, 1)))
            })
            .SetName("SplitByHalfYears_WhenTwoHalfYearsByTwoHalfYears_ReturnTwoHalfYears");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2023, 7, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2023, 1, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2023, 1, 1)), Point.Excluded(new DateTime(2023, 7, 1)))
            })
            .SetName("SplitByHalfYears_WhenThreeHalfYearsByTwoHalfYears_ReturnTwoAndOneHalfYear");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2024, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2023, 1, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2023, 1, 1)), Point.Excluded(new DateTime(2024, 1, 1)))
            })
            .SetName("SplitByHalfYears_WhenFourHalfYearsByTwoHalfYears_ReturnTwoAndTwoHalfYears");
    }

    public static IEnumerable SplitByYears_Data()
    {
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2023, 1, 1)))
            })
            .SetName("SplitByYears_WhenOneYearByTwoYears_ReturnOneYear");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2024, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2024, 1, 1)))
            })
            .SetName("SplitByYears_WhenTwoYearsByTwoYears_ReturnTwoYears");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2025, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2024, 1, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2024, 1, 1)), Point.Excluded(new DateTime(2025, 1, 1)))
            })
            .SetName("SplitByYears_WhenThreeYearsByTwoYears_ReturnTwoAndOneYear");
        yield return new TestCaseData(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2026, 1, 1)), new[]
            {
                new Interval<DateTime>(
                    Point.Included(new DateTime(2022, 1, 1)), Point.Excluded(new DateTime(2024, 1, 1))),
                new Interval<DateTime>(
                    Point.Included(new DateTime(2024, 1, 1)), Point.Excluded(new DateTime(2026, 1, 1)))
            })
            .SetName("SplitByYears_WhenFourYearsByTwoYears_ReturnTwoAndTwoYears");
    }
}