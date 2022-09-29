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

internal static class DateTimeExtensions
{
    public static DateTime Round(this DateTime dateTime, TimeSpan component,
        MidpointRounding midpointRounding = MidpointRounding.ToEven)
    {
        var residue = dateTime.Ticks % component.Ticks;
        var append = (long)Math.Round((double)residue / component.Ticks, midpointRounding) * component.Ticks;
        return new DateTime(dateTime.Ticks - residue + append, dateTime.Kind);
    }

    public static DateTime Ceiling(this DateTime dateTime, TimeSpan component)
    {
        var residue = dateTime.Ticks % component.Ticks;
        var append = Convert.ToInt64(residue > 0) * component.Ticks;
        return new DateTime(dateTime.Ticks - residue + append, dateTime.Kind);
    }

    public static DateTime Floor(this DateTime dateTime, TimeSpan component)
    {
        var residue = dateTime.Ticks % component.Ticks;
        return new DateTime(dateTime.Ticks - residue, dateTime.Kind);
    }
}