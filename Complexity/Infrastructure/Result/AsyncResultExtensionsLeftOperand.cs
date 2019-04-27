using System;
using System.Threading.Tasks;

namespace Infrastructure.Result
{
    /// <summary>
    ///     Extentions for async operations where the task appears in the left operand only
    /// </summary>
    public static class AsyncResultExtensionsLeftOperand
    {
        public static async Task<Result<K, E>> OnSuccess<T, K, E>(this Task<Result<T, E>> resultTask, Func<T, K> func)
            where E : Exception
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<K>> OnSuccess<T, K>(this Task<Result<T>> resultTask, Func<T, K> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<T>> OnSuccess<T>(this Task<Result> resultTask, Func<T> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<K, E>> OnSuccess<T, K, E>(
            this Task<Result<T, E>> resultTask,
            Func<T, Result<K, E>> func) where E : Exception
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<K>> OnSuccess<T, K>(this Task<Result<T>> resultTask, Func<T, Result<K>> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<T>> OnSuccess<T>(this Task<Result> resultTask, Func<Result<T>> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<K>> OnSuccess<T, K>(this Task<Result<T>> resultTask, Func<Result<K>> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<K, E>> OnSuccess<T, K, E>(
            this Task<Result<T, E>> resultTask,
            Func<Result<K, E>> func) where E : Exception
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result> OnSuccess<T>(this Task<Result<T>> resultTask, Func<T, Result> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result> OnSuccess(this Task<Result> resultTask, Func<Result> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(func);
        }

        public static async Task<Result<T>> Ensure<T>(
            this Task<Result<T>> resultTask,
            Func<T, bool> predicate,
            string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.Ensure(predicate, errorMessage);
        }

        public static async Task<Result<T, E>> Ensure<T, E>(
            this Task<Result<T, E>> resultTask,
            Func<T, bool> predicate,
            E error) where E : Exception
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.Ensure(predicate, error);
        }

        public static async Task<Result> Ensure(this Task<Result> resultTask, Func<bool> predicate, string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.Ensure(predicate, errorMessage);
        }

        public static Task<Result<K>> Map<T, K>(this Task<Result<T>> resultTask, Func<T, K> func) =>
            resultTask.OnSuccess(func);

        public static Task<Result<K, E>> Map<T, K, E>(this Task<Result<T, E>> resultTask, Func<T, K> func)
            where E : Exception =>
            resultTask.OnSuccess(func);

        public static Task<Result<T>> Map<T>(this Task<Result> resultTask, Func<T> func) => resultTask.OnSuccess(func);

        public static async Task<Result<T>> OnSuccess<T>(this Task<Result<T>> resultTask, Action<T> action)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(action);
        }

        public static async Task<Result> OnSuccess(this Task<Result> resultTask, Action action)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnSuccess(action);
        }

        public static async Task<T> OnBoth<T>(this Task<Result> resultTask, Func<Result, T> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnBoth(func);
        }

        public static async Task<K> OnBoth<T, K>(this Task<Result<T>> resultTask, Func<Result<T>, K> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnBoth(func);
        }

        public static async Task<K> OnBoth<T, K, E>(this Task<Result<T, E>> resultTask, Func<Result<T, E>, K> func) where E : Exception
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnBoth(func);
        }

//        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> resultTask, Action action) where E : Exception
//        {
//            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
//            return result.OnFailure(action);
//        }
//
//        public static async Task<Result> OnFailure(this Task<Result> resultTask, Action action) where E : Exception
//        {
//            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
//            return result.OnFailure(action);
//        }

        public static async Task<Result<T>> OnFailure<T>(this Task<Result<T>> resultTask, Action<string> action)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnFailure(action);
        }

//        public static async Task<Result<T, E>> OnFailure<T, E>(this Task<Result<T, E>> resultTask, Action<E> action)
//            where E : Exception
//        {
//            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
//            return result.OnFailure(action);
//        }

        public static async Task<Result> OnFailure(this Task<Result> resultTask, Action<string> action)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnFailure(action);
        }

        public static async Task<Result<T>> OnFailureCompensate<T>(
            this Task<Result<T>> resultTask,
            Func<Result<T>> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnFailureCompensate(func);
        }

        public static async Task<Result> OnFailureCompensate(this Task<Result> resultTask, Func<Result> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnFailureCompensate(func);
        }

        public static async Task<Result<T>> OnFailureCompensate<T>(
            this Task<Result<T>> resultTask,
            Func<string, Result<T>> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnFailureCompensate(func);
        }

        public static async Task<Result<T, E>> OnFailureCompensate<T, E>(
            this Task<Result<T, E>> resultTask,
            Func<E, Result<T, E>> func) where E : Exception
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnFailureCompensate(func);
        }

        public static async Task<Result> OnFailureCompensate(this Task<Result> resultTask, Func<string, Result> func)
        {
            var result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.OnFailureCompensate(func);
        }
    }
}
