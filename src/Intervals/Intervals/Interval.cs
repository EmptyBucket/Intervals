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

/// <summary>
/// Represents an interval instance
/// </summary>
public record class Interval<T> : IComparable<Interval<T>>
    where T : IComparable<T>, IEquatable<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.Intervals.Interval"/>
    /// with specified <paramref name="leftPoint" /> and <paramref name="rightPoint" />
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    public Interval(Point<T> leftPoint, Point<T> rightPoint)
    {
        Left = Endpoint.Left(leftPoint);
        Right = Endpoint.Right(rightPoint);
        Inclusion = IntervalInclusionConvert.FromInclusions(leftPoint.Inclusion, rightPoint.Inclusion);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.Intervals.Interval"/>
    /// with specified <paramref name="leftValue" />, <paramref name="rightValue" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="rightValue"></param>
    /// <param name="inclusion"></param>
    public Interval(T leftValue, T rightValue, IntervalInclusion inclusion = IntervalInclusion.RightOpened)
    {
        var (leftInclusion, rightInclusion) = IntervalInclusionConvert.ToInclusions(inclusion);
        Left = Endpoint.Left(leftValue, leftInclusion);
        Right = Endpoint.Right(rightValue, rightInclusion);
        Inclusion = inclusion;
    }

    /// <summary>
    /// Deconstructs instance into the specified <paramref name="left" /> and <paramref name="right" />
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public void Deconstruct(out Point<T> left, out Point<T> right)
    {
        left = Left;
        right = Right;
    }

    /// <summary>
    /// Deconstructs instance into the specified <paramref name="left" />, <paramref name="right" /> and <paramref name="inclusion" />
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="inclusion"></param>
    public void Deconstruct(out T left, out T right, out IntervalInclusion inclusion)
    {
        left = Left.Value;
        right = Right.Value;
        inclusion = Inclusion;
    }

    /// <summary>
    /// Left endpoint of the interval
    /// </summary>
    public Endpoint<T> Left { get; }

    /// <summary>
    /// Right endpoint of the interval
    /// </summary>
    public Endpoint<T> Right { get; }

    /// <summary>
    /// Inclusion of the interval
    /// </summary>
    public IntervalInclusion Inclusion { get; }

    /// <summary>
    /// Returns true if interval is empty, otherwise returns false
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() => Left.Value.CompareTo(Right.Value) is var compareTo &&
                             (Inclusion == IntervalInclusion.Closed ? compareTo > 0 : compareTo >= 0);

    /// <summary>
    /// Compares this instance to a specified <paramref name="other" /> and returns an indication of their relative values
    /// First, the instances are compared on the left endpoints, then, if they are equal, on the right
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Interval<T>? other)
    {
        if (other == null) return 1;

        return Left.CompareTo(other.Left) is var leftCompared && leftCompared != 0
            ? leftCompared
            : Right.CompareTo(other.Right);
    }

    /// <summary>
    /// Returns a value indicating whether this instance is equal to a specified <paramref name="other" /> value
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Interval<T>? other) => other is not null && Left == other.Left && Right == other.Right;

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Left, Right);

    /// <summary>
    /// Converts the value of this instance to "{[(,),[,]]}{Left.Value}, {Right.Value}{[(,),[,]]}" format
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{Left}, {Right}";
}