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

internal static class DateTimeHelper
{
    public const int QuarterLenInMonths = 3;
    public const int HalfYearLenInMonths = 6;

    public static DateTime GetSecondStart(int year, int month, int day, int hour, int minute, int second,
        DateTimeKind kind = DateTimeKind.Utc) =>
        new(year, month, day, hour, minute, second, kind);

    public static DateTime GetSecondOpenedEnd(int year, int month, int day, int hour, int minute, int second,
        DateTimeKind kind = DateTimeKind.Utc) =>
        GetSecondStart(year, month, day, hour, minute, second, kind).AddSeconds(1);

    public static (DateTime Start, DateTime End) GetSecondOpenedEndBounds(int year, int month, int day, int hour,
        int minute, int second, DateTimeKind kind = DateTimeKind.Utc) =>
        (GetSecondStart(year, month, day, hour, minute, second, kind),
            GetSecondOpenedEnd(year, month, day, hour, minute, second, kind));

    public static DateTime GetMinuteStart(int year, int month, int day, int hour, int minute,
        DateTimeKind kind = DateTimeKind.Utc) =>
        new(year, month, day, hour, minute, 0, kind);

    public static DateTime GetMinuteOpenedEnd(int year, int month, int day, int hour, int minute,
        DateTimeKind kind = DateTimeKind.Utc) =>
        GetMinuteStart(year, month, day, hour, minute, kind).AddMinutes(1);

    public static (DateTime Start, DateTime End) GetMinuteOpenedEndBounds(int year, int month, int day, int hour,
        int minute, DateTimeKind kind = DateTimeKind.Utc) =>
        (GetMinuteStart(year, month, day, hour, minute, kind),
            GetMinuteOpenedEnd(year, month, day, hour, minute, kind));

    public static DateTime GetHourStart(int year, int month, int day, int hour,
        DateTimeKind kind = DateTimeKind.Utc) =>
        new(year, month, day, hour, 0, 0, kind);

    public static DateTime GetHourOpenedEnd(int year, int month, int day, int hour,
        DateTimeKind kind = DateTimeKind.Utc) =>
        GetHourStart(year, month, day, hour, kind).AddHours(1);

    public static (DateTime Start, DateTime End) GetHourOpenedEndBounds(int year, int month, int day, int hour,
        DateTimeKind kind = DateTimeKind.Utc) =>
        (GetHourStart(year, month, day, hour, kind), GetHourOpenedEnd(year, month, day, hour, kind));

    public static DateTime GetDayStart(int year, int month, int day, DateTimeKind kind = DateTimeKind.Utc) =>
        new(year, month, day, 0, 0, 0, kind);

    public static DateTime GetDayOpenedEnd(int year, int month, int day, DateTimeKind kind = DateTimeKind.Utc) =>
        GetDayStart(year, month, day, kind).AddDays(1);

    public static (DateTime Start, DateTime End) GetDayOpenedEndBounds(int year, int month, int day,
        DateTimeKind kind = DateTimeKind.Utc) =>
        (GetDayStart(year, month, day, kind), GetDayOpenedEnd(year, month, day, kind));

    public static DateTime GetMonthStart(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
        new(year, month, 1, 0, 0, 0, kind);

    public static DateTime GetMonthOpenedEnd(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
        GetMonthStart(year, month, kind).AddMonths(1);

    public static (DateTime Start, DateTime End) GetMonthOpenedEndBounds(int year, int month,
        DateTimeKind kind = DateTimeKind.Utc) =>
        (GetMonthStart(year, month, kind), GetMonthOpenedEnd(year, month, kind));

    public static DateTime GetQuarterStart(int year, int quarter, DateTimeKind kind = DateTimeKind.Utc) =>
        GetMonthStart(year, (quarter - 1) * QuarterLenInMonths + 1, kind);

    public static DateTime GetQuarterOpenedEnd(int year, int quarter, DateTimeKind kind = DateTimeKind.Utc) =>
        GetMonthOpenedEnd(year, quarter * QuarterLenInMonths, kind);

    public static (DateTime Start, DateTime End) GetQuarterOpenedEndBounds(int year, int quarter,
        DateTimeKind kind = DateTimeKind.Utc) =>
        (GetQuarterStart(year, quarter, kind), GetQuarterOpenedEnd(year, quarter, kind));

    public static DateTime GetHalfYearStart(int year, int halfYear, DateTimeKind kind = DateTimeKind.Utc) =>
        GetMonthStart(year, (halfYear - 1) * HalfYearLenInMonths + 1, kind);

    public static DateTime GetHalfYearOpenedEnd(int year, int halfYear, DateTimeKind kind = DateTimeKind.Utc) =>
        GetMonthOpenedEnd(year, halfYear * HalfYearLenInMonths, kind);

    public static (DateTime Start, DateTime End) GetHalfYearOpenedEndBounds(int year, int halfYear,
        DateTimeKind kind = DateTimeKind.Utc) =>
        (GetHalfYearStart(year, halfYear, kind), GetHalfYearOpenedEnd(year, halfYear, kind));

    public static DateTime GetYearStart(int year, DateTimeKind kind = DateTimeKind.Utc) =>
        GetMonthStart(year, 1, kind);

    public static DateTime GetYearOpenedEnd(int year, DateTimeKind kind = DateTimeKind.Utc) =>
        GetYearStart(year, kind).AddYears(1);

    public static (DateTime Start, DateTime End) GetYearOpenedEndBounds(int year,
        DateTimeKind kind = DateTimeKind.Utc) =>
        (GetYearStart(year, kind), GetYearOpenedEnd(year, kind));
}