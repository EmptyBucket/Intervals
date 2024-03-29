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

using System.Collections.Immutable;

namespace Intervals.Intervals.Enumerable;

internal class OverlapWithEnumerable<T> : MergeEnumerable<T> where T : IEquatable<T>, IComparable<T>
{
    public static IEnumerable<Interval<T>> Create(IEnumerable<Interval<T>> left, IEnumerable<Interval<T>> right)
    {
        var builder = ImmutableList.CreateBuilder<IEnumerable<Interval<T>>>();

        if (left is OverlapWithEnumerable<T> l) builder.AddRange(l.Batches);
        else builder.Add(left);

        if (right is OverlapWithEnumerable<T> r) builder.AddRange(r.Batches);
        else builder.Add(right);

        return new OverlapWithEnumerable<T>(builder.ToImmutable());
    }

    private OverlapWithEnumerable(IImmutableList<IEnumerable<Interval<T>>> batches) : base(batches)
    {
    }

    protected override bool HasDeviation(IReadOnlyList<int> batchBalances) => batchBalances.All(b => b > 0);
}