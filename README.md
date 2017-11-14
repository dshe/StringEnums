## StringEnums&nbsp;&nbsp;[![release](https://img.shields.io/github/release/dshe/StringEnums.svg)](https://github.com/dshe/StringEnums/releases) [![Build status](https://ci.appveyor.com/api/projects/status/a0qowb0k05hih5xm?svg=true)](https://ci.appveyor.com/project/dshe/stringenums) [![License](https://img.shields.io/badge/license-Apache%202.0-7755BB.svg)](https://opensource.org/licenses/Apache-2.0)
***A simple and flexible alternative to System.Enum***
- similar to System.Enum, but with underlying type string
- constants support **multiple string values**
- constants can be added **dynamically**
- contained in a **single** C# 7 source file supporting **.NET Standard 2.0**
- much faster than System.Enum with attributes
- simple and intuitive API
- type-safe
- tested

#### Core
Implement the following pattern to define StringEnum constants. Each constant is associated with one or more unique strings.
```csharp
public sealed class Location : StringEnum<Location>
{
     public static readonly Location Undefined = Create("");
     public static readonly Location Europe    = Create("Europe");
}

Location.Europe.ToString() => "Europe"

Location.ToStringEnum("Europe")    => Location.Europe
Location.ToStringEnum("not found") => null
```
#### Multiple String Values
When a StringEnum constant is associated with more than one string, the first string represents it's string value.
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

Location.America.ToStrings() => [] {"America", "USA"}
```

#### New Constants
After the StringEnum has been created, new constants can be added by calling Add().
```csharp
Location.Add("New Location") => Location newLocation

Location.ToStringEnum("New Location") => newLocation

newLocation.ToString() => "New Location"
```

#### String Case
```csharp
Location.ToStringEnum("EUROPE") => null
Location.SetStringComparer(StringComparer.OrdinalIgnoreCase);
Location.ToStringEnum("EUROPE") => Location.Europe
```
#### All Constants
```csharp
Location.ToStringEnums() => [] { Location.Undefined, Location.Europe, Location.America }
```
#### Extensions
```csharp
"Europe".ToStringEnum<Location>() => Location.Europe

Location.GetType().IsStringEnum() => true
```
