using System;
using Infrastructure.Result;

namespace Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static Result<T, Exception> Ok<T>(this T obj) => obj;
        public static Maybe<T> Sure<T>(this T obj) => obj;

        [Unsafe]
        public static TOut Cast<TOut>(this object obj) => (TOut)obj;

        public static TOut AndThen<TIn, TOut>(this TIn item, Func<TIn, TOut> mapper) => mapper(item);
        
    }
}
