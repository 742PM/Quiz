using System;

namespace Infrastructure.Result
{
    public class ResultSuccessException : Exception
    {
        internal ResultSuccessException() : base(ResultMessages.ErrorIsInaccessibleForSuccess)
        {
        }

        public ResultSuccessException(string message) : base(message)
        {
        }

        public ResultSuccessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}