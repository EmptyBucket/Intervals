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

using Intervals.GranularIntervals;
using Intervals.Intervals;
using Intervals.Points;

{
    // [0, 10]
    var result1 = new Interval<int>(0, 10, IntervalInclusion.Closed);
    // (0, 10)
    var result2 = new Interval<int>(0, 10, IntervalInclusion.Opened);
    // (0, 10]
    var result3 = new Interval<int>(0, 10, IntervalInclusion.LeftOpened);
    // [0, 10)
    var result4 = new Interval<int>(0, 10, IntervalInclusion.RightOpened);
    // [0, 10]
    var result5 = new Interval<int>(Point.Included(0), Point.Included(10));
    // (0, 10)
    var result6 = new Interval<int>(Point.Excluded(0), Point.Excluded(10));
    // (0, 10]
    var result7 = new Interval<int>(Point.Excluded(0), Point.Included(10));
    // [0, 10)
    var result8 = new Interval<int>(Point.Included(0), Point.Excluded(10));
}

{
    // [2022-01-01, 2022-01-25)
    var result1 = new[]
        {
            new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 15)),
            new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 1, 25)),
        }
        .Combine();
    // [2022-01-10, 2022-01-15)
    var result2 = new[]
        {
            new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 15)),
            new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 1, 25)),
        }
        .Overlap();
    // [2022-01-01, 2022-01-10), [2022-01-15, 2022-01-25)
    var result3 = new[]
        {
            new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 15)),
            new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 1, 25)),
        }
        .SymmetricDifference();
    // [2022-01-01, 2022-01-31)
    var result4 = new[]
        {
            new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 5)),
            new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 1, 15)),
            new Interval<DateTime>(new DateTime(2022, 1, 20), new DateTime(2022, 1, 25)),
        }
        .Combine(new[]
        {
            new Interval<DateTime>(new DateTime(2022, 1, 5), new DateTime(2022, 1, 10)),
            new Interval<DateTime>(new DateTime(2022, 1, 15), new DateTime(2022, 1, 20)),
            new Interval<DateTime>(new DateTime(2022, 1, 25), new DateTime(2022, 1, 31)),
        });
    // [2022-01-05, 2022-01-10), [2022-01-15, 2022-01-20), [2022-01-25, 2022-01-31)
    var result5 = result4
        .Subtract(new[]
        {
            new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 5)),
            new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 1, 15)),
            new Interval<DateTime>(new DateTime(2022, 1, 20), new DateTime(2022, 1, 25)),
        });
    // [2022-01-15, 2022-01-20)
    var result6 = result5
        .Overlap(new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 1, 25)));
    // [2022-01-01, 2022-01-15), [2022-01-20, 2022-01-31)
    var result7 = result6
        .SymmetricDifference(new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 1, 31)));
}

{
    // true
    var result1 = new Interval<int>(1, 1).IsEmpty();
    // true
    var result2 = new Interval<string>("abc", "abe").IsOverlap(new Interval<string>("abd", "abg"));
    // true
    var result3 = new Interval<string>("abc", "abz").IsInclude(new Interval<string>("abd", "abe"));
}

{
    // [2022-01-10, 2022-08-16)
    var result1 = new Interval<DateTime>(new DateTime(2022, 1, 10, 1, 0, 0), new DateTime(2022, 8, 15, 1, 0, 0))
        .Ceiling(TimeSpan.FromDays(1));
    // [2022-01-01, 2022-09-01)
    var result2 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .CeilingToMonth();
    // [2022-01-01, 2022-10-01)
    var result3 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .CeilingToQuarter();
    // [2022-01-01, 2023-01-01)
    var result4 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .CeilingToHalfYear();
}

{
    // [2022-01-11, 2022-08-15)
    var result1 = new Interval<DateTime>(new DateTime(2022, 1, 10, 1, 0, 0), new DateTime(2022, 8, 15, 1, 0, 0))
        .Floor(TimeSpan.FromDays(1));
    // [2022-02-01, 2022-08-01)
    var result2 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .FloorToMonth();
    // [2022-04-01, 2022-07-01)
    var result3 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .FloorToQuarter();
    // [2022-07-01, 2022-07-01)
    var result4 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .FloorToHalfYear();
}

{
    // [2022-01-10, 2022-08-15)
    var result1 = new Interval<DateTime>(new DateTime(2022, 1, 10, 1, 0, 0), new DateTime(2022, 8, 15, 1, 0, 0))
        .Round(TimeSpan.FromHours(1));
    // [2022-01-01, 2022-08-01)
    var result2 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .RoundToMonth();
    // [2022-01-01, 2022-07-01)
    var result3 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .RoundToQuarter();
    // [2022-01-01, 2022-07-01)
    var result4 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .RoundToHalfYear();
}

{
    // [2022-01-10, 2022-03-27), [2022-03-27, 2022-07-25), [2022-07-25, 2022-08-15)
    var result1 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .Split(TimeSpan.FromDays(120));
    // [2022-01-10, 2022-02-01), [2022-02-01, 2022-06-01), [2022-06-01, 2022-08-15)
    var result2 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .SplitByMonths(4);
    // [2022-01-10, 2022-04-01), [2022-04-01, 2022-07-01), [2022-07-01, 2022-08-15)
    var result3 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .SplitByQuarters(1);
    // [2022-01-10, 2022-07-01), [2022-07-01, 2022-08-15)
    var result4 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
        .SplitByHalfYears(1);
}

{
    // [2022-01-01T01:01:01, 2022-01-01T01:01:02)
    var result1 = new SecondInterval(2022, 1, 1, 1, 1, 1);
    // [2022-01-01T01:01:00, 2022-01-01T01:02:00)
    var result2 = new MinuteInterval(2022, 1, 1, 1, 1);
    // [2022-01-01T01:00:00, 2022-01-01T02:00:00)
    var result3 = new HourInterval(2022, 1, 1, 1);
    // [2022-01-01, 2022-01-02)
    var result4 = new DayInterval(2022, 1, 1);
    // [2022-01-01, 2022-02-01)
    var result5 = new MonthInterval(2022, 1);
    // [2022-01-01, 2022-04-01)
    var result6 = new QuarterInterval(2022, 1);
    // [2022-01-01, 2022-07-01)
    var result7 = new HalfYearInterval(2022, 1);
    // [2022-01-01, 2023-01-01)
    var result8 = new YearInterval(2022);
    // [2022-01-01, 2022-01-02)
    var result9 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2));
    // [2022-01-01, 2022-02-01)
    var result10 = new MonthGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1));
    // [2022-01-01, 2022-01-02), but the step size is 3 days
    var result11 = new PartOfTimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), TimeSpan.FromDays(3));
    // [2022-01-01, 2022-02-01), but the step size is 3 months
    var result12 = new PartOfMonthGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1), 3);
}

{
    // [2021-12-31, 2022-01-02)
    var result1 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2)).ExpandLeft(1);
    // [2022-01-01, 2022-01-03)
    var result2 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2)).ExpandRight(1);
    // [2022-01-02, 2022-01-03)
    var result3 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2)).Move(1);
    // [2021-12-29, 2022-01-02)
    var result4 = new PartOfTimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), TimeSpan.FromDays(3))
        .ExpandLeft(1);
    // [2022-01-01, 2022-01-05)
    var result5 = new PartOfTimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), TimeSpan.FromDays(3))
        .ExpandRight(1);
    // [2022-01-04, 2022-01-05)
    var result6 = new PartOfTimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), TimeSpan.FromDays(3))
        .Move(1);
}