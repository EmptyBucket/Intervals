[`< Back`](./)

---

# Endpoint&lt;T&gt;

Namespace: Intervals.Points

Represents an endpoint instance

```csharp
public struct Endpoint<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Endpoint&lt;T&gt;](intervals.points.endpoint-1)<br>
Implements IComparable&lt;Endpoint&lt;T&gt;&gt;, IEquatable&lt;Endpoint&lt;T&gt;&gt;

## Properties

### **Value**

```csharp
public T Value { get; set; }
```

#### Property Value

T<br>

### **Inclusion**

```csharp
public Inclusion Inclusion { get; set; }
```

#### Property Value

[Inclusion](intervals.points.inclusion)<br>

### **Location**

```csharp
public EndpointLocation Location { get; set; }
```

#### Property Value

[EndpointLocation](intervals.points.endpointlocation)<br>

## Constructors

### **Endpoint(T, Inclusion, EndpointLocation)**

Represents an endpoint instance

```csharp
Endpoint(T Value, Inclusion Inclusion, EndpointLocation Location)
```

#### Parameters

`Value` T<br>

`Inclusion` [Inclusion](intervals.points.inclusion)<br>

`Location` [EndpointLocation](intervals.points.endpointlocation)<br>

### **Endpoint(Point&lt;T&gt;, EndpointLocation)**

Returns endpoint with specified  and

```csharp
Endpoint(Point<T> point, EndpointLocation location)
```

#### Parameters

`point` Point&lt;T&gt;<br>

`location` [EndpointLocation](intervals.points.endpointlocation)<br>

## Methods

### **Deconstruct(Point`1&, EndpointLocation&)**

```csharp
void Deconstruct(Point`1& point, EndpointLocation& location)
```

#### Parameters

`point` Point`1&<br>

`location` [EndpointLocation&](intervals.points.endpointlocation&)<br>

### **CompareTo(Endpoint&lt;T&gt;)**

Compares this instance to a specified  and returns an indication of their relative values
 First, instances are compared according to their values, then according to the rule:
 ) less than (, ] less than(, ) less thank [, ] less than [, ) equal ), ( equal (, ] equal ], [ equal [, ) less than ], ( greater than [

```csharp
int CompareTo(Endpoint<T> other)
```

#### Parameters

`other` [Endpoint&lt;T&gt;](intervals.points.endpoint-1)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
 Return Value Description Less than zero This instance is less than . Zero This instance is equal to . Greater than zero This instance is greater than .

### **ToString()**

Converts the value of this instance to "{[(,),[,]]}{Value}{[(,),[,]]}" format

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>

### **GetHashCode()**

```csharp
int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Equals(Object)**

```csharp
bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Equals(Endpoint&lt;T&gt;)**

```csharp
bool Equals(Endpoint<T> other)
```

#### Parameters

`other` [Endpoint&lt;T&gt;](intervals.points.endpoint-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Deconstruct(T&, Inclusion&, EndpointLocation&)**

```csharp
void Deconstruct(T& Value, Inclusion& Inclusion, EndpointLocation& Location)
```

#### Parameters

`Value` T&<br>

`Inclusion` [Inclusion&](intervals.points.inclusion&)<br>

`Location` [EndpointLocation&](intervals.points.endpointlocation&)<br>

---

[`< Back`](./)
