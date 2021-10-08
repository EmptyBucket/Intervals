using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PeriodNet.Intervals
{
	public static partial class IntervalsExtensions
	{
		public static IEnumerable<IInterval<T>> Overlap<T>(this IEnumerable<IInterval<T>> left,
			IEnumerable<IInterval<T>> right)
			where T : IComparable<T>, IEquatable<T>
		{
			if (left is OverlapEnumerable<T> leftOverlapEnumerable)
			{
				return right is OverlapEnumerable<T> rightOverlapEnumerable
					? new OverlapEnumerable<T>(leftOverlapEnumerable.Batches.AddRange(rightOverlapEnumerable.Batches))
					: new OverlapEnumerable<T>(leftOverlapEnumerable.Batches.Add(right));
			}
			else
			{
				return right is OverlapEnumerable<T> rightOverlapEnumerable
					? new OverlapEnumerable<T>(rightOverlapEnumerable.Batches.Insert(0, left))
					: new OverlapEnumerable<T>(ImmutableList.Create(left, right));
			}
		}

		private class OverlapEnumerable<T> : IEnumerable<IInterval<T>> where T : IEquatable<T>, IComparable<T>
		{
			public OverlapEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches) => Batches = batches;

			public IImmutableList<IEnumerable<IInterval<T>>> Batches { get; }

			public IEnumerator<IInterval<T>> GetEnumerator()
			{
				var batches = Batches.Select((ie, i) => new {Intervals = ie, BatchIndex = i}).ToArray();
				var endpoints = batches
					.SelectMany(ie => ie.Intervals, (ie, i) => new {ie.BatchIndex, Interval = i})
					.Where(i => !i.Interval.IsEmpty())
					.SelectMany(i => GetEndpoints(i.Interval), (ie, e) => new {ie.BatchIndex, Endpoint = e})
					.OrderBy(m => m.Endpoint)
					.ToArray();
				var batchBalances = new int[batches.Length];

				for (int i = 0, leftEndpointIndex = -1; i < endpoints.Length; i++)
				{
					batchBalances[endpoints[i].BatchIndex] += endpoints[i].Endpoint.ToBalance();
					var allBatchIsPositive = batchBalances.All(b => b > 0);

					if (leftEndpointIndex < 0 && allBatchIsPositive) leftEndpointIndex = i;
					else if (leftEndpointIndex >= 0 && !allBatchIsPositive)
					{
						var interval = new Interval<T>(endpoints[leftEndpointIndex].Endpoint, endpoints[i].Endpoint);
						leftEndpointIndex = -1;

						if (!interval.IsEmpty()) yield return interval;
					}
				}
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}