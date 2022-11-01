[`< Back`](./)

---

# IInterval&lt;T&gt;

Namespace: Intervals.Intervals

Represents the interface of an interval instance

```csharp
public interface IInterval<T> : , , , System.Collections.IEnumerable
```

#### Type Parameters

`T`<br>

Implements IComparable&lt;IInterval&lt;T&gt;&gt;, IEquatable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **Left**

Left endpoint of the interval

```csharp
public abstract Endpoint<T> Left { get; }
```

#### Property Value

Endpoint&lt;T&gt;<br>

### **Right**

Right endpoint of the interval

```csharp
public abstract Endpoint<T> Right { get; }
```

#### Property Value

Endpoint&lt;T&gt;<br>

### **Inclusion**

Inclusion of the interval

```csharp
public abstract IntervalInclusion Inclusion { get; }
```

#### Property Value

[IntervalInclusion](intervals.intervals.intervalinclusion)<br>

## Methods

### **IsEmpty()**

Returns true if interval is empty, otherwise returns false

```csharp
bool IsEmpty()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

---

[`< Back`](./)
