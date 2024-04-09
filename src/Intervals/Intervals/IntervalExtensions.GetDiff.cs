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

using System.Numerics;

namespace Intervals.Intervals;

public static partial class IntervalExtensions
{
#if NET7_0_OR_GREATER
    /// <summary>
    /// Calculates difference of the interval endpoints
    /// </summary>
    /// <param name="interval"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult">Type representing difference of two <see cref="T"/></typeparam>
    /// <returns></returns>
    public static TResult GetDiff<T, TResult>(this Interval<T> interval)
        where T : IComparable<T>, IEquatable<T>, ISubtractionOperators<T, T, TResult>
        where TResult : INumberBase<TResult>
    {
        if (interval.IsEmpty()) return TResult.Zero;

        var (left, right, _) = interval;
        return right - left;
    }
#endif

    /// <summary>
    /// Calculates difference of the <see cref="DateTime"/> interval endpoints
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    public static TimeSpan GetDiff(this Interval<DateTime> interval)
    {
        if (interval.IsEmpty()) return TimeSpan.Zero;

        var (left, right, _) = interval;
        return right - left;
    }
}