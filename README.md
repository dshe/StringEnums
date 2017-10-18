## StringEnum&nbsp;&nbsp;

***An alternative to System.Emum***

- A StringEnum is similar to System.Emum but with underlying type string.
- Each StringEnum value may be represented by one or more string values.
- StringEnum is a reference type, so it's default value is null.
- StringEnum is fast.

#### example
```csharp
public sealed class OrderType : StringEnum<OrderType>
{
     public static readonly OrderType Undefined = Create("");
     public static readonly OrderType Market = Create("MARKET");
     public static readonly OrderType Limit = Create("LIMIT");
     public static readonly OrderType TrailingLimit = Create("TRAIL LIMIT");
}

Assert.Equal(OrderType.Market, OrderType.Parse("MARKET"));
Assert.Equal("MARKET", OrderType.Market.ToString());

```
