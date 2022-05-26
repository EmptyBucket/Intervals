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
        static IEnumerable<EndpointContext> GetEndpoints(IInterval<T> interval, int batchIndex)
        {
            yield return new EndpointContext(interval.Left, batchIndex);
            yield return new EndpointContext(interval.Right, batchIndex);
        }

        static int GetBalance(EndpointContext endpointContext) => (int)endpointContext.Endpoint.Location * 2 - 1;

        var endpoints = _batches
            .Select((b, bIdx) =>
            {
                var endpoints = b.Where(i => !i.IsEmpty()).SelectMany(i => GetEndpoints(i, bIdx));
                return b is MergeEnumerable<IInterval<T>> ? endpoints : endpoints.OrderBy(e => e.Endpoint);
            })
            .Aggregate((a, n) => a.Merge(n, c => c.Endpoint))
            .ToArray();
        var batchBalances = new int[_batches.Count];
        var (pLeft, pRight) = (-1, -1);

        for (var (i, left, deviation) = (0, -1, false); i < endpoints.Length; i++)
        {
            batchBalances[endpoints[i].BatchIndex] += GetBalance(endpoints[i]);
            (var pDeviation, deviation) = (deviation, HasDeviation(batchBalances));

            if (!pDeviation && deviation)
            {
                left = i;

                if (pRight >= 0 && HasGap(endpoints[pRight], endpoints[left]))
                {
                    var interval = CreateInterval(endpoints[pLeft], endpoints[pRight]);
                    (pLeft, pRight) = (-1, -1);

                    if (!interval.IsEmpty()) yield return interval;
                }
            }
            else if (pDeviation && !deviation) (pLeft, pRight) = (pLeft >= 0 ? pLeft : left, i);
        }

        if (pRight >= 0)
        {
            var interval = CreateInterval(endpoints[pLeft], endpoints[pRight]);

            if (!interval.IsEmpty()) yield return interval;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    protected abstract IInterval<T> CreateInterval(EndpointContext leftContext, EndpointContext rightContext);

    protected abstract bool HasGap(EndpointContext leftContext, EndpointContext rightContext);

    protected abstract bool HasDeviation(IReadOnlyList<int> batchBalances);

    protected readonly record struct EndpointContext(Endpoint<T> Endpoint, int BatchIndex);
}