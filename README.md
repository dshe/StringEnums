## StringEnums&nbsp;&nbsp;

***A flexible alternative to System.Enum***
- a StringEnum is similar to System.Enum with underlying type string
- each StringEnum value is represented by one or more strings
- a StringEnum value is a reference type, so it's default value is null
- All StringEnums functionality is contained in a single C# 7 source file supporting .NET Standard 2.0+ with no dependencies
- StringEnums are faster and easier than using string attributes and reflection
- very simple API
- tested

#### usage
```csharp
public sealed class OrderType : StringEnum<OrderType>
{
     public static readonly OrderType Market = Create("MARKET");
     public static readonly OrderType Limit = Create("LIMIT");
}

Assert.Equal("MARKET", OrderType.Market.ToString());

Assert.Equal(OrderType.Market, OrderType.ToStringEnum("MARKET"));
Assert.Equal(OrderType.Market, "MARKET".ToStringEnum<OrderType>());
```
#### new values
```csharp
Assert.False(OrderType.Market.IsNewValue);

var newValue = OrderType.ToStringEnum("SOME NEW VALUE");
Assert.True(newValue.IsNewValue);
```
#### multiple values
```csharp
public sealed class Location : StringEnum<Location>
{
     public static readonly Location Undefined = Create("");
     public static readonly Location Europe = Create("Europe");
     public static readonly Location America = Create("America", "USA");
}

Assert.Equal(Location.America, Location.ToStringEnum("America"));
Assert.Equal(Location.America, Location.ToStringEnum("USA"));

Assert.Equal("America", Location.America.ToString());
```
#### case insensitivity
```csharp
Location.SetComparer(StringComparer.OrdinalIgnoreCase);
Assert.Equal(Location.Europe, Location.ToStringEnum("EUROPE"));
```
#### extensions
```csharp
Assert.True(OrderType.Location.GetType().IsStringEnum());

Assert.Equal(OrderType.Europe, "Europe".ToStringEnum<OrderType>());
```
