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

using System.Collections;
using Intervals.Points;

namespace Intervals.Intervals;

public class Interval<T> : IInterval<T> where T : IComparable<T>, IEquatable<T>
{
    public Interval(Point<T> left, Point<T> right)
    {
        Left = new Endpoint<T>(left, EndpointLocation.Left);
        Right = new Endpoint<T>(right, EndpointLocation.Right);
        Inclusion = IntervalInclusionConvert.FromInclusions(left.Inclusion, right.Inclusion);
    }

    public Interval(T leftValue, T rightValue, IntervalInclusion intervalInclusion = IntervalInclusion.RightOpened)
    {
        var (leftInclusion, rightInclusion) = IntervalInclusionConvert.ToInclusions(intervalInclusion);
        Left = new Endpoint<T>(new Point<T>(leftValue, leftInclusion), EndpointLocation.Left);
        Right = new Endpoint<T>(new Point<T>(rightValue, rightInclusion), EndpointLocation.Right);
        Inclusion = intervalInclusion;
    }

    public Endpoint<T> Left { get; }

    public Endpoint<T> Right { get; }

    public IntervalInclusion Inclusion { get; }

    public bool Equals(IInterval<T>? other) =>
        other is Interval<T> otherInterval && Left == otherInterval.Left && Right == otherInterval.Right;

    public int CompareTo(IInterval<T>? other)
    {
        if (other == null) return 1;

        return Left.CompareTo(other.Left) is var leftCompared && leftCompared != 0
            ? leftCompared
            : Right.CompareTo(other.Right);
    }

    public override int GetHashCode() => HashCode.Combine(Left, Right);

    public IEnumerator<IInterval<T>> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"{Left}, {Right}";
}