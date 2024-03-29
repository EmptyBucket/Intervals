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
/// Represents an day interval instance where the granule size is equal to a day
/// </summary>
public record class DayInterval : TimeGranularInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.DayInterval"/>
    /// with specified <paramref name="year" />, <paramref name="month" />, <paramref name="day" /> and
    /// <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <param name="kind"></param>
    public DayInterval(int year, int month, int day, DateTimeKind kind = DateTimeKind.Unspecified) : base(
        DateTimeHelper.New(year, month, day, kind),
        DateTimeHelper.New(year, month, day, kind).AddDays(1))
    {
        Year = year;
        Month = month;
        Day = day;
    }

    /// <summary>
    /// Year
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Month
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// Day
    /// </summary>
    public int Day { get; }
}