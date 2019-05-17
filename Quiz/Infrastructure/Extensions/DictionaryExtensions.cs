using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Get <see cref="Dictionary{TKey,TValue}"/> that contains only keys from <see cref="keyWhiteList"/>
        /// </summary>
        public static Dictionary<TKey, TValue> TakeFrom<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            IEnumerable<TKey> keyWhiteList)
        {
            var hashSet = keyWhiteList.ToHashSet();
            return dictionary
                .Where(pair => hashSet.Contains(pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}