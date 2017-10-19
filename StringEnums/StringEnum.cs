/*
StringEnums 1.0
https://github.com/dshe/StringEnums
Copyright(c) 2017 DavidS.
Licensed under the Apache License 2.0:
http://www.apache.org/licenses/LICENSE-2.0
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StringEnums
{
    public abstract class StringEnum<T> where T : StringEnum<T>, new()
    {
        private static Dictionary<string, T> Dict = new Dictionary<string, T>();
        public static void SetComparer(StringComparer comparer) => Dict = new Dictionary<string, T>(Dict, comparer);
        private string Value;
        public bool IsNewValue { get; private set; }
        public override string ToString() => Value;

        static StringEnum() => RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);

        protected static T Create(params string[] strings)
        {
            if (strings == null || strings.Length == 0 || strings.Any(x => x == null))
                throw new ArgumentException(nameof(strings));

            // if more than one string is provided, the first string represents the value of the item.
            var t = new T { Value = strings[0] };

            foreach (var s in strings)
            {
                try
                {
                    lock (Dict)
                    {
                        Dict.Add(s, t);
                    }
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException($"StringEnum: {typeof(T).Name} has duplicate value: {s}.", e);
                }
            }
            return t;
        }

        public static T ToStringEnum(string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            lock (Dict)
            {
                if (!Dict.TryGetValue(s, out T t))
                {
                    // The enum value was not found so add it and indicate it is a new value.
                    t = new T { Value = s, IsNewValue = true };
                    Dict.Add(s, t);
                }
                return t;
            }
        }
    }

    public static class StringEnumsEx
    {
        public static T ToStringEnum<T>(this string str) where T: StringEnum<T>, new()
            => StringEnum<T>.ToStringEnum(str);

        public static bool IsStringEnum(this Type type) => IsStringEnum(type.GetTypeInfo());
        public static bool IsStringEnum(this TypeInfo typeInfo)
            => typeInfo.BaseType.GetTypeInfo().IsGenericType
              && typeInfo.BaseType.GetGenericTypeDefinition() == typeof(StringEnum<>);
    }
}
