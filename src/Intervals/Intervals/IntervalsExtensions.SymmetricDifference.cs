using System.Collections;
using System.Collections.Immutable;

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

	//todo кажется, что это можно выразить через другие операции
	private class SymmetricDifferenceEnumerable<T> : IEnumerable<IInterval<T>> where T : IComparable<T>, IEquatable<T>
	{
		public SymmetricDifferenceEnumerable(IImmutableList<IEnumerable<IInterval<T>>> batches) => Batches = batches;

		public IImmutableList<IEnumerable<IInterval<T>>> Batches { get; }

		public IEnumerator<IInterval<T>> GetEnumerator()
		{
			IPoint<T> ToNewPoint(IPoint<T> point, bool needsToInvert) =>
				needsToInvert ? point.WithInvertedInclusion() : point;

			bool HasExactlyOne(IEnumerable<int> enumerable)
			{
				using var enumerator = enumerable.GetEnumerator();
				return enumerator.MoveNext() && !enumerator.MoveNext();
			}

			bool HasGap(IPoint<T> first, IPoint<T> second) =>
				!first.Value.Equals(second.Value) || first.Inclusion == second.Inclusion;

			var batches = Batches.Select((ie, i) => new {Intervals = ie, BatchIndex = i}).ToArray();
			var endpoints = batches
				.SelectMany(ie => ie.Intervals, (ie, i) => new {ie.BatchIndex, Interval = i})
				.Where(i => !i.Interval.IsEmpty())
				.SelectMany(i => GetEndpoints(i.Interval), (ie, e) => new {ie.BatchIndex, Endpoint = e})
				.OrderBy(m => m.Endpoint)
				.ToArray();
			var batchBalances = new int[batches.Length];

			var isLeftInvert = false;
			IPoint<T> leftBound = null!, rightBound = null!;
			for (int i = 0, leftEndpointIndex = -1; i < endpoints.Length; i++)
			{
				var isAnyOtherPositive = batchBalances
					.Where((j, index) => j > 0 && index != endpoints[i].BatchIndex)
					.Any();
					
				batchBalances[endpoints[i].BatchIndex] += endpoints[i].Endpoint.ToBalance();
				var positiveBatchBalances = batchBalances.Where(b => b > 0);
				var exactlyOneBatchIsPositive = HasExactlyOne(positiveBatchBalances);

				if (exactlyOneBatchIsPositive)
				{
					(isLeftInvert, leftEndpointIndex) = (isAnyOtherPositive, i);
					if (rightBound is { })
					{
						if (ToNewPoint(endpoints[i].Endpoint, isLeftInvert) is var point && HasGap(rightBound, point))
						{
							var interval = new Interval<T>(leftBound, rightBound);

							if (!interval.IsEmpty()) yield return interval;
							(leftBound, rightBound) = (null!, null!);
						}
						else
							rightBound = null!;
					}
				}
				else
				{
					// ReSharper disable once ConstantNullCoalescingCondition
					leftBound ??= ToNewPoint(endpoints[leftEndpointIndex].Endpoint, isLeftInvert);
					rightBound ??= ToNewPoint(endpoints[i].Endpoint, isAnyOtherPositive);
					leftEndpointIndex = -1;
				}
			}

			if (rightBound is null) yield break;

			var lastInterval = new Interval<T>(leftBound, rightBound);

			if (!lastInterval.IsEmpty()) yield return lastInterval;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}