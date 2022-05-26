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
using System.Collections.Immutable;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> SymmetricDifference<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
            var l = (left as SymmetricDifferenceEnumerable<T>)?.Batches ?? ImmutableList.Create(left);
            var r = (left as SymmetricDifferenceEnumerable<T>)?.Batches ?? ImmutableList.Create(right);
            return new SymmetricDifferenceEnumerable<T>(l.AddRange(r));
    }

    private class SymmetricDifferenceEnumerable<T> : IEnumerable<IInterval<T>> where T : IComparable<T>, IEquatable<T>
    {
        private int _minuendBatchIndex = -1;

        public SymmetricDifferenceEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches)
        {
            Batches = batches;
        }

        public IImmutableList<IEnumerable<IInterval<T>>> Batches { get; }

        public IEnumerator<IInterval<T>> GetEnumerator()
        {
            var endpoints = Batches
                .Select((i, idx) =>
                {
                    var endpoints =
                        i.Where(il => !il.IsEmpty()).SelectMany(GetEndpoints, (_, e) => (Endpoint: e, BatchIndex: idx));
                    return i is IOrderedEnumerable<IInterval<T>> ? endpoints : endpoints.OrderBy(e => e.Endpoint);
                })
                .Aggregate(EnumerableExtensions.MergeAscending)
                .ToArray();
            var batchBalances = new int[Batches.Count];
            var (prevLeft, prevRight) = (-1, -1);

            for (var (i, left, deviation) = (0, -1, false); i < endpoints.Length; i++)
            {
                batchBalances[endpoints[i].BatchIndex] += GetBalance(endpoints[i].Endpoint);
                (var prevDeviation, deviation) = (deviation, GetDeviance(batchBalances));

                if (!prevDeviation && deviation)
                {
                    left = i;

                    if (prevRight >= 0 && HasGap(endpoints[prevRight].Endpoint, endpoints[left].Endpoint))
                    {
                        var interval = new Interval<T>(
                            CreatePoint(endpoints[prevLeft].Endpoint, endpoints[prevLeft].BatchIndex),
                            CreatePoint(endpoints[prevRight].Endpoint, endpoints[prevRight].BatchIndex));
                        (prevLeft, prevRight) = (-1, -1);

                        if (!interval.IsEmpty()) yield return interval;
                    }
                    
                    _minuendBatchIndex = endpoints[i].BatchIndex;
                }
                else if (prevDeviation && !deviation) (prevLeft, prevRight) = (prevLeft < 0 ? left : prevLeft, i);
            }

            if (prevRight >= 0)
            {
                var interval = new Interval<T>(
                    CreatePoint(endpoints[prevLeft].Endpoint, endpoints[prevLeft].BatchIndex),
                    CreatePoint(endpoints[prevRight].Endpoint, endpoints[prevRight].BatchIndex));

                if (!interval.IsEmpty()) yield return interval;
            }
        }

        private static bool GetDeviance(IReadOnlyList<int> batchBalances) => batchBalances.Count(b => b > 0) == 1;

        private Point<T> CreatePoint(Point<T> point, int batchIndex) => 
            batchIndex == _minuendBatchIndex ? point : point with { Inclusion = point.Inclusion.Invert() };

        private static bool HasGap(Point<T> first, Point<T> second) =>
            !first.Value.Equals(second.Value) || first.Inclusion == second.Inclusion;

        private static int GetBalance(Endpoint<T> endpoint) => (int)endpoint.Location * 2 - 1;

        private static IEnumerable<Endpoint<T>> GetEndpoints(IInterval<T> interval)
        {
            yield return interval.Left;
            yield return interval.Right;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}