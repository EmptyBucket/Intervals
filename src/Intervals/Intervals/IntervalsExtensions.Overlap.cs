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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Intervals.Intervals
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