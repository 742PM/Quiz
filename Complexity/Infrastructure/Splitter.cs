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

            internal Key(Guid value) => Value = value.ToString();
        }

        public static (string result, Key) SafeConcat(this IList<string> strings)
        {
            if (strings.IsNullOrEmpty() || strings.Count < 2 )
                throw new ArgumentException("You can not concat less than two strings");

            var key = new Key(Guid.NewGuid());
            var result = strings.Aggregate(String.Empty, (acc, item) => acc + key.Value + item);
            return (result, key);


        }
        public static bool TrySafeSplit(this string line, Key key, out IList<string> strings)
        {
            strings = null;
            if (!line.Contains(key.Value))
                return false;
            strings = line.Split(key.Value).Where(s=>!s.IsNullOrEmpty()).ToList();
            return true;
        }
    }
}