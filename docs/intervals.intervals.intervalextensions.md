[`< Back`](./)

---

# IntervalExtensions

Namespace: Intervals.Intervals

```csharp
public static class IntervalExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IntervalExtensions](intervals.intervals.intervalextensions)

## Methods

### **Ceiling(IInterval&lt;DateTime&gt;, TimeSpan)**

Returns the smallest interval aligned to  that is greater than or equal to the specified

```csharp
public static IInterval<DateTime> Ceiling(IInterval<DateTime> interval, TimeSpan granuleSize)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`granuleSize` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

#### Returns

[IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

### **CeilingToMonth(IInterval&lt;DateTime&gt;)**

Returns the smallest interval aligned to month that is greater than or equal to the specified

```csharp
public static IInterval<DateTime> CeilingToMonth(IInterval<DateTime> interval)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

#### Returns

[IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

### **CeilingToQuarter(IInterval&lt;DateTime&gt;)**

Returns the smallest interval aligned to quarter that is greater than or equal to the specified

```csharp
public static IInterval<DateTime> CeilingToQuarter(IInterval<DateTime> interval)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

#### Returns

[IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

### **CeilingToHalfYear(IInterval&lt;DateTime&gt;)**

Returns the smallest interval aligned to half-year that is greater than or equal to the specified

```csharp
public static IInterval<DateTime> CeilingToHalfYear(IInterval<DateTime> interval)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

#### Returns

[IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

### **IsOverlap&lt;T&gt;(IEnumerable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;)**

Returns true if the specified  and  intervals intersect, otherwise returns false

```csharp
public static bool IsOverlap<T>(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
```

#### Type Parameters

`T`<br>

#### Parameters

`left` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

`right` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsInclude&lt;T&gt;(IEnumerable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;)**

Returns true if the specified  intervals include  intervals, otherwise returns false

```csharp
public static bool IsInclude<T>(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
```

#### Type Parameters

`T`<br>

#### Parameters

`left` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

`right` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Combine&lt;T&gt;(IEnumerable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;)**

Returns the union of the specified  and  intervals

```csharp
public static IEnumerable<IInterval<T>> Combine<T>(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
```

#### Type Parameters

`T`<br>

#### Parameters

`left` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

`right` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

#### Returns

IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

### **Overlap&lt;T&gt;(IEnumerable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;)**

Returns the intersection of the specified  and  intervals

```csharp
public static IEnumerable<IInterval<T>> Overlap<T>(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
```

#### Type Parameters

`T`<br>

#### Parameters

`left` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

`right` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

#### Returns

IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

### **Subtract&lt;T&gt;(IEnumerable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;)**

Returns the difference between the specified  and  intervals

```csharp
public static IEnumerable<IInterval<T>> Subtract<T>(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
```

#### Type Parameters

`T`<br>

#### Parameters

`left` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

`right` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

#### Returns

IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

### **SymmetricDifference&lt;T&gt;(IEnumerable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;)**

Returns the symmetric difference between the specified  and  intervals

```csharp
public static IEnumerable<IInterval<T>> SymmetricDifference<T>(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
```

#### Type Parameters

`T`<br>

#### Parameters

`left` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

`right` IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

#### Returns

IEnumerable&lt;IInterval&lt;T&gt;&gt;<br>

### **Floor(IInterval&lt;DateTime&gt;, TimeSpan)**

Returns the largest interval aligned to  that is less than or equal to the specified

```csharp
public static IGranularInterval<DateTime> Floor(IInterval<DateTime> interval, TimeSpan granuleSize)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`granuleSize` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **FloorToMonth(IInterval&lt;DateTime&gt;)**

Returns the largest interval aligned to month that is less than or equal to the specified

```csharp
public static IGranularInterval<DateTime> FloorToMonth(IInterval<DateTime> interval)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **FloorToQuarter(IInterval&lt;DateTime&gt;)**

Returns the largest interval aligned to quarter that is less than or equal to the specified

```csharp
public static IGranularInterval<DateTime> FloorToQuarter(IInterval<DateTime> interval)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **FloorToHalfYear(IInterval&lt;DateTime&gt;)**

Returns the largest interval aligned to half-year that is less than or equal to the specified

```csharp
public static IGranularInterval<DateTime> FloorToHalfYear(IInterval<DateTime> interval)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **Round(IInterval&lt;DateTime&gt;, TimeSpan, MidpointRounding)**

Rounds a  to a interval aligned to , and uses the specified rounding convention for midpoint values.

```csharp
public static IGranularInterval<DateTime> Round(IInterval<DateTime> interval, TimeSpan granuleSize, MidpointRounding midpointRounding)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`granuleSize` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **RoundToMonth(IInterval&lt;DateTime&gt;, MidpointRounding)**

Rounds a  to a interval aligned to month, and uses the specified rounding convention for midpoint values.

```csharp
public static IGranularInterval<DateTime> RoundToMonth(IInterval<DateTime> interval, MidpointRounding midpointRounding)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **RoundToQuarter(IInterval&lt;DateTime&gt;, MidpointRounding)**

Rounds a  to a interval aligned to quarter, and uses the specified rounding convention for midpoint values.

```csharp
public static IGranularInterval<DateTime> RoundToQuarter(IInterval<DateTime> interval, MidpointRounding midpointRounding)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **RoundToHalfYear(IInterval&lt;DateTime&gt;, MidpointRounding)**

Rounds a  to a interval aligned to half-year, and uses the specified rounding convention for midpoint values.

```csharp
public static IGranularInterval<DateTime> RoundToHalfYear(IInterval<DateTime> interval, MidpointRounding midpointRounding)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[IGranularInterval&lt;DateTime&gt;](intervals.granularintervals.igranularinterval-1)<br>

### **Split(IInterval&lt;DateTime&gt;, TimeSpan, Boolean)**

Splits the interval into sub-intervals of  length and
 the rest, which was less than  in length,
 if  is false, otherwise removes it

```csharp
public static IEnumerable<IInterval<DateTime>> Split(IInterval<DateTime> interval, TimeSpan granuleSize, bool removeIncomplete)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`granuleSize` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

`removeIncomplete` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[IEnumerable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **SplitByMonths(IInterval&lt;DateTime&gt;, Int32, Boolean)**

Splits the interval into sub-intervals of  length and
 the rest, which was less than  in length,
 if  is false, otherwise removes it

```csharp
public static IEnumerable<IInterval<DateTime>> SplitByMonths(IInterval<DateTime> interval, int monthsCount, bool removeIncomplete)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`monthsCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`removeIncomplete` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[IEnumerable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **SplitByQuarters(IInterval&lt;DateTime&gt;, Int32, Boolean)**

Splits the interval into sub-intervals of  length and
 the rest, which was less than  in length,
 if  is false, otherwise removes it

```csharp
public static IEnumerable<IInterval<DateTime>> SplitByQuarters(IInterval<DateTime> interval, int quartersCount, bool removeIncomplete)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`quartersCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`removeIncomplete` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[IEnumerable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **SplitByHalfYears(IInterval&lt;DateTime&gt;, Int32, Boolean)**

Splits the interval into sub-intervals of  length and
 the rest, which was less than  in length,
 if  is false, otherwise removes it

```csharp
public static IEnumerable<IInterval<DateTime>> SplitByHalfYears(IInterval<DateTime> interval, int halfYearsCount, bool removeIncomplete)
```

#### Parameters

`interval` [IInterval&lt;DateTime&gt;](intervals.intervals.iinterval-1)<br>

`halfYearsCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`removeIncomplete` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[IEnumerable&lt;IInterval&lt;DateTime&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

---

[`< Back`](./)
