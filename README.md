## StringEnums&nbsp;&nbsp;

***A flexible alternative to System.Enum***
- a StringEnum is similar to System.Enum with underlying type string
- each StringEnum value is represented by one or more strings
- a StringEnum value is a reference type, so it's default value is null
- all StringEnums functionality is contained in a single C# 7 source file supporting .NET Standard 2.0+ with no dependencies
- StringEnums are much faster and easier than using member attributes and reflection
- extremely simple API
- tested

#### usage
```csharp
public sealed class OrderType : StringEnum<OrderType>
{
     public static readonly OrderType Market = Create("MARKET");
     public static readonly OrderType Limit  = Create("LIMIT");
}

OrderType.Market.ToString() => "MARKET"

OrderType.ToStringEnum("MARKET") => OrderType.Market

OrderType.ToStringEnums() => new List<OrderType> { OrderType.Market, OrderType.Limit }

```
#### new values
```csharp
OrderType.Market.IsNewValue => false

OrderType.ToStringEnum("SOME NEW VALUE").IsNewValue => true
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
#### case insensitivity
```csharp
Location.SetComparer(StringComparer.OrdinalIgnoreCase)
Location.ToStringEnum("EUROPE") => Location.Europe
```
#### extensions
```csharp
OrderType.Location.GetType().IsStringEnum() => true

"Europe".ToStringEnum<OrderType>() => OrderType.Europe
```
