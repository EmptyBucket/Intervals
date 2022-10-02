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
    public static DateTime Ceiling(this DateTime dateTime, TimeSpan granuleSize)
    {
        var residue = new TimeSpan(dateTime.Ticks % granuleSize.Ticks);
        var append = Convert.ToInt64(residue > TimeSpan.Zero) * granuleSize;
        return dateTime - residue + append;
    }

    public static DateTime CeilingToMonth(this DateTime dateTime)
    {
        var start = DateTimeHelper.GetMonthStart(dateTime.Year, dateTime.Month, dateTime.Kind);
        return dateTime > start
            ? DateTimeHelper.GetMonthOpenedEnd(dateTime.Year, dateTime.Month, dateTime.Kind)
            : start;
    }

    public static DateTime CeilingToQuarter(this DateTime dateTime)
    {
        var start = DateTimeHelper.GetQuarterStart(dateTime.Year, dateTime.GetQuarterNumber(), dateTime.Kind);
        return dateTime > start
            ? DateTimeHelper.GetQuarterOpenedEnd(dateTime.Year, dateTime.GetQuarterNumber(), dateTime.Kind)
            : start;
    }

    public static DateTime CeilingToHalfYear(this DateTime dateTime)
    {
        var start = DateTimeHelper.GetHalfYearStart(dateTime.Year, dateTime.GetHalfYearNumber(), dateTime.Kind);
        return dateTime > start
            ? DateTimeHelper.GetHalfYearOpenedEnd(dateTime.Year, dateTime.GetHalfYearNumber(), dateTime.Kind)
            : start;
    }
}