using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logger
{
    /// <summary>
    ///     Extensions for adding the <see cref="TelegramLoggerProvider" /> to the <see cref="ILoggingBuilder" />
    /// </summary>
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        ///     Adds a file logger named 'Telegram' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder" /> to use.</param>
        /// <param name="configure">Configure an instance of the <see cref="TelegramLoggerOptions" /> to set logging options</param>
        public static ILoggingBuilder AddTelegram(this ILoggingBuilder builder, Action<TelegramLoggerOptions> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));
            builder.AddTelegram();
            builder.Services.Configure(configure);
            return builder;
        }

        /// <summary>
        ///     Adds a file logger named 'Telegram' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder" /> to use.</param>
        public static ILoggingBuilder AddTelegram(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, TelegramLoggerProvider>();
            return builder;
        }
    }
}