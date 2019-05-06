using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class Splitter
    {
        public struct Key
        {
            internal string Value;

            public Key(Guid value) => Value = value.ToString();
        }

        public static (string result, Key) SafeConcat(this IList<string> strings)
        {
            var key = new Key(Guid.NewGuid());
            if (strings.Count == 0)
                return ("", key);
            var result = strings.Aggregate(String.Empty, (acc, item) => acc + key.Value + item);
            return (result, key);
        }

        public static string SafeConcat(this IList<string> strings, out Key outKey)
        {
            var (result,key) = strings.SafeConcat();
            outKey = key;
            return result;
        }
        public static bool TrySafeSplit(this string line, Key key, out IList<string> strings)
        {
            strings = null;

            strings = line.Split(key.Value).Where(s=>s!="").ToList();
            return true;
        }

        public static IList<string> SafeSplit(this string line, Key key)
        {
            TrySafeSplit(line, key, out var strings);
            return strings;
        }
    }
}