# IGranularInterval&lt;T&gt;

Namespace: Intervals.GranularIntervals

Represents an granular interval instance

```csharp
public interface IGranularInterval<T> : , , , , System.Collections.IEnumerable
```

#### Type Parameters

`T`<br>

Implements IInterval&lt;T&gt;, IComparable&lt;IInterval&lt;T&gt;&gt;, IEquatable&lt;IInterval&lt;T&gt;&gt;, IEnumerable&lt;IInterval&lt;T&gt;&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Methods

### **Move(Int32)**

Returns a new interval moved by the specified .
 If the  is positive, then move to the right, if negative, then move to the left

```csharp
IGranularInterval<T> Move(int granulesCount)
```

#### Parameters

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[IGranularInterval&lt;T&gt;](./intervals.granularintervals.igranularinterval-1.md)<br>

### **ExpandLeft(Int32)**

Returns a new interval expanded by the specified  to the right

```csharp
IGranularInterval<T> ExpandLeft(int granulesCount)
```

#### Parameters

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[IGranularInterval&lt;T&gt;](./intervals.granularintervals.igranularinterval-1.md)<br>

### **ExpandRight(Int32)**

Returns a new interval expanded by the specified  to the left

```csharp
IGranularInterval<T> ExpandRight(int granulesCount)
```

#### Parameters

`granulesCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[IGranularInterval&lt;T&gt;](./intervals.granularintervals.igranularinterval-1.md)<br>
