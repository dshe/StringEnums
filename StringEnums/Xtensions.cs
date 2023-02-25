using System.Reflection;

namespace StringEnums;

public static class Xtensions
{
    public static T? ToStringEnum<T>(this string str) where T : StringEnum<T>, new() =>
        StringEnum<T>.ToStringEnum(str);


    public static bool IsStringEnum(this Type type) =>
        IsStringEnum(type.GetTypeInfo());


    public static bool IsStringEnum(this TypeInfo typeInfo)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);

        Type? baseType = typeInfo.BaseType;
        if (baseType is null || !baseType.GetTypeInfo().IsGenericType)
            return false;

        return baseType.GetGenericTypeDefinition() == typeof(StringEnum<>);
    }
}
