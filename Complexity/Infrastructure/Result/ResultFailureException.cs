using System;

namespace Infrastructure.Result
{
    public class ResultFailureException : Exception
    {
        internal ResultFailureException(string error) : base(ResultMessages.ValueIsInaccessibleForFailure)
        {
            Error = error;
        }

        public ResultFailureException() : base()
        {
        }

        public ResultFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public string Error { get; }
    }
}