namespace Intervals.Utils;

internal static class DateTimeHelper
{
    private const int QuarterLenInMonths = 3;
    private const int HalfAYearLenInMonths = 6;

    public static int GetQuarter(this DateTime dateTime) =>
        dateTime.GetIntervalNumberInYear(QuarterLenInMonths);

    public static int GetHalfAYear(this DateTime dateTime) =>
        dateTime.GetIntervalNumberInYear(HalfAYearLenInMonths);

    public static DateTime GetStartOfMonth(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
        new(year, month, 1, 0, 0, 0, kind);

    public static DateTime GetOpenedEndOfMonth(int year, int month, DateTimeKind kind = DateTimeKind.Utc) =>
        GetStartOfMonth(year, month, kind).AddMonths(1);

    public static DateTime GetStartOfQuarter(int year, int quarter, DateTimeKind kind = DateTimeKind.Utc) =>
        GetStartOfMonth(year, (quarter - 1) * QuarterLenInMonths + 1, kind);

    public static DateTime GetOpenedEndOfQuarter(int year, int quarter, DateTimeKind kind = DateTimeKind.Utc) =>
        GetOpenedEndOfMonth(year, quarter * QuarterLenInMonths, kind);

    public static DateTime GetStartOfHalfAYear(int year, int halfAYear, DateTimeKind kind = DateTimeKind.Utc) =>
        GetStartOfMonth(year, (halfAYear - 1) * HalfAYearLenInMonths + 1, kind);

    public static DateTime GetOpenedEndOfHalfAYear(int year, int halfAYear, DateTimeKind kind = DateTimeKind.Utc) =>
        GetOpenedEndOfMonth(year, halfAYear * HalfAYearLenInMonths, kind);

    public static DateTime GetStartOfYear(int year, DateTimeKind kind = DateTimeKind.Utc) =>
        GetStartOfMonth(year, 1, kind);

    public static DateTime GetOpenedEndOfYear(int year, DateTimeKind kind = DateTimeKind.Utc) =>
        GetOpenedEndOfMonth(year, 12, kind);

    private static int GetIntervalNumberInYear(this DateTime dateTime, int intervalLen) =>
        (dateTime.Month - 1) / intervalLen + 1;
}