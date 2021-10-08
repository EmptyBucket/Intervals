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

namespace Intervals
{
	internal static class DateTimeHelper
	{
		private const int QuarterLenInMonths = 3;
		private const int HalfAYearLenInMonths = 6;

		public static int GetQuarter(this DateTime dateTime) => dateTime.GetIntervalNumberInYear(QuarterLenInMonths);

		public static int GetHalfAYear(this DateTime dateTime) => dateTime.GetIntervalNumberInYear(HalfAYearLenInMonths);

		public static DateTime GetStartOfMonth(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
			new DateTime(year, month, 1, 0, 0, 0, kind);

		public static DateTime GetOpenedEndOfMonth(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, month, kind).AddMonths(1);

		public static DateTime GetStartOfQuarter(int year, int quarterNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, (quarterNumber - 1) * QuarterLenInMonths + 1, kind);

		public static DateTime GetOpenedEndOfQuarter(int year, int quarterNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetOpenedEndOfMonth(year, quarterNumber * QuarterLenInMonths, kind);

		public static DateTime GetStartOfHalfAYear(int year, int halfAYearNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, (halfAYearNumber - 1) * HalfAYearLenInMonths + 1, kind);

		public static DateTime GetOpenedEndOfHalfAYear(int year, int halfAYearNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetOpenedEndOfMonth(year, halfAYearNumber * HalfAYearLenInMonths, kind);

		public static DateTime GetStartOfYear(int year, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, 1, kind);

		public static DateTime GetOpenedEndOfYear(int year, DateTimeKind kind = DateTimeKind.Utc) =>
			GetOpenedEndOfMonth(year, 12, kind);

		private static int GetIntervalNumberInYear(this DateTime dateTime, int intervalLen) =>
			(dateTime.Month - 1) / intervalLen + 1;
	}
}