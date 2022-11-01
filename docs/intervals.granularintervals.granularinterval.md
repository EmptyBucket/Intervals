[`< Back`](./)

---

# GranularInterval

Namespace: Intervals.GranularIntervals

Represents an granular interval instance where the granule size is determined by the length of the interval

```csharp
public class GranularInterval : Intervals.Intervals.Interval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.IComparable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.IEquatable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.Generic.IEnumerable`1[[Intervals.Intervals.IInterval`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Intervals, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable, IGranularInterval`1
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Interval&lt;DateTime&gt;](intervals.intervals.interval-1) → [GranularInterval](intervals.granularintervals.granularinterval)<br>
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

### **GranularInterval(Point&lt;DateTime&gt;, Point&lt;DateTime&gt;)**

```csharp
public GranularInterval(Point<DateTime> leftPoint, Point<DateTime> rightPoint)
```

#### Parameters

`leftPoint` [Point&lt;DateTime&gt;](intervals.points.point-1)<br>

`rightPoint` [Point&lt;DateTime&gt;](intervals.points.point-1)<br>

### **GranularInterval(DateTime, DateTime, IntervalInclusion)**

```csharp
public GranularInterval(DateTime leftValue, DateTime rightValue, IntervalInclusion inclusion)
```

#### Parameters

`leftValue` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`rightValue` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`inclusion` [IntervalInclusion](intervals.intervals.intervalinclusion)<br>

## Methods

### **Move(Int32)**

Returns a new interval moved by the specified .
 If the  is positive, then move to the right, if negative, then move to the left

```csharp
public IGranularInterval<DateTime> Move(int granulesCount)
```

#### Parameters

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **ExpandLeft(Int32)**

Returns a new interval expanded by the specified  to the right

```csharp
public IGranularInterval<DateTime> ExpandLeft(int granulesCount)
```

#### Parameters

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **ExpandRight(Int32)**

Returns a new interval expanded by the specified  to the left

```csharp
public IGranularInterval<DateTime> ExpandRight(int granulesCount)
```

#### Parameters

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

---

[`< Back`](./)
