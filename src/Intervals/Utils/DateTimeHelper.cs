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
    public const int MonthsInQuarter = 3;
    public const int MonthsInHalfYear = 6;
    public const int MonthsInYear = 12;

    public static int QuarterToMonth(int quarter)
    {
        if (quarter is < 1 or > 4) throw new ArgumentException($"{quarter} must be more than zero and less than four");
        
        return (quarter - 1) * MonthsInQuarter + 1;
    }

    public static int HalfYearToMonth(int halfYear)
    {
        if (halfYear is < 1 or > 2) throw new ArgumentException($"{halfYear} must be more than zero and less than two");
        
        return (halfYear - 1) * MonthsInHalfYear + 1;
    }

    public static DateTime GetMonthStart(int year, int month, DateTimeKind kind = DateTimeKind.Unspecified) =>
        new(year, month, 1, 0, 0, 0, kind);
}