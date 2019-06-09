using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Infrastructure.Logger
{
    /// <summary>
    ///     An <see cref="ILoggerProvider" /> that writes logs to a file
    /// </summary>
    [ProviderAlias("Telegram")]
    public class TelegramLoggerProvider : BatchingLoggerProvider
    {
        private const string TelegramTokenVariableName = "TELEGRAM_LOG_TOKEN";
        private readonly long chatId;
        private readonly PeriodicityOptions periodicity;

        /// <summary>
        ///     Creates an instance of the <see cref="TelegramLoggerProvider" />
        /// </summary>
        /// <param name="options">The options object controlling the logger</param>
        public TelegramLoggerProvider(IOptionsMonitor<TelegramLoggerOptions> options) : base(options)
        {
            var loggerOptions = options.CurrentValue;
            periodicity = loggerOptions.Periodicity;
            chatId = loggerOptions.ChatId;
            Client = new TelegramBotClient(Environment.GetEnvironmentVariable(TelegramTokenVariableName));
        }

        public TelegramBotClient Client { get; }

        /// <inheritdoc />
        protected override async Task WriteMessagesAsync(IEnumerable<LogMessage> messages,
            CancellationToken cancellationToken)
        {
            foreach (var group in messages.GroupBy(GetGrouping))
            {
                var fullName = GetFullName(group.Key);
                //await Client.SendTextMessageAsync(chatId, fullName, cancellationToken: cancellationToken);
                foreach (var item in group)
                    await Client.SendTextMessageAsync(chatId, item.Message, cancellationToken: cancellationToken);
            }
        }

        private string GetFullName((int Year, int Month, int Day, int Hour, int Minute) group)
        {
            switch (periodicity)
            {
                case PeriodicityOptions.Minutely:
                    return $"{group.Year:0000}{group.Month:00}{group.Day:00}{group.Hour:00}{group.Minute:00}";
                case PeriodicityOptions.Hourly:
                    return $"{group.Year:0000}{group.Month:00}{group.Day:00}{group.Hour:00}";
                case PeriodicityOptions.Daily:
                    return $"{group.Year:0000}{group.Month:00}{group.Day:00}";
                case PeriodicityOptions.Monthly:
                    return $"{group.Year:0000}{group.Month:00}";
                default:
                    throw new InvalidDataException("Invalid periodicity");
                    ;
            }
        }

        private (int Year, int Month, int Day, int Hour, int Minute) GetGrouping(LogMessage message)
        {
            return (message.Timestamp.Year, message.Timestamp.Month, message.Timestamp.Day, message.Timestamp.Hour,
                message.Timestamp.Minute);
        }
    }
}
