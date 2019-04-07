using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable enable

namespace StringEnums
{
    public static class Extensions
    {
        public static T? ToStringEnum<T>(this string str) where T : StringEnum<T>, new() =>
            StringEnum<T>.ToStringEnum(str);

        public static bool IsStringEnum(this Type type) =>
            IsStringEnum(type.GetTypeInfo());

        public static bool IsStringEnum(this TypeInfo typeInfo)
        {
            if (typeInfo == null)
                throw new ArgumentNullException(nameof(typeInfo));
            var baseType = typeInfo.BaseType;
            if (baseType == null || !baseType.GetTypeInfo().IsGenericType)
                return false;
            return baseType.GetGenericTypeDefinition() == typeof(StringEnum<>);
        }
    }
}
