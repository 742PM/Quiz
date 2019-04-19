using System;

namespace Infrastructure.Result
{
    public class ResultFailureException : Exception
    {
        internal ResultFailureException(string error) : base(ResultMessages.ValueIsInaccessibleForFailure)
        {
            Error = error;
        }

        public string Error { get; }
    }
}