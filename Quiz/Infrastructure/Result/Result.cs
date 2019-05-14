using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Result
{
    internal sealed class ResultCommonLogic : ResultCommonLogic<string>
    {
        private ResultCommonLogic(bool isFailure, string error) : base(isFailure, error)
        {
        }

        [DebuggerStepThrough]
        public static ResultCommonLogic Create(bool isFailure, string error)
        {
            if (isFailure)
            {
                if (string.IsNullOrEmpty(error))
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorMessageIsNotProvidedForFailure);
            }
            else
            {
                if (error != null)
                    throw new ArgumentException(ResultMessages.ErrorMessageIsProvidedForSuccess, nameof(error));
            }

            return new ResultCommonLogic(isFailure, error);
        }
    }

    public struct Result : IResult, ISerializable
    {
        private static readonly Result OkResult = new Result(false, null);

        public static string ErrorMessagesSeparator = ", ";
        public static bool DefaultConfigureAwait = false;

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _logic.GetObjectData(info, context);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic _logic;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public string Error => _logic.Error;

        [DebuggerStepThrough]
        private Result(bool isFailure, string error)
        {
            _logic = ResultCommonLogic.Create(isFailure, error);
        }

        [DebuggerStepThrough]
        public static Result Ok() => OkResult;

        [DebuggerStepThrough]
        public static Result Fail(string error) => new Result(true, error);

        [DebuggerStepThrough]
        public static Result Create(bool isSuccess, string error) => isSuccess ? Ok() : Fail(error);

        public static Result Create(Func<bool> predicate, string error) => Create(predicate(), error);

        public static async Task<Result> Create(Func<Task<bool>> predicate, string error)
        {
            var isSuccess = await predicate()
                                .ConfigureAwait(DefaultConfigureAwait);
            return Create(isSuccess, error);
        }

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value) => new Result<T>(false, value, null);

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error) => new Result<T>(true, default, error);

        public static Result<T> Create<T>(bool isSuccess, T value, string error) =>
            isSuccess ? Ok(value) : Fail<T>(error);

        public static Result<T> Create<T>(Func<bool> predicate, T value, string error) =>
            Create(predicate(), value, error);

        public static async Task<Result<T>> Create<T>(Func<Task<bool>> predicate, T value, string error)
        {
            var isSuccess = await predicate()
                                .ConfigureAwait(DefaultConfigureAwait);
            return Create(isSuccess, value, error);
        }

        [DebuggerStepThrough]
        public static Result<T, E> Ok<T, E>(T value) where E:Exception=> new Result<T, E>(false, value, default) ;

        [DebuggerStepThrough]
        public static Result<T, E> Fail<T, E>(E error) where E : Exception => new Result<T, E>(true, default, error);

        [DebuggerStepThrough]
        public static Result<T, E> Create<T, E>(bool isSuccess, T value, E error) where E : Exception =>
            isSuccess ? Ok<T, E>(value) : Fail<T, E>(error);

        public static Result<T, E> Create<T, E>(Func<bool> predicate, T value, E error) where E : Exception =>
            predicate() ? Ok<T, E>(value) : Fail<T, E>(error);

        public static async Task<Result<T, E>> Create<T, E>(Func<Task<bool>> predicate, T value, E error) where E : Exception
        {
            var isSuccess = await predicate()
                                .ConfigureAwait(DefaultConfigureAwait);
            return isSuccess ? Ok<T, E>(value) : Fail<T, E>(error);
        }

        /// <summary>
        ///     Returns first failure in the list of <paramref name="results" />. If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            foreach (var result in results)
                if (result.IsFailure)
                    return Fail(result.Error);

            return Ok();
        }

        /// <summary>
        ///     Returns failure which combined from all failures in the <paramref name="results" /> list. Error messages are
        ///     separated by <paramref name="errorMessagesSeparator" />.
        ///     If there is no failure returns success.
        /// </summary>
        /// <param name="errorMessagesSeparator">Separator for error messages.</param>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result Combine(string errorMessagesSeparator, params Result[] results)
        {
            var failedResults = results.Where(x => x.IsFailure)
                                       .ToList();

            if (failedResults.Count == 0)
                return Ok();

            var errorMessage = string.Join(errorMessagesSeparator, failedResults.Select(x => x.Error)
                                                                                .ToArray());
            return Fail(errorMessage);
        }

        [DebuggerStepThrough]
        public static Result Combine(params Result[] results) => Combine(ErrorMessagesSeparator, results);

        [DebuggerStepThrough]
        public static Result Combine<T>(params Result<T>[] results) => Combine(ErrorMessagesSeparator, results);

        [DebuggerStepThrough]
        public static Result Combine<T>(string errorMessagesSeparator, params Result<T>[] results)
        {
            var untyped = results.Select(result => (Result) result)
                                 .ToArray();
            return Combine(errorMessagesSeparator, untyped);
        }

        private static readonly Func<Exception, string> DefaultTryErrorHandler = exc => exc.Message;

        public static Result Try(Action action, Func<Exception, string> errorHandler = null)
        {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try
            {
                action();
                return Ok();
            }
            catch (Exception exc)
            {
                var message = errorHandler(exc);
                return Fail(message);
            }
        }

        public static Result<T> Try<T>(Func<T> func, Func<Exception, string> errorHandler = null)
        {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try
            {
                return Ok(func());
            }
            catch (Exception exc)
            {
                var message = errorHandler(exc);
                return Fail<T>(message);
            }
        }

        public static async Task<Result<T>> Try<T>(Func<Task<T>> func, Func<Exception, string> errorHandler = null)
        {
            errorHandler = errorHandler ?? DefaultTryErrorHandler;

            try
            {
                var result = await func()
                                 .ConfigureAwait(DefaultConfigureAwait);
                return Ok(result);
            }
            catch (Exception exc)
            {
                var message = errorHandler(exc);
                return Fail<T>(message);
            }
        }

        public static Result<T, E> Try<T, E>(Func<T> func, Func<Exception, E> errorHandler) where E :  Exception
        {
            try
            {
                return Ok<T, E>(func());
            }
            catch (Exception exc)
            {
                var error = errorHandler(exc);
                return Fail<T, E>(error);
            }
        }

        public static async Task<Result<T, E>> Try<T, E>(Func<Task<T>> func, Func<Exception, E> errorHandler)
            where E :  Exception
        {
            try
            {
                var result = await func()
                                 .ConfigureAwait(DefaultConfigureAwait);
                return Ok<T, E>(result);
            }
            catch (Exception exc)
            {
                var error = errorHandler(exc);
                return Fail<T, E>(error);
            }
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure, out string error)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
            error = IsFailure ? Error : null;
        }
    }

    public struct Result<T> : IResult, ISerializable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic _logic;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public string Error => _logic.Error;

        public static implicit operator Result<T>(T value) => Result.Ok(value);

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _logic.GetObjectData(info, context);

            if (IsSuccess) info.AddValue("Value", Value);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (!IsSuccess)
                    throw new ResultFailureException(Error);

                return _value;
            }
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error)
        {
            _logic = ResultCommonLogic.Create(isFailure, error);
            _value = value;
        }

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
                return Result.Ok();
            return Result.Fail(result.Error);
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure, out T value)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
            value = IsSuccess ? Value : default;
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure, out T value, out string error)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
            value = IsSuccess ? Value : default;
            error = IsFailure ? Error : null;
        }
    }

    public struct Result<T, TError
            > : IResult, ISerializable where TError
             : Exception
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic<TError
            > _logic;

        public bool IsFailure => _logic.IsFailure;
        public bool IsSuccess => _logic.IsSuccess;
        public TError
             Error => _logic.Error;
        public static implicit operator Result<T, TError
            >(T value) => Result.Ok<T, TError
            >(value);
        public static implicit operator Result<T, TError
            >(TError
             error) => Result.Fail<T, TError
            >(error);

        // public static implicit operator Result<T, E>(Result<T> value) => value.IsSuccess ? (Result<T, E>) value.Value : default(E);

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _logic.GetObjectData(info, context);

            if (IsSuccess) info.AddValue("Value", Value);
        }

        public Result<TOut,TError
            > Then<TOut>(Func<T,TOut> f)
        {
            if (IsSuccess)
                return f(Value);
            return Error;
        }
        public Result<TOut,Exception> Then<TOut>(Func<T, Result<TOut,Exception>> f)
        {
            if (IsSuccess)
                return f(Value);
            return Error;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (!IsSuccess)
                    throw new ResultFailureException<TError
            >(Error);

                return _value;
            }
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, TError
             error)
        {
            _logic = new ResultCommonLogic<TError
            >(isFailure, error);
            _value = value;
        }

        public static implicit operator Result(Result<T, TError
            > result)
        {
            if (result.IsSuccess)
                return Result.Ok();
            return Result.Fail(result.Error.ToString());
        }

        public static implicit operator Result<T>(Result<T, TError
            > result)
        {
            if (result.IsSuccess)
                return Result.Ok(result.Value);
            return Result.Fail<T>(result.Error.ToString());
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure, out T value)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
            value = IsSuccess ? Value : default;
        }

        public void Deconstruct(out bool isSuccess, out bool isFailure, out T value, out TError
             error)
        {
            isSuccess = IsSuccess;
            isFailure = IsFailure;
            value = IsSuccess ? Value : default;
            error = IsFailure ? Error : default;
        }

    }

    
}
