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

using FluentAssertions;
using NUnit.Framework;
using PeriodNet.Intervals;

namespace PeriodNet.Test
{
	public class IntervalSymmetricDifferenceTests
	{
		[TestCaseSource(typeof(SingleIntervalsSymmetricDifferenceData))]
		public void SymmetricDifference_WhenSingleIntervals(IInterval<int> first, IInterval<int> second,
			IInterval<int>[] result)
		{
			var actual1 = first.SymmetricDifference(second);
			var actual2 = second.SymmetricDifference(first);

			actual1.Should().Equal(result);
			actual2.Should().Equal(result);
		}

		[TestCaseSource(typeof(MultipleIntervalsSymmetricDifferenceData))]
		public void SymmetricDifference_WhenMultipleIntervals(IInterval<int>[] first, IInterval<int>[] second,
			IInterval<int>[] result)
		{
			var actual1 = first.SymmetricDifference(second);
			var actual2 = second.SymmetricDifference(first);

			actual1.Should().Equal(result);
			actual2.Should().Equal(result);
		}
	}
}