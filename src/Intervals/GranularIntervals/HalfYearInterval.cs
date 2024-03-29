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

using Intervals.Utils;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an half-year interval instance where the granule size is equal to a half-year
/// </summary>
public record class HalfYearInterval : MonthGranularInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.HalfYearInterval"/>
    /// with specified <paramref name="year" />, <paramref name="halfYear" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="halfYear"></param>
    /// <param name="kind"></param>
    public HalfYearInterval(int year, int halfYear, DateTimeKind kind = DateTimeKind.Unspecified) : base(
        DateTimeHelper.New(year, DateTimeHelper.HalfYearToMonth(halfYear), 1, kind),
        DateTimeHelper.New(year, DateTimeHelper.HalfYearToMonth(halfYear), 1, kind)
            .AddMonths(DateTimeHelper.MonthsInHalfYear))
    {
        Year = year;
        HalfYear = halfYear;
    }

    /// <summary>
    /// Year
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Half year
    /// </summary>
    public int HalfYear { get; }
}