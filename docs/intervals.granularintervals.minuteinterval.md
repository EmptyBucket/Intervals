# MinuteInterval

Namespace: Intervals.GranularIntervals

Represents an minute interval instance where the granule size is equal to a minute

```csharp
public class MinuteInterval : GranularInterval, Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.IComparable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.IEquatable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.Generic.IEnumerable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable, IGranularInterval`1
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Interval&lt;DateTime&gt;](./intervals.intervals.interval-1.md) → [GranularInterval](./intervals.granularintervals.granularinterval.md) → [MinuteInterval](./intervals.granularintervals.minuteinterval.md)<br>
Implements [IInterval&lt;DateTime&gt;](./intervals.intervals.iinterval-1.md), [IComparable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable-1), [IEquatable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1), [IEnumerable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable), [IGranularInterval&lt;DateTime&gt;](./intervals.granularintervals.igranularinterval-1.md)

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

### **Day**

```csharp
public int Day { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Hour**

```csharp
public int Hour { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Minute**

```csharp
public int Minute { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Left**

Left endpoint of the interval

```csharp
public Endpoint<DateTime> Left { get; }
```

#### Property Value

[Endpoint&lt;DateTime&gt;](./intervals.points.endpoint-1.md)<br>

### **Right**

Right endpoint of the interval

```csharp
public Endpoint<DateTime> Right { get; }
```

#### Property Value

[Endpoint&lt;DateTime&gt;](./intervals.points.endpoint-1.md)<br>

### **Inclusion**

Inclusion of the interval

```csharp
public IntervalInclusion Inclusion { get; }
```

#### Property Value

[IntervalInclusion](./intervals.intervals.intervalinclusion.md)<br>

## Constructors

### **MinuteInterval(Int32, Int32, Int32, Int32, Int32, DateTimeKind)**

```csharp
public MinuteInterval(int year, int month, int day, int hour, int minute, DateTimeKind kind)
```

#### Parameters

`year` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`month` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`day` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`hour` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`minute` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`kind` [DateTimeKind](https://docs.microsoft.com/en-us/dotnet/api/system.datetimekind)<br>
