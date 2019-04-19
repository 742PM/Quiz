namespace Infrastructure.Result
{
    public class ResultFailureException<E> : ResultFailureException
    {
        internal ResultFailureException(E error) : base(ResultMessages.ValueIsInaccessibleForFailure)
        {
            Error = error;
        }

        public new E Error { get; }
    }
}
