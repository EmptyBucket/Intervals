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

namespace Intervals.Utils;

public static partial class DateTimeExtensions
{
    /// <summary>
    /// Returns the largest value aligned to <paramref name="granuleSize" /> that is less than or equal to the specified <paramref name="dateTime" />
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="granuleSize"></param>
    /// <returns></returns>
    public static DateTime Floor(this DateTime dateTime, TimeSpan granuleSize)
    {
        var residue = new TimeSpan(dateTime.Ticks % granuleSize.Ticks);
        return dateTime - residue;
    }

    /// <summary>
    /// Returns the largest value aligned to month that is less than or equal to the specified <paramref name="dateTime" />
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime FloorToMonth(this DateTime dateTime) =>
        DateTimeHelper.New(dateTime.Year, dateTime.Month, 1, dateTime.Kind);

    /// <summary>
    /// Returns the largest value aligned to quarter that is less than or equal to the specified <paramref name="dateTime" />
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime FloorToQuarter(this DateTime dateTime) =>
        DateTimeHelper.New(dateTime.Year, DateTimeHelper.QuarterToMonth(dateTime.GetQuarter()), 1, dateTime.Kind);

    /// <summary>
    /// Returns the largest value aligned to half-year that is less than or equal to the specified <paramref name="dateTime" />
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime FloorToHalfYear(this DateTime dateTime) =>
        DateTimeHelper.New(dateTime.Year, DateTimeHelper.HalfYearToMonth(dateTime.GetHalfYear()), 1, dateTime.Kind);

    /// <summary>
    /// Returns the largest value aligned to year that is less than or equal to the specified <paramref name="dateTime" />
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime FloorToYear(this DateTime dateTime) =>
        DateTimeHelper.New(dateTime.Year, 1, 1, dateTime.Kind);
}