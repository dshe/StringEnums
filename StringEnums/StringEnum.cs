using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace StringEnums;

// Class with self-referencing generic constraint:

// Rename type name StringEnum so that it does not end in 'Enum'
#pragma warning disable CA1711

public abstract class StringEnum<T> where T : StringEnum<T>, new()
#pragma warning restore CA1711
{
    // static ctor
    static StringEnum() => RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);

    private static Dictionary<string, T> _constants = [];
    public static void SetStringComparer(StringComparer comparer) =>
        _constants = new Dictionary<string, T>(_constants, comparer);

    public static IList<T> ToStringEnums()
    {
        lock (_constants)
        {
            return _constants.Values.Distinct().ToList();
        }
    }

    private string[] _strings = [];
    public IEnumerable<string> ToStrings() => _strings;
    public override string ToString() => _strings.FirstOrDefault("");

    protected static T Create(params string[] strings) =>
        Add(strings) ?? throw new ArgumentException($"StringEnum<{typeof(T).Name}>.Create(): string value in {(string.Join(",", strings))} already exists.");

    public static T? Add(params string[] strings)
    {
        ArgumentNullException.ThrowIfNull(strings);

        if (strings.Length == 0)
            throw new ArgumentException("No strings!", nameof(strings));

        lock (_constants)
        {
            if (strings.Any(str => _constants.ContainsKey(str)))
                return null;
            // null indicates that no StringEnum was added because at least one of the string arguments already exists.

            T constant = new() { _strings = strings };

            foreach (string str in strings)
                _constants.Add(str, constant);

            return constant;
        }
    }

    public static T? ToStringEnum(in string str)
    {
        ArgumentNullException.ThrowIfNull(str);

        lock (_constants)
        {
            if (_constants.TryGetValue(str, out T? constant))
                return constant;
            return null;
            // null indicates that no StringEnum was found for this string.
        }
    }
}
