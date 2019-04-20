using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Result
{
    public static class ResultExtensions
    {
        public static Result<TK, TE> OnSuccess<T, TK, TE>(this Result<T, TE> result, Func<T, TK> func) where TE : Exception
        {
            if (result.IsFailure)
                return Result.Fail<TK, TE>(result.Error);

            return Result.Ok<TK, TE>(func(result.Value));
        }

        public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<T, TK> func)
        {
            if (result.IsFailure)
                return Result.Fail<TK>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        public static Result<TK, TE> OnSuccess<T, TK, TE>(this Result<T, TE> result, Func<T, Result<TK, TE>> func)
            where TE : Exception
        {
            if (result.IsFailure)
                return Result.Fail<TK, TE>(result.Error);

            return func(result.Value);
        }

        public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<T, Result<TK>> func)
        {
            if (result.IsFailure)
                return Result.Fail<TK>(result.Error);

            return func(result.Value);
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return func();
        }

        public static Result<TK, TE> OnSuccess<T, TK, TE>(this Result<T, TE> result, Func<Result<TK, TE>> func) where TE : Exception
        {
            if (result.IsFailure)
                return Result.Fail<TK, TE>(result.Error);

            return func();
        }

        public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<Result<TK>> func)
        {
            if (result.IsFailure)
                return Result.Fail<TK>(result.Error);

            return func();
        }

        public static Result<TK> OnSuccess<T, TK, TE>(this Result<T, TE> result, Func<T, Result<TK>> func) where TE : Exception
        {
            if (result.IsFailure)
                return Result.Fail<TK, TE>(result.Error);

            return func(result.Value);
        }

        public static Result OnSuccess<T, TK, TE>(this Result<T, TE> result, Func<T, Result> func) where TE : Exception
        {
            if (result.IsFailure)
                return Result.Fail<TK, TE>(result.Error);

            return func(result.Value);
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            return func(result.Value);
        }

        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static Result<T, TE> Ensure<T, TE>(this Result<T, TE> result, Func<T, bool> predicate, TE error)
            where TE : Exception
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T, TE>(error);

            return result;
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return result;
        }

        public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.Fail(errorMessage);

            return result;
        }

        public static Result<TK, TE> Map<T, TK, TE>(
            this Result<T, TE> result,
            Func<T, TK> onSuccess,
            Func<TE, TK> onFailure) where TE : Exception
        {
            var (isSuccess, _, value, error) = result;
            return isSuccess ? onSuccess(value) : onFailure(error);
        }



        public static Result<TK, TE> Map<T, TK, TE>(this Result<T, TE> result, Func<T, TK> func) where TE : Exception =>
            result.OnSuccess(func);

        public static Result<TK> Map<T, TK>(this Result<T> result, Func<T, TK> func) => result.OnSuccess(func);

        public static Result<T> Map<T>(this Result result, Func<T> func) => result.OnSuccess(func);

        public static Result<T, TE> OnSuccess<T, TE>(this Result<T, TE> result, Action<T> action) where TE : Exception
        {
            if (result.IsSuccess) action(result.Value);

            return result;
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess) action(result.Value);

            return result;
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess) action();

            return result;
        }

        public static Result OnSuccessTry(
            this Result result,
            Action action,
            Func<Exception, string> errorHandler = null) =>
            result.IsFailure ? result : Result.Try(action, errorHandler);

        public static Result<T> OnSuccessTry<T>(
            this Result result,
            Func<T> func,
            Func<Exception, string> errorHandler = null) =>
            result.IsFailure ? Result.Fail<T>(result.Error) : Result.Try(func, errorHandler);

        public static Result OnSuccessTry<T>(
            this Result<T> result,
            Action<T> action,
            Func<Exception, string> errorHandler = null)
        {
            return result.IsFailure ? Result.Fail(result.Error) : Result.Try(() => action(result.Value), errorHandler);
        }

        public static Result<TK> OnSuccessTry<T, TK>(
            this Result<T> result,
            Func<T, TK> func,
            Func<Exception, string> errorHandler = null)
        {
            return result.IsFailure ? Result.Fail<TK>(result.Error) : Result.Try(() => func(result.Value), errorHandler);
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func) => func(result);

        public static TK OnBoth<T, TK>(this Result<T> result, Func<Result<T>, TK> func) => func(result);

        public static TK OnBoth<T, TK, TE>(this Result<T, TE> result, Func<Result<T, TE>, TK> func) where TE : Exception => func(result);

        public static T OnBoth<T, TE>(this Result<T, TE> result, Func<Result<T, TE>, T> func) where TE : Exception =>
            func(result);

        public static Result<T, TE> OnFailure<T, TE>(this Result<T, TE> result, Func<TE, T> compensator) where TE : Exception
        {
            if (result.IsFailure)
                return compensator(result.Error);

            return result;
        }

        public static Result<T, TE> OnFailure<T, TE>(this Result<T, TE> result, Func<TE, Result<T, TE>> action)
            where TE : Exception
        {
            if (result.IsFailure)
                return action(result.Error);

            return result;
        }

        public static Result<TK, TE> OnFailure<T, TE, TK>(this Result<T, TE> result, Func<TE, Result<TK, TE>> action)
            where TE : Exception where TK : class
        {
            if (result.IsFailure)
                return action(result.Error);

            return result.Value as TK ?? throw new InvalidCastException();
        }

        public static Result<TK, TE> OnFailure<T, TE, TK>(this Result<T, TE> result, Func<TE, TK> action)
            where TE : Exception where TK : class
        {
            if (result.IsFailure)
                return action(result.Error);

            return result.Value as TK ?? throw new InvalidCastException();
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsFailure) action(result.Error);

            return result;
        }

        public static Result OnFailure(this Result result, Action<string> action)
        {
            if (result.IsFailure)
                action(result.Error);

            return result;
        }

        public static Result<T, TE> OnFailureCompensate<T, TE>(this Result<T, TE> result, Func<Result<T, TE>> func)
            where TE : Exception
        {
            if (result.IsFailure)
                return func();

            return result;
        }

        public static Result<T> OnFailureCompensate<T>(this Result<T> result, Func<Result<T>> func)
        {
            if (result.IsFailure)
                return func();

            return result;
        }

        public static Result OnFailureCompensate(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return func();

            return result;
        }

        public static Result<T, TE> OnFailureCompensate<T, TE>(this Result<T, TE> result, Func<TE, Result<T, TE>> func)
            where TE : Exception
        {
            if (result.IsFailure)
                return func(result.Error);

            return result;
        }

        public static Result<T> OnFailureCompensate<T>(this Result<T> result, Func<string, Result<T>> func)
        {
            if (result.IsFailure)
                return func(result.Error);

            return result;
        }

        public static Result OnFailureCompensate(this Result result, Func<string, Result> func)
        {
            if (result.IsFailure)
                return func(result.Error);

            return result;
        }

        public static Result Combine(this IEnumerable<Result> results, string errorMessagesSeparator) =>
            Result.Combine(errorMessagesSeparator, results as Result[] ?? results.ToArray());

        public static Result Combine(this IEnumerable<Result> results) =>
            Result.Combine(results as Result[] ?? results.ToArray());

        public static Result<IEnumerable<T>> Combine<T>(
            this IEnumerable<Result<T>> results,
            string errorMessagesSeparator)
        {
            var data = results as Result<T>[] ?? results.ToArray();

            var result = Result.Combine(errorMessagesSeparator, data);

            return result.IsSuccess ? Result.Ok(data.Select(e => e.Value)) : Result.Fail<IEnumerable<T>>(result.Error);
        }

        public static Result<TK> Combine<T, TK>(
            this IEnumerable<Result<T>> results,
            Func<IEnumerable<T>, TK> composer,
            string errorMessageSeparator)
        {
            var result = results.Combine(errorMessageSeparator);

            return result.IsSuccess ? Result.Ok(composer(result.Value)) : Result.Fail<TK>(result.Error);
        }
    }
}
