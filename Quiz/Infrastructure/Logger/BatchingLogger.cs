using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logger
{
    public class BatchingLogger : ILogger
    {
        private readonly string category;
        private readonly BatchingLoggerProvider provider;

        public BatchingLogger(BatchingLoggerProvider loggerProvider, string categoryName)
        {
            provider = loggerProvider;
            category = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return provider.ScopeProvider?.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return provider.IsEnabled;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            Log(DateTimeOffset.Now, logLevel, eventId, state, exception, formatter);
        }

        public void Log<TState>(DateTimeOffset timestamp, LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var builder = new StringBuilder();
            builder.AppendLine($"*{System.Reflection.Assembly.GetEntryAssembly().GetName().Name}*");
            builder.Append(timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
            builder.Append(" *[");
            builder.Append(logLevel.ToString());
            builder.Append("]* ");
            builder.Append(category);

            var scopeProvider = provider.ScopeProvider;
            if (scopeProvider != null)
            {
                scopeProvider.ForEachScope((scope, stringBuilder) => stringBuilder.Append(" => ").Append(scope),
                    builder);

                builder.AppendLine(":");
            }
            else
            {
                builder.Append(": ");
            }

            builder.AppendLine(formatter(state, exception));

            if (exception != null) builder.AppendLine(exception.ToString());

            provider.AddMessage(timestamp, builder.ToString());
        }
    }
}