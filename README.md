# Intervals

Library for working with generic intervals and granular time intervals (like a quarter, a week...). Intervals support different inclusions (Opened, LeftOpened, RightOpened, Closed)

### Documentation:

* https://github.com/EmptyBucket/Intervals/wiki/

#### Nuget:

* https://www.nuget.org/packages/ap.Intervals/

## Usage

### Interval initialization

If you do not explicitly specify inclusion, then the interval will be `IntervalInclusion.RightOpened`, i.e. `[x, y)`. `IntervalInclusion.RightOpened` and `IntervalInclusion.LeftOpened` are preferred because for them the result of operations is always obvious. But when choosing this type of interval, you must keep in mind that in client code you must use pair of non-strict and strict inequality, e.g. `x <= {you-variable} && {you-variable} < y` for `IntervalInclusion.RightOpened`

```csharp
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
```

### Multi-interval operations

Operations have `O(nlog)` asymptotic complexity, even if you did some complex method chaining it would still be `O(nlog)`
where each point would only be sorted once

```csharp
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
```

| Combine | Overlap | Substract | SymmetricDifference |
| --- | --- | --- | --- |
| ![image](https://user-images.githubusercontent.com/8377311/170842990-f7fa9a86-93cb-4904-b0c1-d44e6402b9e8.png) | ![image](https://user-images.githubusercontent.com/8377311/170842996-4eeb830e-cb43-4403-9d0e-f3f6935c030c.png) | ![image](https://user-images.githubusercontent.com/8377311/170843001-518e926a-ff64-46cb-b88e-a12436ef43b0.png) | ![image](https://user-images.githubusercontent.com/8377311/170843011-a271a586-d46a-4dba-8648-40b91332d630.png) |

### Is interval operations

```csharp
// true
var result1 = new Interval<int>(1, 1).IsEmpty();
// true
var result2 = new Interval<string>("abc", "abe").IsOverlap(new Interval<string>("abd", "abg"));
// true
var result3 = new Interval<string>("abc", "abz").IsInclude(new Interval<string>("abd", "abe"));
```

### Granular interval initialization

You should keep in mind that each month has a different number of days, so intervals derived from a month also have a different lengths. Therefore, later on you will see special method overloads for working with intervals derived from the month, which usually use `IntervalInclusion.RightOpened`, e.g. `Move [2022-01-01, 2022-02-01) -> [2022-02-01, 2022 -03-01)` as you can see it is always deterministic

```csharp
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
```

### Granular interval operations

```csharp
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
```

### Ceiling interval operations

```csharp
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
```

### Floor interval operations

```csharp
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
```

### Round interval operations

```csharp
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
```

### Split interval operations

`Split` can return incomplete chunks when there is not enough space for the next chunk, e.g. `new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 2, 15)).SplitByMonths(1)` will return `[2022-01-01, 2022-02-01), [2022-02-01, 2022-02-15)`. If you want whole chunks you can use the `Floor`, `Ceiling`, `Round` operations to align interval and then use `Split`

```csharp
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
```

### Helper methods that you might find useful

https://github.com/EmptyBucket/Intervals/wiki/intervals.utils.datetimeextensions
https://github.com/EmptyBucket/Intervals/wiki/intervals.points.pointextensions
https://github.com/EmptyBucket/Intervals/wiki/intervals.points.inclusionextensions
https://github.com/EmptyBucket/Intervals/wiki/intervals.points.endpointlocationextensions
