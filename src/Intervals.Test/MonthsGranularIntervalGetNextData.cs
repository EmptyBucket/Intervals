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
using NUnit.Framework;

namespace PeriodNet.Test
{
	public class MonthsGranularIntervalGetNextData : IEnumerable
	{
		public IEnumerator GetEnumerator()
		{
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2021, 2, 1),
					new DateTime(2021, 2, 1), new DateTime(2021, 3, 1))
				.SetName("WhenMonth_ReturnNextMonth");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2021, 3, 1),
					new DateTime(2021, 3, 1), new DateTime(2021, 5, 1))
				.SetName("WhenMonths_ReturnNextMonths");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2022, 1, 1),
					new DateTime(2022, 1, 1), new DateTime(2023, 1, 1))
				.SetName("WhenYear_ReturnNextYear");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2022, 3, 1),
					new DateTime(2022, 3, 1), new DateTime(2023, 5, 1))
				.SetName("WhenYearAndMonths_ReturnNextYearAndMonths");
			yield return new TestCaseData(
					new DateTime(2021, 1, 1), new DateTime(2023, 3, 1),
					new DateTime(2023, 3, 1), new DateTime(2025, 5, 1))
				.SetName("WhenYearsAndMonths_ReturnNextYearsAndMonths");
		}
	}
}