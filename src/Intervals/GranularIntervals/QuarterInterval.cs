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
/// Represents an quarter interval instance where the granule size is equal to a quarter
/// </summary>
public record class QuarterInterval : MonthGranularInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.QuarterInterval"/>
    /// with specified <paramref name="year" />, <paramref name="quarter" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="quarter"></param>
    /// <param name="kind"></param>
    public QuarterInterval(int year, int quarter, DateTimeKind kind = DateTimeKind.Unspecified) : base(
        DateTimeHelper.New(year, DateTimeHelper.QuarterToMonth(quarter), 1, kind),
        DateTimeHelper.New(year, DateTimeHelper.QuarterToMonth(quarter), 1, kind)
            .AddMonths(DateTimeHelper.MonthsInQuarter))
    {
        Year = year;
        Quarter = quarter;
    }

    /// <summary>
    /// Year
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Quarter
    /// </summary>
    public int Quarter { get; }
}