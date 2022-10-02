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
    public static DateTime Round(this DateTime dateTime, TimeSpan granuleSize,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var residue = new TimeSpan(dateTime.Ticks % granuleSize.Ticks);
        var append = Math.Round(residue / granuleSize, midpointRounding) * granuleSize;
        return dateTime - residue + append;
    }

    public static DateTime RoundToMonth(this DateTime dateTime,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (start, end) = DateTimeHelper.GetMonthOpenedEndBounds(dateTime.Year, dateTime.Month, dateTime.Kind);
        return RoundTo(start, dateTime, end, midpointRounding);
    }

    public static DateTime RoundToQuarter(this DateTime dateTime,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (start, end) =
            DateTimeHelper.GetQuarterOpenedEndBounds(dateTime.Year, dateTime.GetQuarterNumber(), dateTime.Kind);
        return RoundTo(start, dateTime, end, midpointRounding);
    }

    public static DateTime RoundToHalfYear(this DateTime dateTime,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var (start, end) =
            DateTimeHelper.GetHalfYearOpenedEndBounds(dateTime.Year, dateTime.GetHalfYearNumber(), dateTime.Kind);
        return RoundTo(start, dateTime, end, midpointRounding);
    }

    private static DateTime RoundTo(DateTime start, DateTime dateTime, DateTime end, MidpointRounding midpointRounding)
    {
        var pastSize = dateTime - start;
        var totalSize = end - start;
        var append = Math.Round(pastSize / totalSize, midpointRounding) * totalSize;
        return start + append;
    }
}