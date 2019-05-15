using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> TakeIfIn<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            HashSet<TKey> keyWhiteList)
        {
            return dictionary
                .Where(pair => keyWhiteList.Contains(pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}