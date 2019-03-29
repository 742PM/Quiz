using System.Threading.Tasks;

namespace Infrastructure.Result
{
    public static class AsyncMaybeExtensions
    {
        public static async Task<Result<T>> ToResult<T>(this Task<Maybe<T>> maybeTask, string errorMessage)
            where T : class
        {
            var maybe = await maybeTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return maybe.ToResult(errorMessage);
        }

        public static async Task<Result<T, E>> ToResult<T, E>(this Task<Maybe<T>> maybeTask, E error)
            where T : class where E : class
        {
            var maybe = await maybeTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return maybe.ToResult(error);
        }
    }
}
