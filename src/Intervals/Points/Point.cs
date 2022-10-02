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

namespace Intervals.Points;

public static class Point
{
    /// <summary>
    /// Returns point with specified <paramref name="value" /> and Inclusion.Included
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Point<T> Included<T>(T value) where T : IEquatable<T> => new(value, Inclusion.Included);

    /// <summary>
    /// Returns point with specified <paramref name="value" /> and Inclusion.Excluded
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Point<T> Excluded<T>(T value) where T : IEquatable<T> => new(value, Inclusion.Excluded);
}

/// <summary>
/// Represents an point instance
/// </summary>
/// <param name="Value"></param>
/// <param name="Inclusion"></param>
/// <typeparam name="T"></typeparam>
public readonly record struct Point<T>(T Value, Inclusion Inclusion) where T : IEquatable<T>
{
    /// <summary>
    /// Converts the value of this instance to "{Value}-{Inclusion}" format
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{Value}-{Inclusion}";
}