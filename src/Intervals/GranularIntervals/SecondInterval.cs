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

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an second interval instance where the granule size is equal to a second
/// </summary>
public class SecondInterval : TimeGranularInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.SecondInterval"/>
    /// with specified <paramref name="year" />, <paramref name="month" />, <paramref name="day" />,
    /// <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    /// <param name="second"></param>
    /// <param name="kind"></param>
    public SecondInterval(int year, int month, int day, int hour, int minute, int second,
        DateTimeKind kind = DateTimeKind.Unspecified) : base(
        new DateTime(year, month, day, hour, minute, second, kind),
        new DateTime(year, month, day, hour, minute, second, kind).AddSeconds(1))
    {
        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
        Second = second;
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

    /// <summary>
    /// Hour
    /// </summary>
    public int Hour { get; }

    /// <summary>
    /// Minute
    /// </summary>
    public int Minute { get; }

    /// <summary>
    /// Second
    /// </summary>
    public int Second { get; }
}