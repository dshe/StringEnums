using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace StringEnums.Tests.Utility
{
    public static class PrimitiveEnumEx
    {
        public static string ToString(this Enum value) => 
            value
            .GetType()
            .GetField(value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(inherit: false)
            ?.Value
            ?? value.ToString();

        public static T ToEnum<T>(this string str) where T : struct =>
            (T)(typeof(T)
            .GetTypeInfo()
            .DeclaredFields
            .Where(m => m.GetCustomAttribute<EnumMemberAttribute>()?.Value == str)
            .SingleOrDefault()
            ?.GetValue(null)
            ?? Enum.Parse<T>(str));
    }

}
