/*
StringEnums 1.1
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
        static StringEnum() => RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle); // static ctor

        private static Dictionary<string, T> Constants = new Dictionary<string, T>();
        public static void SetStringComparer(StringComparer comparer) => Constants = new Dictionary<string, T>(Constants, comparer);

        public static IList<T> ToStringEnums()
        {
            lock (Constants)
                return Constants.Values.Distinct().ToList();
        }

        private string[] Strings;
        public IList<string> ToStrings() => Strings.ToList(); // return a copy
        public override string ToString() => Strings[0];

        protected static T Create(params string[] strings)
        {
            var constant = Add(strings);
            if (constant == null)
                throw new ArgumentException($"StringEnum<{typeof(T).Name}>.Create(): string value in {(string.Join(",", strings))} already exists.");
            return constant;
        }

        public static T Add(params string[] strings)
        {
            if (strings == null || strings.Length == 0 || strings.Any(x => x == null))
                throw new ArgumentException(nameof(strings));

            lock (Constants)
            {
                foreach (var str in strings)
                    if (Constants.ContainsKey(str))
                        return null; // Null indicates that no StringEnum was added because at least one of the string arguments already exists.

                var constant = new T { Strings = strings };

                foreach (var str in strings)
                    Constants.Add(str, constant);

                return constant;
            }
        }

        public static T ToStringEnum(string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            lock (Constants)
            {
                if (!Constants.TryGetValue(str, out T constant))
                    return null; // Null indicates that no StringEnum was found for this string.
                return constant;
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
