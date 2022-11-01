[`< Back`](./)

---

# Point&lt;T&gt;

Namespace: Intervals.Points

Represents an point instance

```csharp
public struct Point<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Point&lt;T&gt;](intervals.points.point-1)<br>
Implements IEquatable&lt;Point&lt;T&gt;&gt;

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

## Constructors

### **Point(T, Inclusion)**

Represents an point instance

```csharp
Point(T Value, Inclusion Inclusion)
```

#### Parameters

`Value` T<br>

`Inclusion` [Inclusion](intervals.points.inclusion)<br>

## Methods

### **ToString()**

Converts the value of this instance to "{Value}-{Inclusion}" format

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

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

### **Equals(Point&lt;T&gt;)**

```csharp
bool Equals(Point<T> other)
```

#### Parameters

`other` [Point&lt;T&gt;](intervals.points.point-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Deconstruct(T&, Inclusion&)**

```csharp
void Deconstruct(T& Value, Inclusion& Inclusion)
```

#### Parameters

`Value` T&<br>

`Inclusion` [Inclusion&](intervals.points.inclusion&)<br>

---

[`< Back`](./)
