## StringEnums&nbsp;&nbsp;

***A simple and flexible alternative to System.Enum***
- a StringEnum is similar to System.Enum with underlying type string
- a StringEnum value is a reference type, so it's default value is null
- all StringEnum functionality is contained in a single C# 7 source file supporting .NET Standard 2.0+
- StringEnums are faster and easier than using member attributes and reflection to support strings
- simple and intuitive API
- type-safe
- tested

#### basic usage
Use Create(string) to create a StringEnum value associated with one or more unique strings.
```csharp
public sealed class Location : StringEnum<Location>
{
     public static readonly Location Undefined = Create("");
     public static readonly Location Europe    = Create("Europe");
}
Location.Europe.ToString() => "Europe"

Location.ToStringEnum("Europe") => Location.Europe
```
#### multiple values
When a StringEnum value is associated with more than one string, the first string represents it's string value.
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
When ToStringEnum(string) is called with a string that is not associated with any StringEnum, a new StringEnum is created which has IsNewValue set to true. 
```csharp
Location.Undefined.IsNewValue => false
Location.Europe.IsNewValue    => false
Location.America.IsNewValue   => false

Location.ToStringEnum("Europe")  => Location.Europe

Location newLocation = Location.ToStringEnum("NEW VALUE");
newLocation.IsNewValue => true
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
