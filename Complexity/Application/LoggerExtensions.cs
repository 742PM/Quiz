using System;
using Microsoft.Extensions.Logging;

namespace Application
{
    public static class LoggerExtensions
    {
        public static T LogInfo<T, TLogged>(this T item, Func<T, string> message, ILogger<TLogged> logger)
        {
            logger.LogInformation(message(item));
            return item;
        }
        public static T LogError<T, TLogged>(this T item, Func<T, string> message, ILogger<TLogged> logger)
        {
            logger.LogError(message(item));
            return item;
        }
    }
}