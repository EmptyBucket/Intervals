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

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> SymmetricDifference<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
        if (left is SymmetricDifferenceEnumerable<T> leftSymmetricDifferenceEnumerable)
        {
            return right is SymmetricDifferenceEnumerable<T> rightSymmetricDifferenceEnumerable
                ? new SymmetricDifferenceEnumerable<T>(
                    leftSymmetricDifferenceEnumerable.Batches.AddRange(rightSymmetricDifferenceEnumerable.Batches))
                : new SymmetricDifferenceEnumerable<T>(leftSymmetricDifferenceEnumerable.Batches.Add(right));
        }
        else
        {
            return right is SymmetricDifferenceEnumerable<T> rightSymmetricDifferenceEnumerable
                ? new SymmetricDifferenceEnumerable<T>(rightSymmetricDifferenceEnumerable.Batches.Insert(0, left))
                : new SymmetricDifferenceEnumerable<T>(ImmutableList.Create(left, right));
        }
    }

    private class SymmetricDifferenceEnumerable<T> : IEnumerable<IInterval<T>> where T : IComparable<T>, IEquatable<T>
    {
        public SymmetricDifferenceEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches) => Batches = batches;

        public IImmutableList<IEnumerable<IInterval<T>>> Batches { get; }

        public IEnumerator<IInterval<T>> GetEnumerator()
        {
            var batches = Batches.Select((ie, i) => new { Intervals = ie, BatchIndex = i }).ToArray();
            var endpoints = batches
                .SelectMany(ie => ie.Intervals, (ie, i) => new { ie.BatchIndex, Interval = i })
                .Where(i => !i.Interval.IsEmpty())
                .SelectMany(i => GetEndpoints(i.Interval), (ie, e) => new { ie.BatchIndex, Endpoint = e })
                .OrderBy(m => m.Endpoint)
                .ToArray();
            var batchBalances = new int[batches.Length];

            var isLeftInvert = false;
            Point<T>? leftBound = null, rightBound = null;
            for (int i = 0, leftEndpointIndex = -1; i < endpoints.Length; i++)
            {
                var isAnyOtherPositive = batchBalances
                    .Where((j, index) => j > 0 && index != endpoints[i].BatchIndex)
                    .Any();

                batchBalances[endpoints[i].BatchIndex] += GetBalance(endpoints[i].Endpoint);
                var exactlyOneBatchIsPositive = batchBalances.Count(b => b > 0) == 1;

                if (exactlyOneBatchIsPositive)
                {
                    (isLeftInvert, leftEndpointIndex) = (isAnyOtherPositive, i);
                    if (rightBound is { })
                    {
                        if (ToNewPoint(endpoints[i].Endpoint, isLeftInvert) is var point &&
                            HasGap(rightBound.Value, point))
                        {
                            var interval = new Interval<T>(leftBound!.Value, rightBound.Value);

                            if (!interval.IsEmpty()) yield return interval;
                            (leftBound, rightBound) = (null!, null!);
                        }
                        else
                            rightBound = null!;
                    }
                }
                else
                {
                    leftBound ??= ToNewPoint(endpoints[leftEndpointIndex].Endpoint, isLeftInvert);
                    rightBound ??= ToNewPoint(endpoints[i].Endpoint, isAnyOtherPositive);
                    leftEndpointIndex = -1;
                }
            }

            if (rightBound is null) yield break;

            var lastInterval = new Interval<T>(leftBound!.Value, rightBound.Value);

            if (!lastInterval.IsEmpty()) yield return lastInterval;
        }

        private static Point<T> ToNewPoint(Point<T> point, bool needsToInvert) =>
            needsToInvert ? point with { Inclusion = point.Inclusion.Invert() } : point;

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