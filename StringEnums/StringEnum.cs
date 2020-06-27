﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        private string[] Strings = new string[] { };

        public IList<string> ToStrings() => Strings.ToList(); // return a copy

        public override string ToString() => Strings.FirstOrDefault();

        protected static T Create(params string[] strings) =>
            Add(strings) ?? throw new ArgumentException($"StringEnum<{typeof(T).Name}>.Create(): string value in {(string.Join(",", strings))} already exists.");

        public static T? Add(params string[] strings)
        {
            if (strings == null)
                throw new ArgumentNullException(nameof(strings));
            if (!strings.Any())
                throw new ArgumentException(nameof(strings));

            lock (Constants)
            {
                if (strings.Where(str => Constants.ContainsKey(str)).Any())
                    return null; // null indicates that no StringEnum was added because at least one of the string arguments already exists.

                var constant = new T { Strings = strings };

                foreach (var str in strings)
                    Constants.Add(str, constant);

                return constant;
            }
        }

        public static T? ToStringEnum(in string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            lock (Constants)
            {
                if (Constants.TryGetValue(str, out T constant))
                    return constant;
                return null; // null indicates that no StringEnum was found for this string.
            }
        }
    }
}
