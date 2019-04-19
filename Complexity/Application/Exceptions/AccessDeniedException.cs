using System;

namespace Application.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) : base(message) { }

        public AccessDeniedException() : base()
        {
        }

        public AccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
