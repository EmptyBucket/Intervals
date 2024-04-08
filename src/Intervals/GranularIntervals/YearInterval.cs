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

using System.Text.Json.Serialization;
using Intervals.Intervals;
using Intervals.Utils;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an year interval instance
/// </summary>
[Serializable]
public record class YearInterval : MonthGranularInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.YearlyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="granuleLength" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="granuleLength"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public YearInterval(DateTime leftValue, TimeSpan granuleLength,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, granuleLength, DateTimeHelper.MonthsInYear, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.YearInterval"/>
    /// with specified <paramref name="year" />, <paramref name="granuleLength" /> and <paramref name="kind" />
    /// </summary>
    /// <param name="year"></param>
    /// <param name="granuleLength"></param>
    /// <param name="kind"></param>
    public YearInterval(int year, TimeSpan granuleLength, DateTimeKind kind = DateTimeKind.Unspecified)
        : this(DateTimeHelper.New(year, 1, 1, kind), granuleLength)
    {
    }
}