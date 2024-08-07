﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace StringEnums;

// Class with self-referencing generic constraint:

// Rename type name StringEnum so that it does not end in 'Enum'
#pragma warning disable CA1711

public abstract class StringEnum<T> where T : StringEnum<T>, new()
#pragma warning restore CA1711
{
    // static ctor
    static StringEnum() => RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);

    private static Dictionary<string, T> Constants = [];
    public static void SetStringComparer(StringComparer comparer) =>
        Constants = new Dictionary<string, T>(Constants, comparer);

    public static IList<T> ToStringEnums()
    {
        lock (Constants)
        {
            return Constants.Values.Distinct().ToList();
        }
    }

    private string[] Strings = [];
    public IEnumerable<string> ToStrings() => Strings;
    public override string ToString() => Strings.FirstOrDefault("");

    protected static T Create(params string[] strings) =>
        Add(strings) ?? throw new ArgumentException($"StringEnum<{typeof(T).Name}>.Create(): string value in {(string.Join(",", strings))} already exists.");

    public static T? Add(params string[] strings)
    {
        ArgumentNullException.ThrowIfNull(strings);

        if (strings.Length == 0)
            throw new ArgumentException("No strings!", nameof(strings));

        lock (Constants)
        {
            if (strings.Any(str => Constants.ContainsKey(str)))
                return null;
            // null indicates that no StringEnum was added because at least one of the string arguments already exists.

            T constant = new() { Strings = strings };

            foreach (string str in strings)
                Constants.Add(str, constant);

            return constant;
        }
    }

    public static T? ToStringEnum(in string str)
    {
        ArgumentNullException.ThrowIfNull(str);

        lock (Constants)
        {
            if (Constants.TryGetValue(str, out T? constant))
                return constant;
            return null;
            // null indicates that no StringEnum was found for this string.
        }
    }
}
