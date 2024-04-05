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
using Intervals.Points;

namespace Intervals.GranularIntervals;

/// <summary>
/// Represents an hourly interval instance where the granule size is hour
/// </summary>
[Serializable]
public record class HourlyInterval : TimeGranularInterval
{
    private new static readonly TimeSpan GranuleLength = TimeSpan.FromHours(1);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.HourlyInterval"/>
    /// with specified <paramref name="leftPoint" /> and <paramref name="rightPoint" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    public HourlyInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint)
        : base(leftPoint, rightPoint, GranuleLength)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.HourlyInterval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="inclusion"></param>
    [JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public HourlyInterval(DateTime leftValue, DateTime rightValue,
        IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, rightValue, GranuleLength, inclusion)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.GranularIntervals.HourlyInterval"/>
    /// with specified <paramref name="leftValue" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="inclusion"></param>
    public HourlyInterval(DateTime leftValue, IntervalInclusion inclusion = IntervalInclusion.RightOpened)
        : base(leftValue, GranuleLength, inclusion)
    {
    }
}