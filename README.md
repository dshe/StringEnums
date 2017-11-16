## StringEnums&nbsp;&nbsp;[![release](https://img.shields.io/github/release/dshe/StringEnums.svg)](https://github.com/dshe/StringEnums/releases) [![Build status](https://ci.appveyor.com/api/projects/status/a0qowb0k05hih5xm?svg=true)](https://ci.appveyor.com/project/dshe/stringenums) [![License](https://img.shields.io/badge/license-Apache%202.0-7755BB.svg)](https://opensource.org/licenses/Apache-2.0)
***A simple and flexible alternative to System.Enum***
- similar to System.Enum, but with underlying type string
- constants support **multiple string values**
- constants can be added **dynamically**
- contained in a **single** C# 7 source file supporting **.NET Standard 2.0**
- faster than System.Enum with attributes
- simple and intuitive API
- type-safe
- tested

#### Core
Implement the following pattern to define StringEnum constants. Each constant is associated with one or more unique strings.
```csharp
public sealed class SecurityType : StringEnum<SecurityType>
{
    public static readonly SecurityType Undefined = Create("");
    public static readonly SecurityType Cash      = Create("C");
    public static readonly SecurityType Stock     = Create("STK");
    public static readonly SecurityType Bond      = Create("BOND", "BND");
}

SecurityType.Cash.ToString() => "C"

SecurityType.ToStringEnum("C")         => SecurityType.Cash
SecurityType.ToStringEnum("not found") => null
```
#### Multiple String Values
When a StringEnum constant is associated with more than one string, the first string represents it's string value.
```csharp
SecurityType.ToStringEnum("BOND") => SecurityType.Bond
SecurityType.ToStringEnum("BND")  => SecurityType.Bond

SecurityType.Bond.ToString() => "BOND"

SecurityType.Bond.ToStrings() => [] {"BOND", "BND"}
```
#### New Constants
After the StringEnum has been created, new constants can be added by calling Add().
```csharp
SecurityType.Add("New SecurityType") => SecurityType newSecurityType

SecurityType.ToStringEnum("New SecurityType") => newSecurityType

newSecurityType.ToString() => "New SecurityType"
```

#### String Case
```csharp
SecurityType.ToStringEnum("stk") => null
SecurityType.SetStringComparer(StringComparer.OrdinalIgnoreCase);
SecurityType.ToStringEnum("stk") => Location.Stock
```
#### All Constants
```csharp
SecurityType.ToStringEnums() => [] { SecurityType.Undefined, SecurityType.Cash, SecurityType.Stock, SecurityType.Bond, newSecurityType }
```
#### Extensions
```csharp
"C".ToStringEnum<SecurityType>() => SecurityType.Cash

SecurityType.Cash.GetType().IsStringEnum() => true
```
