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
using Intervals.Intervals;

namespace Intervals.GranularIntervals
{
	public abstract class GranularInterval<TInterval> : Interval<DateTime>, IGranularInterval<DateTime, TInterval>
		where TInterval : IGranularInterval<DateTime>
	{
		private readonly TimeSpan _granuleSize;

		protected GranularInterval(IPoint<DateTime> left, IPoint<DateTime> right)
			: base(left, right) =>
			_granuleSize = ComputeGranuleSize(left.Value, right.Value);

		protected GranularInterval(DateTime leftValue, DateTime rightValue, IntervalType intervalType)
			: base(leftValue, rightValue, intervalType) =>
			_granuleSize = ComputeGranuleSize(leftValue, rightValue);

		public TInterval GetPrev() => AddBatches(-1);

		public TInterval GetNext() => AddBatches(1);

		private static TimeSpan ComputeGranuleSize(DateTime leftValue, DateTime rightValue) => rightValue - leftValue;

		private TInterval AddBatches(int batchesCount)
		{
			var totalGranulesSize = _granuleSize * batchesCount;
			return Create(
				new Point<DateTime>(Left.Value + totalGranulesSize, Right.Inclusion.Invert()),
				new Point<DateTime>(Right.Value + totalGranulesSize, Left.Inclusion.Invert()));
		}

		protected abstract TInterval Create(Point<DateTime> left, Point<DateTime> right);
	}

	public class GranularInterval : GranularInterval<GranularInterval>
	{
		public GranularInterval(IPoint<DateTime> left, IPoint<DateTime> right)
			: base(left, right)
		{
		}

		public GranularInterval(DateTime leftValue, DateTime rightValue, IntervalType intervalType = IntervalType.RightOpen)
			: base(leftValue, rightValue, intervalType)
		{
		}

		protected override GranularInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new GranularInterval(left, right);
	}
}