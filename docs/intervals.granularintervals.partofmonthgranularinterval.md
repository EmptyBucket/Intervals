[`< Back`](./)

---

# PartOfMonthGranularInterval

Namespace: Intervals.GranularIntervals

Represents an part of the granular interval instance where the granule size is determined by the number of months and explicitly specified.
 That is, the granule size on the basis of which operations are executed can be larger or smaller than the interval itself

```csharp
public class PartOfMonthGranularInterval : MonthGranularInterval, Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.IComparable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.IEquatable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.Generic.IEnumerable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable, IGranularInterval`1
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Interval&lt;DateTime&gt;](intervals.intervals.interval-1) → [MonthGranularInterval](intervals.granularintervals.monthgranularinterval) → [PartOfMonthGranularInterval](intervals.granularintervals.partofmonthgranularinterval)<br>
Implements [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1), [IComparable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable-1), [IEquatable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1), [IEnumerable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable), [IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)

## Properties

### **Left**

Left endpoint of the interval

```csharp
public Endpoint<DateTime> Left { get; }
```

#### Property Value

[Endpoint&lt;DateTime&gt;](intervals.points.endpoint-1)<br>

### **Right**

Right endpoint of the interval

```csharp
public Endpoint<DateTime> Right { get; }
```

#### Property Value

[Endpoint&lt;DateTime&gt;](intervals.points.endpoint-1)<br>

### **Inclusion**

Inclusion of the interval

```csharp
public IntervalInclusion Inclusion { get; }
```

#### Property Value

[IntervalInclusion](intervals.intervals.intervalinclusion)<br>

## Constructors

### **PartOfMonthGranularInterval(Point&lt;DateTime&gt;, Point&lt;DateTime&gt;, Int32)**

```csharp
public PartOfMonthGranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint, int granulesCount)
```

#### Parameters

`leftPoint` [Point&lt;DateTime&gt;](intervals.points.point-1)<br>

`rightPoint` [Point&lt;DateTime&gt;](intervals.points.point-1)<br>

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PartOfMonthGranularInterval(DateTime, DateTime, Int32, IntervalInclusion)**

```csharp
public PartOfMonthGranularInterval(DateTime leftValue, DateTime rightValue, int granulesCount, IntervalInclusion inclusion)
```

#### Parameters

`leftValue` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`rightValue` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`inclusion` [IntervalInclusion](intervals.intervals.intervalinclusion)<br>

---

[`< Back`](./)
