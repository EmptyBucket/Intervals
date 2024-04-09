# Intervals

Library for working with generic intervals and generic granular intervals

Intervals support different inclusions (`Opened`, `LeftOpened`, `RightOpened`, `Closed`)
* `Opened` inclusion is when both of interval endpoints are excluded (math notation as __({LeftValue}, {RightValue})__ )
* `LeftOpened` inclusion is when left endpoint of interval is excluded and right endpoint is included (math notation as __({LeftValue}, {RightValue}]__ )
* `RightOpened` inclusion is when right endpoint of interval is included and right endpoint is excluded (math notation as __[{LeftValue}, {RightValue})__ )
* `Closed` inclusion is when both of interval endpoints are included (math notation as __[{LeftValue}, {RightValue}]__ )

### Documentation:

* [Obsolete] https://github.com/EmptyBucket/Intervals/wiki/

#### Nuget:

* https://www.nuget.org/packages/ap.Intervals/

## Usage

### Interval initialization

If you do not explicitly specify `IntervalInclusion`, then the interval will be `IntervalInclusion.RightOpened`,
i.e. `[x, y)`. `IntervalInclusion.RightOpened` and `IntervalInclusion.LeftOpened` are preferred because
for them the result of operations is always obvious. But when choosing this type of interval,
you must keep in mind that in client code you must use pair of non-strict and strict comparison operators,
e.g. `LeftValue <= {you-variable} && {you-variable} < RightValue` for `IntervalInclusion.RightOpened`

```csharp
// (0, 10)
var result1 = new Interval<int>(0, 10, IntervalInclusion.Opened);
// (0, 10]
var result2 = new Interval<int>(0, 10, IntervalInclusion.LeftOpened);
// [0, 10)
var result3 = new Interval<int>(0, 10, IntervalInclusion.RightOpened);
// [0, 10]
var result4 = new Interval<int>(0, 10, IntervalInclusion.Closed);
```

### Alternative way interval initialization
```csharp
// (0, 10)
var result1 = new Interval<int>(Point.Excluded(0), Point.Excluded(10));
// (0, 10]
var result2 = new Interval<int>(Point.Excluded(0), Point.Included(10));
// [0, 10)
var result3 = new Interval<int>(Point.Included(0), Point.Excluded(10));
// [0, 10]
var result4 = new Interval<int>(Point.Included(0), Point.Included(10));
```

### Multi-interval operations

Operations have `O(nlog)` asymptotic complexity, even if you did some complex method chaining it would still be `O(nlog)`
where each point would only be sorted once

