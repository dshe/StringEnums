## StringEnums&nbsp;&nbsp;

***A simple and flexible alternative to System.Enum***
- similar to System.Enum, but with underlying type string
- easier and faster than System.Enum with attributes
- constants support multiple string values
- constants can be added dynamically
- contained in a single C# 7 source file supporting .NET Standard 2.0+
- simple and intuitive API
- type-safe
- tested

#### basic usage
Implement the pattern below to define StringEnum constants associated with one or more unique strings.
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
#### multiple string values
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

Location.America.ToStrings() => new[] {"America", "USA"}
```
#### new constants
Call Add() to add new constants to the StringEnum.
```csharp
Location newLocation = Location.Add("New Location");

Assert.Equal(newLocation, Location.ToStringEnum("New Location"));

Assert.Equal("New Location", newLocation.ToString());

Assert.Equal(new[] { Location.Undefined, Location.Europe, Location.America, newLocation },
      Location.ToStringEnums());
```
#### string case
```csharp
Location.SetStringComparer(StringComparer.OrdinalIgnoreCase);
Location.ToStringEnum("EUROPE") => Location.Europe
```
#### all constants
```csharp
Location.ToStringEnums() => new[] { Location.Undefined, Location.Europe, Location.America }
```
#### extensions
```csharp
"Europe".ToStringEnum<Location>() => Location.Europe

Location.GetType().IsStringEnum() => true
```
