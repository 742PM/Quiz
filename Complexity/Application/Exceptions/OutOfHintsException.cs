using System;

namespace Application.Exceptions
{
    public class OutOfHintsException : Exception {
        public OutOfHintsException() : base()
        {
        }

        public OutOfHintsException(string message) : base(message)
        {
        }

        public OutOfHintsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}