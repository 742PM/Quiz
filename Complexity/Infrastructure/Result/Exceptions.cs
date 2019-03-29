using System;

namespace Infrastructure.Result
{
    public class ResultSuccessException : Exception
    {
        internal ResultSuccessException() : base(ResultMessages.ErrorIsInaccessibleForSuccess)
        {
        }
    }

    public class ResultFailureException : Exception
    {
        internal ResultFailureException(string error) : base(ResultMessages.ValueIsInaccessibleForFailure)
        {
            Error = error;
        }

        public string Error { get; }
    }

    public class ResultFailureException<E> : ResultFailureException
    {
        internal ResultFailureException(E error) : base(ResultMessages.ValueIsInaccessibleForFailure)
        {
            Error = error;
        }

        public new E Error { get; }
    }
}
