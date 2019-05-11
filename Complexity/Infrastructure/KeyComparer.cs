using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class KeyComparer<TSource, TKey> : IEqualityComparer<TSource>
    {
        private readonly Func<TSource, TKey> selector;

        public KeyComparer(Func<TSource, TKey> selector)
        {
            this.selector = selector;
        }

        public bool Equals(TSource x, TSource y) => selector(x).Equals(selector(y));

        public int GetHashCode(TSource obj) => selector(obj).GetHashCode();
    }
}