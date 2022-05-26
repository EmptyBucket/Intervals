using System.Collections;
using System.Collections.Immutable;
using Intervals.Utils;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> Combine<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T>
    {
        var l = (left as CombineEnumerable<T>)?.Batches ?? ImmutableList.Create(left);
        var r = (left as CombineEnumerable<T>)?.Batches ?? ImmutableList.Create(right);
        return new CombineEnumerable<T>(l.AddRange(r));
    }

    private class CombineEnumerable<T> : IOrderedEnumerable<IInterval<T>> where T : IEquatable<T>, IComparable<T>
    {
        public CombineEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches)
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
                batchBalances[endpoints[i].BatchIndex] += endpoints[i].Endpoint.GetBalance();
                (var prevDeviation, deviation) = (deviation, batchBalances.Any(b => b > 0));

                if (!prevDeviation && deviation)
                {
                    left = i;

                    if (prevRight >= 0 && HasGap<T>(endpoints[prevRight].Endpoint, endpoints[left].Endpoint))
                    {
                        var interval = new Interval<T>(endpoints[prevLeft].Endpoint, endpoints[prevRight].Endpoint);
                        (prevLeft, prevRight) = (-1, -1);

                        if (!interval.IsEmpty()) yield return interval;
                    }
                }
                else if (prevDeviation && !deviation) (prevLeft, prevRight) = (prevLeft < 0 ? left : prevLeft, i);
            }

            if (prevRight >= 0)
            {
                var interval = new Interval<T>(endpoints[prevLeft].Endpoint, endpoints[prevRight].Endpoint);

                if (!interval.IsEmpty()) yield return interval;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}