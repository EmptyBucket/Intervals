# DateTimeExtensions

Namespace: Intervals.Utils

```csharp
public static class DateTimeExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DateTimeExtensions](./intervals.utils.datetimeextensions.md)

## Methods

### **Ceiling(DateTime, TimeSpan)**

Returns the smallest value aligned to  that is greater than or equal to the specified

```csharp
public static DateTime Ceiling(DateTime dateTime, TimeSpan granuleSize)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`granuleSize` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **CeilingToMonth(DateTime)**

Returns the smallest value aligned to month that is greater than or equal to the specified

```csharp
public static DateTime CeilingToMonth(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **CeilingToQuarter(DateTime)**

Returns the smallest value aligned to quarter that is greater than or equal to the specified

```csharp
public static DateTime CeilingToQuarter(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **CeilingToHalfYear(DateTime)**

Returns the smallest value aligned to half-year that is greater than or equal to the specified

```csharp
public static DateTime CeilingToHalfYear(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **GetQuarter(DateTime)**

Returns the quarter number for the specified

```csharp
public static int GetQuarter(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
quarter number

### **GetHalfYear(DateTime)**

Returns the half-year number for the specified

```csharp
public static int GetHalfYear(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
half-year number

### **Floor(DateTime, TimeSpan)**

Returns the largest value aligned to  that is less than or equal to the specified

```csharp
public static DateTime Floor(DateTime dateTime, TimeSpan granuleSize)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`granuleSize` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **FloorToMonth(DateTime)**

Returns the largest value aligned to month that is less than or equal to the specified

```csharp
public static DateTime FloorToMonth(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **FloorToQuarter(DateTime)**

Returns the largest value aligned to quarter that is less than or equal to the specified

```csharp
public static DateTime FloorToQuarter(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **FloorToHalfYear(DateTime)**

Returns the largest value aligned to half-year that is less than or equal to the specified

```csharp
public static DateTime FloorToHalfYear(DateTime dateTime)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **Round(DateTime, TimeSpan, MidpointRounding)**

Rounds a  to a value aligned to , and uses the specified rounding convention for midpoint values.

```csharp
public static DateTime Round(DateTime dateTime, TimeSpan granuleSize, MidpointRounding midpointRounding)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`granuleSize` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **RoundToMonth(DateTime, MidpointRounding)**

Rounds a  to a value aligned to month, and uses the specified rounding convention for midpoint values.

```csharp
public static DateTime RoundToMonth(DateTime dateTime, MidpointRounding midpointRounding)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **RoundToQuarter(DateTime, MidpointRounding)**

Rounds a  to a value aligned to quarter, and uses the specified rounding convention for midpoint values.

```csharp
public static DateTime RoundToQuarter(DateTime dateTime, MidpointRounding midpointRounding)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

### **RoundToHalfYear(DateTime, MidpointRounding)**

Rounds a  to a value aligned to half-year, and uses the specified rounding convention for midpoint values.

```csharp
public static DateTime RoundToHalfYear(DateTime dateTime, MidpointRounding midpointRounding)
```

#### Parameters

`dateTime` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`midpointRounding` [MidpointRounding](https://docs.microsoft.com/en-us/dotnet/api/system.midpointrounding)<br>

#### Returns

[DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>
