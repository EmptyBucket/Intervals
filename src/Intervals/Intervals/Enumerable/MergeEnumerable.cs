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

namespace Intervals.Intervals.Enumerable;

internal abstract class MergeEnumerable<T> : IEnumerable<IInterval<T>>
    where T : IEquatable<T>, IComparable<T>
{
    private readonly IImmutableList<IEnumerable<IInterval<T>>> _batches;

    protected MergeEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
    {
        var l = (left as MergeEnumerable<T>)?._batches ?? ImmutableList.Create(left);
        var r = (left as MergeEnumerable<T>)?._batches ?? ImmutableList.Create(right);
        _batches = l.AddRange(r);
    }

    public IEnumerator<IInterval<T>> GetEnumerator()
    {
        var endpoints = _batches
            .Select((b, bIdx) =>
            {
                var endpoints = b.Where(i => !i.IsEmpty()).SelectMany(i => GetEndpoints(i, bIdx));
                return b is MergeEnumerable<IInterval<T>> ? endpoints : endpoints.OrderBy(e => e.Endpoint);
            })
            .Aggregate((a, n) => a.Merge(n, e => e.Endpoint))
            .ToArray();
        var batchBalances = new int[_batches.Count];

        var deviation = false;
        IInterval<T>? interval = null;
        Endpoint<T>? left = null;

        foreach (var endpoint in endpoints)
        {
            batchBalances[endpoint.BatchIndex] += GetBalance(endpoint);

            (var pDeviation, deviation) = (deviation, HasDeviation(batchBalances));
            if (pDeviation == deviation) continue;

            if (deviation) left = endpoint;
            else
            {
                (var pInterval, interval) = (interval, CreateInterval(left!.Value, endpoint));

                if (!IsNullOrEmpty(pInterval))
                    if (PointExtensions.HasGap<T>(pInterval!.Right, interval.Left)) yield return pInterval;
                    else interval = CreateInterval(pInterval.Left, interval.Right);
            }
        }

        if (!IsNullOrEmpty(interval)) yield return interval!;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    protected abstract bool HasDeviation(IReadOnlyList<int> batchBalances);

    private static int GetBalance(Endpoint<T> endpoint) => (int)endpoint.Location * 2 - 1;

    private static bool IsNullOrEmpty(IInterval<T>? interval) => interval is null || interval.IsEmpty();

    private static IInterval<T> CreateInterval(Endpoint<T> leftEndpoint, Endpoint<T> rightEndpoint)
    {
        var (lValue, lInclusion, lLocation) = leftEndpoint;
        var (rValue, rInclusion, rLocation) = rightEndpoint;
        var lPoint = new Point<T>(lValue, lLocation == EndpointLocation.Right ? lInclusion.Invert() : lInclusion);
        var rPoint = new Point<T>(rValue, rLocation == EndpointLocation.Left ? rInclusion.Invert() : rInclusion);
        return new Interval<T>(lPoint, rPoint);
    }

    private static IEnumerable<EndpointContext> GetEndpoints(IInterval<T> interval, int batchIndex)
    {
        yield return new EndpointContext(interval.Left, batchIndex);
        yield return new EndpointContext(interval.Right, batchIndex);
    }

    private readonly record struct EndpointContext(Endpoint<T> Endpoint, int BatchIndex)
    {
        public static implicit operator Endpoint<T>(EndpointContext context) => context.Endpoint;
    }
}