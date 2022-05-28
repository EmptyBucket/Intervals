using System.Collections;
using System.Collections.Immutable;
using Intervals.Points;
using Intervals.Utils;

namespace Intervals.Intervals.Enumerable;

internal abstract class MergeEnumerable<T> : IEnumerable<IInterval<T>>
    where T : IEquatable<T>, IComparable<T>
{
    protected MergeEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches)
    {
        Batches = batches;
    }

    public IImmutableList<IEnumerable<IInterval<T>>> Batches { get; }

    public IEnumerator<IInterval<T>> GetEnumerator()
    {
        var endpoints = Batches
            .Select((b, bIdx) =>
            {
                var endpoints = b.Where(i => !i.IsEmpty()).SelectMany(i => GetEndpoints(i, bIdx));
                return b is MergeEnumerable<T> ? endpoints : endpoints.OrderBy(e => e.Endpoint);
            })
            .Aggregate((a, n) => a.Merge(n, e => e.Endpoint))
            .ToArray();
        var batchBalances = new int[Batches.Count];

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