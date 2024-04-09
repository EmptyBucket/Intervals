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

using System.Text.Json.Serialization;
using Intervals.Intervals;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an half-yearly interval instance
/// </summary>
[Serializable]
public record class HalfYearlyInterval : MonthlyInterval
{
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    private HalfYearlyInterval(DateTime leftValue, DateTime rightValue, TimeSpan granuleLength,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, granuleLength, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.HalfYearlyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granuleLength" />,
    /// <paramref name="halfYearsCount" />, <paramref name="inclusion" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="halfYearsCount"></param>
    /// <param name="inclusion"></param>
    /// <param name="kind"></param>
    public HalfYearlyInterval(DateTime leftValue, TimeSpan granuleLength, int halfYearsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened, DateTimeKind kind = DateTimeKind.Unspecified)
        : base(leftValue, granuleLength, halfYearsCount * DateTimeHelper.MonthsInHalfYear, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.HalfYearlyInterval"/>
    /// with specified <paramref name="year" />, <paramref name="halfYear" />, <paramref name="granuleLength" />,
    /// <paramref name="halfYearsCount" />, <paramref name="inclusion" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="halfYear"></param>
    /// <param name="granuleLength"></param>
    /// <param name="halfYearsCount"></param>
    /// <param name="inclusion"></param>
    /// <param name="kind"></param>
    public HalfYearlyInterval(int year, int halfYear, TimeSpan granuleLength, int halfYearsCount,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened, DateTimeKind kind = DateTimeKind.Unspecified)
        : base(DateTimeHelper.GetMonthStart(year, DateTimeHelper.HalfYearToMonth(halfYear), kind), granuleLength,
            halfYearsCount * DateTimeHelper.MonthsInHalfYear, inclusion)
    {
    }
}