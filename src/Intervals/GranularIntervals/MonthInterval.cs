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
	public class MonthInterval : MonthsGranularInterval<MonthInterval>
	{
		public MonthInterval(int year, int month)
			: base(DateTimeHelper.GetStartOfMonth(year, month), DateTimeHelper.GetOpenEndOfMonth(year, month),
				IntervalType.RightOpen) =>
			(Year, Month) = (year, month);

		private MonthInterval(Point<DateTime> left, Point<DateTime> right) : base(left, right) =>
			(Year, Month) = (left.Value.Year, left.Value.Month);

		public int Year { get; }

		public int Month { get; }

		protected override MonthInterval Create(Point<DateTime> left, Point<DateTime> right) =>
			new MonthInterval(left, right);
	}
}