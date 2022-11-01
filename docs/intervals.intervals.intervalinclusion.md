[`< Back`](./)

---

# IntervalInclusion

Namespace: Intervals.Intervals

Represents the inclusion of the interval

```csharp
public enum IntervalInclusion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [IntervalInclusion](intervals.intervals.intervalinclusion)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Opened | 0 | Both endpoints of the interval are excluded |
| LeftOpened | 1 | Left endpoint is excluded and right endpoint of the interval is included |
| RightOpened | 2 | Left endpoint is included and right endpoint of the interval is excluded |
| Closed | 3 | Both endpoints of the interval are included |

---

[`< Back`](./)
