# Intervals
Library for working with generic intervals and granular time intervals (like a quarter, a week...). Intervals support different inclusions (Opened, LeftOpened, RightOpened, Closed). Operations have O(nlog) asymptotic complexity, even if you did some complex method chaining it would still be O(nlog) where each point would only be sorted once
### Documentation: 
* https://github.com/EmptyBucket/Intervals/blob/master/readme/index.md
#### Nuget:
* https://www.nuget.org/packages/ap.Intervals/
## Usage
```csharp
var enumerable = new[]
    {
        new Interval<int>(0, 10, IntervalInclusion.LeftOpened),
        new Interval<int>(20, 30, IntervalInclusion.Closed),
        new Interval<int>(40, 50, IntervalInclusion.RightOpened)
    }
    .Combine(new[]
        {
            new Interval<int>(10, 20, IntervalInclusion.Opened),
            new Interval<int>(30, 40, IntervalInclusion.Opened)
        }
        .Subtract(new[]
        {
            new Interval<int>(15, 25, IntervalInclusion.Closed),
            new Interval<int>(25, 35, IntervalInclusion.Closed)
        }))
    .Overlap(new[]
        {
            new Interval<int>(15, 20, IntervalInclusion.Closed),
            new Interval<int>(30, 35, IntervalInclusion.Closed)
        }
        .SymmetricDifference(new Interval<int>(20, 35, IntervalInclusion.Opened)))
    .Overlap(new Interval<int>(20, 25, IntervalInclusion.Opened))
    .ToArray();

var result = new QuarterInterval(2022, 3)
    .Move(-1)
    .Combine(new MonthInterval(2022, 1).Move(1))
    .SelectMany(i => i.SplitByMonths(2));
```
#### Combine
![image](https://user-images.githubusercontent.com/8377311/170842990-f7fa9a86-93cb-4904-b0c1-d44e6402b9e8.png)
#### Overlap
![image](https://user-images.githubusercontent.com/8377311/170842996-4eeb830e-cb43-4403-9d0e-f3f6935c030c.png)
#### Subtract
![image](https://user-images.githubusercontent.com/8377311/170843001-518e926a-ff64-46cb-b88e-a12436ef43b0.png)
#### SymmetricDifference
![image](https://user-images.githubusercontent.com/8377311/170843011-a271a586-d46a-4dba-8648-40b91332d630.png)

