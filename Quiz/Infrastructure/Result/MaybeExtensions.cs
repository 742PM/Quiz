using System;
using System.Threading.Tasks;
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

        public static Result<T, TE> ToResult<T, TE>(this Maybe<T> maybe, TE error) where TE : Exception
        {
            if (maybe.HasNoValue)
                return Result.Fail<T, TE>(error);

            return Result.Ok<T, TE>(maybe.Value);
        }

        public static T Unwrap<T>(this Maybe<T> maybe, T defaultValue = default) => maybe.Unwrap(x => x, defaultValue);

        public static TK Unwrap<T, TK>(this Maybe<T> maybe, Func<T, TK> selector, TK defaultValue = default) => maybe.HasValue ? selector(maybe.Value) : defaultValue;

        public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
        {
            if (maybe.HasNoValue)
                return Maybe<T>.None;

            return predicate(maybe.Value) ? maybe : Maybe<T>.None;
        }

        public static Maybe<TK> Map<T, TK>(this Maybe<T> maybe, Func<T, TK> selector) => maybe.HasNoValue ? Maybe<TK>.None : selector(maybe.Value);

        public static Maybe<TK> FlatMap<T, TK>(this Maybe<T> maybe, Func<T, Maybe<TK>> selector) => maybe.HasNoValue ? Maybe<TK>.None : selector(maybe.Value);

        public static async Task<Maybe<TK>> MapAsync<T, TK>(this Task<Maybe<T>> maybe, Func<T, TK> selector) => (await maybe).HasNoValue ? Maybe<TK>.None : selector(maybe.Result.Value);

        public static async Task<Maybe<TK>> FlatMapAsync<T, TK>(this Task<Maybe<T>> maybe, Func<T, Maybe<TK>> selector) =>
            (await maybe).HasNoValue ? Maybe<TK>.None : selector(maybe.Result.Value);
        public static async Task<Maybe<TK>> FlatMapAsync<T, TK>(this Task<Maybe<T>> maybe, Func<T, Task<Maybe<TK>>> selector) =>
            (await maybe).HasNoValue ? Maybe<TK>.None : await selector(maybe.Result.Value);

        public static void Execute<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.HasNoValue)
                return;
            action(maybe.Value);
        }
    }
}