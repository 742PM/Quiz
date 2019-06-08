using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logger
{
    public class TelegramLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
        public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
    }
    class TelegramLogger : ILogger
    {
        private readonly string name;
        private readonly TelegramLoggerConfiguration config;

        public TelegramLogger(string name, TelegramLoggerConfiguration config)
        {
            this.name = name;
            this.config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = config.Color;
                Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {name} - {formatter(state, exception)}");
                Console.ForegroundColor = color;
            }
        }
    }
    public class ColoredConsoleLoggerProvider : ILoggerProvider
    {
        private readonly TelegramLoggerConfiguration config;
        private readonly ConcurrentDictionary<string, TelegramLogger> loggers = new ConcurrentDictionary<string, TelegramLogger>();

        public ColoredConsoleLoggerProvider(TelegramLoggerConfiguration config)
        {
            this.config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new TelegramLogger(name, config));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
