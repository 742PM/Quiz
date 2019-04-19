namespace Infrastructure.Result
{
    public class ResultFailureException<E> : ResultFailureException
    {
        internal ResultFailureException(E error) : base(ResultMessages.ValueIsInaccessibleForFailure)
        {
            Error = error;
        }

        public ResultFailureException() : base()
        {
        }

        public ResultFailureException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public new E Error { get; }
    }
}
