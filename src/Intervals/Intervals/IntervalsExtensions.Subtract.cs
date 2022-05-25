using System.Collections;
using System.Collections.Immutable;
using Intervals.Points;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> Subtract<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right)
        where T : IComparable<T>, IEquatable<T> =>
        left is SubtractEnumerable<T> leftSubtractEnumerable
            ? new SubtractEnumerable<T>(
                leftSubtractEnumerable.MinuendBatch, leftSubtractEnumerable.SubtractBatches.Add(right))
            : new SubtractEnumerable<T>(left, ImmutableList.Create(right));

    private class SubtractEnumerable<T> : IEnumerable<IInterval<T>> where T : IComparable<T>, IEquatable<T>
    {
        public SubtractEnumerable(IEnumerable<IInterval<T>> minuendBatch,
            IImmutableList<IEnumerable<IInterval<T>>> subtractBatches) =>
            (MinuendBatch, SubtractBatches) = (minuendBatch, subtractBatches);

        public IEnumerable<IInterval<T>> MinuendBatch { get; }

        public IImmutableList<IEnumerable<IInterval<T>>> SubtractBatches { get; }

        public IEnumerator<IInterval<T>> GetEnumerator()
        {
            const int minuendBatchIndex = 0;

            Point<T> ToNewPoint(Point<T> point, int batchIndex) =>
                batchIndex == minuendBatchIndex ? point : point with { Inclusion = point.Inclusion.Invert() };

            var batches = new[] { MinuendBatch }
                .Concat(SubtractBatches)
                .Select((ie, i) => new { Intervals = ie, BatchIndex = i })
                .ToArray();
            var endpoints = batches
                .SelectMany(ie => ie.Intervals, (ie, i) => new { ie.BatchIndex, Interval = i })
                .Where(i => !i.Interval.IsEmpty())
                .SelectMany(i => GetEndpoints(i.Interval), (ie, e) => new { ie.BatchIndex, Endpoint = e })
                .OrderBy(m => m.Endpoint)
                .ToArray();
            var batchBalances = new int[batches.Length];

            for (int i = 0, leftEndpointIndex = -1; i < endpoints.Length; i++)
            {
                batchBalances[endpoints[i].BatchIndex] += endpoints[i].Endpoint.ToBalance();
                var minuendBatchIsPositive = batchBalances[minuendBatchIndex] > 0;
                var anySubtractBatchIsPositive = batchBalances.Skip(1).Any(b => b > 0);

                if (leftEndpointIndex < 0 && minuendBatchIsPositive && !anySubtractBatchIsPositive)
                    leftEndpointIndex = i;
                else if (leftEndpointIndex >= 0 && (!minuendBatchIsPositive || anySubtractBatchIsPositive))
                {
                    var interval = new Interval<T>(
                        ToNewPoint(endpoints[leftEndpointIndex].Endpoint, endpoints[leftEndpointIndex].BatchIndex),
                        ToNewPoint(endpoints[i].Endpoint, endpoints[i].BatchIndex));
                    leftEndpointIndex = -1;

                    if (!interval.IsEmpty()) yield return interval;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}