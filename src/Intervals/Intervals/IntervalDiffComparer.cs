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

/// <summary>
/// Compares intervals by their difference in endpoints using custom difference computer
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TResult">Length type</typeparam>
public class IntervalDiffComparer<T, TResult> : IComparer<Interval<T>>
    where T : IComparable<T>, IEquatable<T>
    where TResult : IComparable<TResult>
{
    private readonly Func<Interval<T>, TResult> _diffSelector;

    public IntervalDiffComparer(Func<Interval<T>, TResult> diffSelector) => _diffSelector = diffSelector;

    /// <summary>
    /// Compares two intervals
    /// </summary>
    /// <param name="x">First interval</param>
    /// <param name="y">Second interval</param>
    /// <returns>Comparison result</returns>
    public int Compare(Interval<T>? x, Interval<T>? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;

        return _diffSelector(x).CompareTo(_diffSelector(y));
    }
}

/// <summary>
/// Compares <see cref="DateTime"/> intervals by their difference using default difference computer
/// </summary>
public class DefaultDateTimeIntervalDiffComparer : IntervalDiffComparer<DateTime, TimeSpan>
{
    public DefaultDateTimeIntervalDiffComparer() : base(IntervalExtensions.GetDiff)
    {
    }
}

#if NET7_0_OR_GREATER
/// <summary>
/// Compares intervals by their difference using default difference computer
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TResult"></typeparam>
public class DefaultIntervalDiffComparer<T, TResult> : IntervalDiffComparer<T, TResult>
    where T : IComparable<T>, IEquatable<T>, ISubtractionOperators<T, T, TResult>
    where TResult : INumberBase<TResult>, IComparable<TResult>
{
    public DefaultIntervalDiffComparer() : base(IntervalExtensions.GetDiff<T, TResult>)
    {
    }
}
#endif