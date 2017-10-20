## StringEnums&nbsp;&nbsp;

***A flexible alternative to System.Enum***
- a StringEnum is similar to System.Enum with underlying type string
- a StringEnum value is a reference type, so it's default value is null
- all StringEnums functionality is contained in a single C# 7 source file supporting .NET Standard 2.0+
- much faster and easier than using member attributes and reflection
- extremely simple API
- type-safe
- tested

#### basic usage
```csharp
public sealed class OrderType : StringEnum<OrderType>
{
     public static readonly OrderType Market = Create("MARKET");
     public static readonly OrderType Limit  = Create("LIMIT");
}

OrderType.Market.ToString() => "MARKET"

OrderType.ToStringEnum("MARKET") => OrderType.Market
```
#### multiple values
```csharp
public sealed class Location : StringEnum<Location>
{
     public static readonly Location Undefined = Create("");
     public static readonly Location Europe    = Create("Europe");
     public static readonly Location America   = Create("America", "USA");
}

Location.ToStringEnum("America")  => Location.America
Location.ToStringEnum("USA")      => Location.America

Location.America.ToString()  => "America"

Location.America.ToStrings() => new List<string> {"America", "USA"}
```
#### new values
```csharp
Location.Undefined.IsNewValue => false
Location.Europe.IsNewValue => false
Location.America.IsNewValue => false

var newValue = OrderType.ToStringEnum("NEW VALUE");
newValue.IsNewValue => true
```
#### case insensitivity
```csharp
Location.SetStringComparer(StringComparer.OrdinalIgnoreCase);
Location.ToStringEnum("EUROPE") => Location.Europe
```
#### all values
```csharp
Location.ToStringEnums() => new List<Location> { Location.Undefined, Location.Europe, Location.America }
```
#### extensions
```csharp
"Europe".ToStringEnum<Location>() => Location.Europe

Location.GetType().IsStringEnum() => true
```
