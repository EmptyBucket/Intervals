using System;

namespace PeriodNet
{
	internal static class DateTimeHelper
	{
		private const int QuarterLenInMonths = 3;
		private const int HalfAYearLenInMonths = 6;

		public static int GetQuarter(this DateTime dateTime) => dateTime.GetIntervalNumberInYear(QuarterLenInMonths);

		public static int GetHalfAYear(this DateTime dateTime) => dateTime.GetIntervalNumberInYear(HalfAYearLenInMonths);

		public static DateTime GetStartOfMonth(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
			new DateTime(year, month, 1, 0, 0, 0, kind);

		public static DateTime GetOpenEndOfMonth(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, month, kind).AddMonths(1);

		public static DateTime GetStartOfQuarter(int year, int quarterNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, (quarterNumber - 1) * QuarterLenInMonths + 1, kind);

		public static DateTime GetOpenEndOfQuarter(int year, int quarterNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetOpenEndOfMonth(year, quarterNumber * QuarterLenInMonths, kind);

		public static DateTime GetStartOfHalfAYear(int year, int halfAYearNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, (halfAYearNumber - 1) * HalfAYearLenInMonths + 1, kind);

		public static DateTime GetOpenEndOfHalfAYear(int year, int halfAYearNumber, DateTimeKind kind = DateTimeKind.Utc) =>
			GetOpenEndOfMonth(year, halfAYearNumber * HalfAYearLenInMonths, kind);

		public static DateTime GetStartOfYear(int year, DateTimeKind kind = DateTimeKind.Utc) =>
			GetStartOfMonth(year, 1, kind);

		public static DateTime GetOpenEndOfYear(int year, DateTimeKind kind = DateTimeKind.Utc) =>
			GetOpenEndOfMonth(year, 12, kind);

		private static int GetIntervalNumberInYear(this DateTime dateTime, int intervalLen) =>
			(dateTime.Month - 1) / intervalLen + 1;
	}
}