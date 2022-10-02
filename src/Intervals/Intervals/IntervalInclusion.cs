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

// ReSharper disable ShiftExpressionZeroLeftOperand

using Intervals.Points;

namespace Intervals.Intervals;

/// <summary>
/// Represents the inclusion of the interval
/// </summary>
[Flags]
public enum IntervalInclusion
{
    /// <summary>
    /// Both endpoints of the interval are excluded
    /// </summary>
    Opened = Inclusion.Excluded << EndpointLocation.Left | Inclusion.Excluded << EndpointLocation.Right,
    /// <summary>
    /// Left endpoint is excluded and right endpoint of the interval is included
    /// </summary>
    LeftOpened = Inclusion.Excluded << EndpointLocation.Left | Inclusion.Included << EndpointLocation.Right,
    /// <summary>
    /// Left endpoint is included and right endpoint of the interval is excluded
    /// </summary>
    RightOpened = Inclusion.Included << EndpointLocation.Left | Inclusion.Excluded << EndpointLocation.Right,
    /// <summary>
    /// Both endpoints of the interval are included
    /// </summary>
    Closed = Inclusion.Included << EndpointLocation.Left | Inclusion.Included << EndpointLocation.Right
}