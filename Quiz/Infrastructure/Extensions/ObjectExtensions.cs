using System;
using Infrastructure.Result;

namespace Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static Result<T, Exception> Ok<T>(this T obj) => obj;
    }
}
