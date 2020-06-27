## StringEnums&nbsp;&nbsp; [![Build status](https://ci.appveyor.com/api/projects/status/a0qowb0k05hih5xm?svg=true)](https://ci.appveyor.com/project/dshe/stringenums) [![NuGet](https://img.shields.io/nuget/vpre/StringEnums.svg)](https://www.nuget.org/packages/StringEnums/) [![License](https://img.shields.io/badge/license-Apache%202.0-7755BB.svg)](https://opensource.org/licenses/Apache-2.0)
***A simple and flexible alternative to System.Enum***
- similar to System.Enum, but with underlying type **string**
- constants support **multiple string values**
- constants can be added **dynamically**
- contained in a single C# 8 source file
- supports **.NET Standard 2.0**
- much faster than System.Enum with attributes
- simple and intuitive API
- no dependencies
- type-safe
- tested


Implement the pattern used in the example below to define StringEnum constants. Note that each constant is associated with one or more unique strings:
```csharp
public sealed class SecurityType : StringEnum<SecurityType>
{
    public static readonly SecurityType Undefined = Create("");
    public static readonly SecurityType Cash      = Create("C");
    public static readonly SecurityType Stock     = Create("STK");
    public static readonly SecurityType Bond      = Create("BOND", "BND");
}
```
```csharp
SecurityType.Cash.ToString() => "C"

SecurityType.ToStringEnum("C")         => SecurityType.Cash
SecurityType.ToStringEnum("not found") => null
```
#### Multiple String Values
When a StringEnum constant is associated with more than one string, the first string represents its string value.
```csharp
SecurityType.ToStringEnum("BOND") => SecurityType.Bond
SecurityType.ToStringEnum("BND")  => SecurityType.Bond

SecurityType.Bond.ToString()  => "BOND"

SecurityType.Bond.ToStrings() => [] {"BOND", "BND"}
```
#### New Constants
After the StringEnum has been created, new constants can be added by calling Add().
```csharp
SecurityType newSecurityType = SecurityType.Add("New SecurityType");

SecurityType.ToStringEnum("New SecurityType") => newSecurityType

newSecurityType.ToString() => "New SecurityType"
```
#### All Constants
```csharp
SecurityType.ToStringEnums() =>
    [] { SecurityType.Undefined, SecurityType.Cash, SecurityType.Stock, SecurityType.Bond, newSecurityType }
```
#### String Case
```csharp
SecurityType.ToStringEnum("stk") => null

SecurityType.SetStringComparer(StringComparer.OrdinalIgnoreCase);
SecurityType.ToStringEnum("stk") => SecurityType.Stock
```
#### Extensions
```csharp
"C".ToStringEnum<SecurityType>() => SecurityType.Cash

SecurityType.Cash.GetType().IsStringEnum() => true
```
