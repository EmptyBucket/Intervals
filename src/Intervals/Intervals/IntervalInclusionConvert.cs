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

using Intervals.Points;

namespace Intervals.Intervals;

public static class IntervalInclusionConvert
{
    /// <summary>
    /// Returns the converted inclusion of the interval from the <paramref name="leftInclusion" /> and <paramref name="rightInclusion" /> of the point
    /// </summary>
    /// <param name="leftInclusion"></param>
    /// <param name="rightInclusion"></param>
    /// <returns></returns>
    public static IntervalInclusion FromInclusions(Inclusion leftInclusion, Inclusion rightInclusion) =>
        (IntervalInclusion)((int)leftInclusion << (int)EndpointLocation.Left |
                            (int)rightInclusion << (int)EndpointLocation.Right);

    /// <summary>
    /// Returns the converted inclusion of the point from the <paramref name="intervalInclusion" /> of the interval
    /// </summary>
    /// <param name="intervalInclusion"></param>
    /// <returns></returns>
    public static (Inclusion Left, Inclusion Right) ToInclusions(IntervalInclusion intervalInclusion) =>
        ((Inclusion)((int)intervalInclusion >> (int)EndpointLocation.Left & 1),
            (Inclusion)((int)intervalInclusion >> (int)EndpointLocation.Right & 1));
}