| Combine | Overlap | Substract | SymmetricDifference |
| --- | --- | --- | --- |
| ![image](https://user-images.githubusercontent.com/8377311/170842990-f7fa9a86-93cb-4904-b0c1-d44e6402b9e8.png) | ![image](https://user-images.githubusercontent.com/8377311/170842996-4eeb830e-cb43-4403-9d0e-f3f6935c030c.png) | ![image](https://user-images.githubusercontent.com/8377311/170843001-518e926a-ff64-46cb-b88e-a12436ef43b0.png) | ![image](https://user-images.githubusercontent.com/8377311/170843011-a271a586-d46a-4dba-8648-40b91332d630.png) |

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

### `Is` interval operations

Any incorrect interval is considered empty, e.g. [2, 1], (1, 1), [1, 1), ...

You can use intervals with any `IComparable<T>` type with interesting effects, look at string in the following example

```csharp
// true
var result1 = new Interval<int>(1, 1).IsEmpty();
// true
var result2 = new Interval<string>("abc", "abe").IsOverlap(new Interval<string>("abd", "abg"));
// true
var result3 = new Interval<string>("abc", "abz").IsInclude(new Interval<string>("abd", "abe"));
```

### Granular interval initialization
I mentioned earlier that operations are always obvious only for `IntervalInclusion.RightOpened` or
`IntervalInclusion.LeftOpened`, so you probably have a question: what operations are not obvious for
`IntervalInclusion.Closed` and `IntervalInclusion.Opened`?

Let's look at an example: what is the length of the interval [2022/01/01, 2022/01/10]?
The correct answer is 10 days or 10 days 23 hours 59 minutes or 10 days 23 hours 59 minutes ... to max precision?
The correct answer will depend on the context. A similar problem occurs not only with the operation of getting the length,
but also with other operations, for example, think about how `Split` should work for closed interval? Therefore,
a new term granular interval appears, essentially this is the basis from which the interval consists, e.g.
```csharp
new TimeGranularInterval(new DateTime(2022, 1, 1, 1, 0, 0), new DateTime(2022, 1, 10, 1, 0, 0), TimeSpan.FromDays(1), IntervalInclusion.Closed)
```
This is an interval that consists of 10 granules of 1 day, but all granules are shifted 1 hour forward

You should keep in mind that each month has a different number of days, so intervals derived from a month also have a different lengths.
Therefore, another terms monthly intervals arises. In this type of intervals, operations are calculated based on calendar months, e.g.
```csharp
new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 2, 28), TimeSpan.FromDays(1), IntervalInclusion.Closed)
```
This is an interval that consists of 31 + 28 granules of 1 day for 2 months

More examples:
```csharp
// [2022-01-01T01:01:01, 2022-01-01T01:01:02)
var result1 = new SecondInterval(new DateTime(2022, 1, 1, 1, 1, 1));
// [2022-01-01T01:01:00, 2022-01-01T01:02:00)
var result2 = new MinutelyInterval(new DateTime(2022, 1, 1, 1, 1, 0));
// [2022-01-01T01:00:00, 2022-01-01T02:00:00)
var result3 = new HourlyInterval(new DateTime(2022, 1, 1, 1, 0, 0));
// [2022-01-01, 2022-01-02)
var result4 = new DailyInterval(new DateTime(2022, 1, 1));
// [2022-01-01, 2022-01-02)
var result5 = new WeeklyInterval(new DateTime(2022, 1, 1));
// [2022-01-01, 2022-02-01)
var result6 = new MonthlyInterval(2022, 1, TimeSpan.FromDays(1), 1);
// [2022-01-01, 2022-04-01)
var result7 = new QuarterlyInterval(2022, 1, TimeSpan.FromDays(1), 1);
// [2022-01-01, 2022-07-01)
var result8 = new HalfYearlyInterval(2022, 1, TimeSpan.FromDays(1), 1);
// [2022-01-01, 2023-01-01)
var result9 = new YearlyInterval(2022, TimeSpan.FromDays(1), 1);
// [2022-01-01, 2022-01-04) with custom granule length
var result12 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 3), TimeSpan.FromDays(2));
// [2022-01-01, 2022-02-01) with custom granule length
var result13 = new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1), TimeSpan.FromSeconds(2));
```

### Granular interval operations

```csharp
// [2021-12-31, 2022-01-02)
var result1 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), TimeSpan.FromDays(1))
    .MoveByGranule(-1, 0);
// [2022-01-01, 2022-01-03)
var result2 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), TimeSpan.FromDays(1))
    .MoveByGranule(0, 1);
// [2022-01-02, 2022-01-03)
var result3 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), TimeSpan.FromDays(1))
    .MoveByGranule(1);
// [2021-12-30, 2022-01-03)
var result4 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 3), TimeSpan.FromDays(2))
    .MoveByLength(-1, 0);
// [2022-01-01, 2022-01-05)
var result5 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 3), TimeSpan.FromDays(2))
    .MoveByLength(0, 1);
// [2022-01-03, 2022-01-05)
var result6 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 3), TimeSpan.FromDays(2))
    .MoveByLength(1);
// [2021-12-31, 2022-02-01)
var result7 = new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1), TimeSpan.FromDays(1))
    .MoveByGranule(-1, 0);
// [2022-01-01, 2022-02-02)
var result8 = new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1), TimeSpan.FromDays(1))
    .MoveByGranule(0, 1);
// [2022-01-02, 2022-02-02)
var result9 = new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 2, 1), TimeSpan.FromDays(1))
    .MoveByGranule(1);
// [2021-11-01, 2022-03-01)
var result10 = new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 3, 1), TimeSpan.FromDays(1))
    .MoveByLength(-1, 0);
// [2022-01-01, 2022-05-01)
var result11 = new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 3, 1), TimeSpan.FromDays(1))
    .MoveByLength(0, 1);
// [2022-03-01, 2022-05-01)
var result12 = new MonthlyInterval(new DateTime(2022, 1, 1), new DateTime(2022, 3, 1), TimeSpan.FromDays(1))
    .MoveByLength(1);
// 10
var result13 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 12), TimeSpan.FromDays(1),
        IntervalInclusion.Opened)
    .Length;
// 10
var result14 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 11), TimeSpan.FromDays(1),
        IntervalInclusion.LeftOpened)
    .Length;
// 10
var result15 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 11), TimeSpan.FromDays(1),
        IntervalInclusion.RightOpened)
    .Length;
// 10
var result16 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 10), TimeSpan.FromDays(1),
        IntervalInclusion.Closed)
    .Length;
// (2021-12-31, 2022-01-11)
var result17 = new TimeGranularInterval(new DateTime(2022, 1, 1), new DateTime(2022, 1, 10), TimeSpan.FromDays(1),
        IntervalInclusion.Closed)
    .Convert(IntervalInclusion.Opened);
```

### `Split` interval operations

`Split` can return incomplete chunks when there is not enough space for the next chunk, e.g.
```csharp
new Interval<DateTime>(new DateTime(2022, 1, 1), new DateTime(2022, 2, 15)).SplitByMonths(TimeSpan.FromDays(1), 1)
```
will return `[2022-01-01, 2022-02-01), [2022-02-01, 2022-02-15)`

More examples:
```csharp
// (2022-01-10, 2022-01-20), [2022-01-20, 2022-01-30), [2022-01-30, 2022-02-01)
var result1 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 2, 1), IntervalInclusion.Opened)
    .Split(TimeSpan.FromDays(10));
// [2022-01-10, 2022-01-19T23:59:59], [2022-01-20, 2022-01-29T23:59:59], [2022-01-30, 2022-02-01]
var result2 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 2, 1), IntervalInclusion.Closed)
    .Split(TimeSpan.FromSeconds(1), 10 * 24 * 60 * 60);
// (2022-01-10, 2022-05-01), (2022-04-30, 2022-08-15)
var result3 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15), IntervalInclusion.Opened)
    .SplitByMonths(TimeSpan.FromDays(1), 4);
// [2022-01-10, 2022-03-31], [2022-04-01, 2022-06-30], [2022-07-01, 2022-08-15]
var result4 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15), IntervalInclusion.Closed)
    .SplitByQuarters(TimeSpan.FromDays(1), 1);
// [2022-01-10, 2022-07-01), [2022-07-01, 2022-08-15)
var result5 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
    .SplitByHalfYears(TimeSpan.FromDays(1), 1);
// [2022-01-10, 2022-08-15)
var result6 = new Interval<DateTime>(new DateTime(2022, 1, 10), new DateTime(2022, 8, 15))
    .SplitByYears(TimeSpan.FromDays(1), 1);
```

### Helper methods that you might find useful

* https://github.com/EmptyBucket/Intervals/wiki/intervals.utils.datetimeextensions
* https://github.com/EmptyBucket/Intervals/wiki/intervals.points.pointextensions
* https://github.com/EmptyBucket/Intervals/wiki/intervals.points.inclusionextensions
* https://github.com/EmptyBucket/Intervals/wiki/intervals.points.endpointlocationextensions
