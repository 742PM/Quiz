using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Infrastructure.Result
{
    internal class ResultCommonLogic<TError>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TError error;

        [DebuggerStepThrough]
        internal ResultCommonLogic(bool isFailure, TError error)
        {
            if (isFailure)
            {
                if (error == null)
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorObjectIsNotProvidedForFailure);
            }
            else
            {
                if (error != null)
                    throw new ArgumentException(ResultMessages.ErrorObjectIsProvidedForSuccess, nameof(error));
            }

            IsFailure = isFailure;
            this.error = error;
        }

        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        public TError Error
        {
            [DebuggerStepThrough]
            get
            {
                if (IsSuccess)
                    throw new ResultSuccessException();

                return error;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsFailure", IsFailure);
            info.AddValue("IsSuccess", IsSuccess);
            if (IsFailure) info.AddValue("Error", Error);
        }
    }
}