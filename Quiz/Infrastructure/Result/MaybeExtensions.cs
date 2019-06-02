using System;
using Infrastructure.Extensions;

namespace Infrastructure.Result
{
    public static class MaybeExtensions
    {
        public static Maybe<TOut> Try<TIn, TOut>(this TIn item, Func<TIn, TOut> func) => item.Try(x => func(x).Sure());

        public static Maybe<TOut> Try<TIn, TOut>(this TIn item, Func<TIn, Maybe<TOut>> func)
        {
            try
            {
                return func(item);
            }
            catch
            {
                return Maybe<TOut>.None;
            }
        }

        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage)
        {
            if (maybe.HasNoValue)
                return Result.Fail<T>(errorMessage);

            return Result.Ok(maybe.Value);
        }

        public static Result<T, E> ToResult<T, E>(this Maybe<T> maybe, E error) where E : Exception
        {
            if (maybe.HasNoValue)
                return Result.Fail<T, E>(error);

            return Result.Ok<T, E>(maybe.Value);
        }

        public static T Unwrap<T>(this Maybe<T> maybe, T defaultValue = default) => maybe.Unwrap(x => x, defaultValue);

        public static K Unwrap<T, K>(this Maybe<T> maybe, Func<T, K> selector, K defaultValue = default) => maybe.HasValue ? selector(maybe.Value) : defaultValue;

        public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
        {
            if (maybe.HasNoValue)
                return Maybe<T>.None;

            return predicate(maybe.Value) ? maybe : Maybe<T>.None;
        }

        public static Maybe<K> Select<T, K>(this Maybe<T> maybe, Func<T, K> selector) => maybe.HasNoValue ? Maybe<K>.None : selector(maybe.Value);

        public static Maybe<K> Select<T, K>(this Maybe<T> maybe, Func<T, Maybe<K>> selector) => maybe.HasNoValue ? Maybe<K>.None : selector(maybe.Value);

        public static void Execute<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.HasNoValue)
                return;
            action(maybe.Value);
        }
    }
}