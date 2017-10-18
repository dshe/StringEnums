## StringEnums&nbsp;&nbsp;

***A flexible alternative to System.Emum***
- a StringEnum is similar to System.Emum with underlying type string
- each StringEnum value may be represented by one or more strings
- a StringEnum is a reference type, so it's default value is null
- contained in a single C# 7 source file supporting .NET Standard 2.0+ with no dependencies
- faster than attributes
- tested

#### usage
```csharp
public sealed class OrderType : StringEnum<OrderType>
{
     public static readonly OrderType Undefined = Create("");
     public static readonly OrderType Market = Create("MARKET");
     public static readonly OrderType Limit = Create("LIMIT");
}

Assert.Equal(OrderType.Market, OrderType.Parse("MARKET"));
Assert.Equal("MARKET", OrderType.Market.ToString());
```
#### new values
```csharp
Assert.False(OrderType.Market.IsNewValue);

var newValue = OrderType.Parse("SOME NEW VALUE");
Assert.True(newValue.IsNewValue);
```
#### multiple values
```csharp
public sealed class Location : StringEnum<Location>
{
     public static readonly Location Europe = Create("Europe");
     public static readonly Location America = Create("America", "USA");
}

Assert.Equal(Location.America, Location.Parse("America"));
Assert.Equal(Location.America, Location.Parse("USA"));
Assert.Equal("America", Location.America.ToString());
```
#### case insensitivity
```csharp
Location.SetComparer(StringComparer.OrdinalIgnoreCase);
Assert.Equal(Location.Europe, Location.Parse("EUROPE"));
```
#### extensions
```csharp
Assert.True(typeof(OrderType).IsStringEnum());
Assert.True(typeof(OrderType).GetTypeInfo().IsStringEnum());

Assert.Equal(OrderType.Market, "MARKET".ToStringEnum<OrderType>());
```


