[`< Back`](./)

---

# MonthInterval

Namespace: Intervals.GranularIntervals

Represents an month interval instance where the granule size is equal to a month

```csharp
public class MonthInterval : MonthGranularInterval, Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.IComparable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.IEquatable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.Generic.IEnumerable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable, IGranularInterval`1
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Interval&lt;DateTime&gt;](intervals.intervals.interval-1) → [MonthGranularInterval](intervals.granularintervals.monthgranularinterval) → [MonthInterval](intervals.granularintervals.monthinterval)<br>
Implements [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1), [IComparable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable-1), [IEquatable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1), [IEnumerable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable), [IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)

## Properties

### **Year**

```csharp
public int Year { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Month**

```csharp
public int Month { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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

### **MonthInterval(Int32, Int32, DateTimeKind)**

```csharp
public MonthInterval(int year, int month, DateTimeKind kind)
```

#### Parameters

`year` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`month` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`kind` [DateTimeKind](https://docs.microsoft.com/en-us/dotnet/api/system.datetimekind)<br>

---

[`< Back`](./)
