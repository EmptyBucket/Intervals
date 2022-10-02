# IntervalInclusionConvert

Namespace: Intervals.Intervals

```csharp
public static class IntervalInclusionConvert
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IntervalInclusionConvert](./intervals.intervals.intervalinclusionconvert.md)

## Methods

### **FromInclusions(Inclusion, Inclusion)**

Returns the converted inclusion of the interval from the  and  of the point

```csharp
public static IntervalInclusion FromInclusions(Inclusion leftInclusion, Inclusion rightInclusion)
```

#### Parameters

`leftInclusion` [Inclusion](./intervals.points.inclusion.md)<br>

`rightInclusion` [Inclusion](./intervals.points.inclusion.md)<br>

#### Returns

[IntervalInclusion](./intervals.intervals.intervalinclusion.md)<br>

### **ToInclusions(IntervalInclusion)**

Returns the converted inclusion of the point from the  of the interval

```csharp
public static ValueTuple<Inclusion, Inclusion> ToInclusions(IntervalInclusion intervalInclusion)
```

#### Parameters

`intervalInclusion` [IntervalInclusion](./intervals.intervals.intervalinclusion.md)<br>

#### Returns

[ValueTuple&lt;Inclusion, Inclusion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.valuetuple-2)<br>
