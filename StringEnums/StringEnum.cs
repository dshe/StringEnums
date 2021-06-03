using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
/*
Self Referencing Generic Constraint

Notice that the generic constraint is self-referencing. This is a pattern that Eric Lippert discourages:
Yes it is legal, and it does have some legitimate uses. I see this pattern rather a lot(**). However, I personally don’t like it and I discourage its use.
This is a C# variation on what’s called the Curiously Recurring Template Pattern in C++, and I will leave it to my betters to explain its uses in that language.
Essentially the pattern in C# is an attempt to enforce the usage of the CRTP.
…snip…
So that’s one good reason to avoid this pattern: because it doesn’t actually enforce the constraint you think it does.
…snip…
The second reason to avoid this is simply because it bakes the noodleof anyone who reads the code.
https://haacked.com/archive/2012/09/30/primitive-obsession-custom-string-types-and-self-referencing-generic-constraints.aspx/
*/
namespace StringEnums
{
    public abstract class StringEnum<T> where T : StringEnum<T>, new()
    {
        static StringEnum() => RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle); // static ctor

        private static Dictionary<string, T> Constants = new();
        public static void SetStringComparer(StringComparer comparer) => Constants = new Dictionary<string, T>(Constants, comparer);

        public static IList<T> ToStringEnums()
        {
            lock (Constants)
                return Constants.Values.Distinct().ToList();
        }

        private string[] Strings = Array.Empty<string>();

        public IList<string> ToStrings() => Strings.ToList(); // return a copy

        public override string ToString() => Strings.FirstOrDefault() ?? "";

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
                if (Constants.TryGetValue(str, out T? constant))
                    return constant;
                return null; // null indicates that no StringEnum was found for this string.
            }
        }
    }
}
