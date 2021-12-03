# StringEnums&nbsp;&nbsp; [![Build status](https://ci.appveyor.com/api/projects/status/a0qowb0k05hih5xm?svg=true)](https://ci.appveyor.com/project/dshe/stringenums) [![NuGet](https://img.shields.io/nuget/vpre/StringEnums.svg)](https://www.nuget.org/packages/StringEnums/) [![License](https://img.shields.io/badge/license-Apache%202.0-7755BB.svg)](https://opensource.org/licenses/Apache-2.0)
***A simple and flexible alternative to System.Enum***
- **.NET 6.0** library
- similar to System.Enum, but with underlying type **string**
- constants support **multiple string values**
- constants may be added **dynamically**
- much faster than System.Enum with attributes
- simple and intuitive API
- CLS compliant
- tested
- no dependencies

Implement the pattern used in the example below to define StringEnum constants. Note that each constant is associated with one or more unique strings:
```csharp
public sealed class SecurityType : StringEnum<SecurityType>
{
    public static SecurityType Undefined { get; } = Create("");
    public static SecurityType Cash      { get; } = Create("C");
    public static SecurityType Stock     { get; } = Create("STK");
    public static SecurityType Bond      { get; } = Create("BOND", "BND");
}
```
```csharp
Assert.Equal("C", SecurityType.Cash.ToString());

Assert.Equal(SecurityType.Cash, SecurityType.ToStringEnum("C"));
Assert.Null(SecurityType.ToStringEnum("not found"));

```
### Multiple String Values
When a StringEnum constant is associated with more than one string, the first string represents its string value.
```csharp
Assert.Equal(SecurityType.Bond, SecurityType.ToStringEnum("BOND"));
Assert.Equal(SecurityType.Bond, SecurityType.ToStringEnum("BND"));

Assert.Equal("BOND", SecurityType.Bond.ToString());

Assert.Equal(new[] { "BOND", "BND" }, SecurityType.Bond.ToStrings());
```
### New Constants
After the StringEnum has been created, new constants can be added by calling Add().
```csharp
SecurityType? newSecurityType = SecurityType.Add("New SecurityType");

Assert.NotNull(newSecurityType);

Assert.Equal(newSecurityType, SecurityType.ToStringEnum("New SecurityType"));

Assert.Equal("New SecurityType", newSecurityType.ToString());
```
### All Constants
```csharp
Assert.Equal(
    new[] { SecurityType.Undefined, SecurityType.Cash, SecurityType.Stock, SecurityType.Bond, newSecurityType },
    SecurityType.ToStringEnums());
```
### String Case
```csharp
Assert.Null(SecurityType.ToStringEnum("stk"));

SecurityType.SetStringComparer(StringComparer.OrdinalIgnoreCase);
Assert.Equal(SecurityType.Stock, SecurityType.ToStringEnum("stk"));
```
### Extensions
```csharp
Assert.Equal(SecurityType.Cash, "C".ToStringEnum<SecurityType>());

Assert.True(SecurityType.Cash.GetType().IsStringEnum());
```
