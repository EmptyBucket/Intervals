[`< Back`](./)

---

# IntervalInclusionConvert

Namespace: Intervals.Intervals

```csharp
public static class IntervalInclusionConvert
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IntervalInclusionConvert](intervals.intervals.intervalinclusionconvert)

## Methods

### **FromInclusions(Inclusion, Inclusion)**

Returns the converted inclusion of the interval from the  and  of the point

```csharp
public static IntervalInclusion FromInclusions(Inclusion leftInclusion, Inclusion rightInclusion)
```

#### Parameters

`leftInclusion` [Inclusion](intervals.points.inclusion)<br>

`rightInclusion` [Inclusion](intervals.points.inclusion)<br>

#### Returns

[IntervalInclusion](intervals.intervals.intervalinclusion)<br>

### **ToInclusions(IntervalInclusion)**

Returns the converted inclusion of the point from the  of the interval

```csharp
public static ValueTuple<Inclusion, Inclusion> ToInclusions(IntervalInclusion intervalInclusion)
```

#### Parameters

`intervalInclusion` [IntervalInclusion](intervals.intervals.intervalinclusion)<br>

#### Returns

[ValueTuple&lt;Inclusion, Inclusion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.valuetuple-2)<br>

---

[`< Back`](./)
