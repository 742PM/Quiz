using System;
using System.Collections.Generic;
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

        /// <summary>
        ///     Creates an instance of the <see cref="TelegramLoggerProvider" />
        /// </summary>
        /// <param name="options">The options object controlling the logger</param>
        public TelegramLoggerProvider(IOptionsMonitor<TelegramLoggerOptions> options) : base(options)
        {
            var loggerOptions = options.CurrentValue;
            chatId = loggerOptions.ChatId;
            Client = new TelegramBotClient(Environment.GetEnvironmentVariable(TelegramTokenVariableName));
        }

        public TelegramBotClient Client { get; }

        /// <inheritdoc />
        protected override async Task WriteMessagesAsync(
            IEnumerable<LogMessage> messages,
            CancellationToken cancellationToken)
        {
            foreach (var message in messages)
                await Client.SendTextMessageAsync(chatId, message.ToTelegramFormat(),
                    cancellationToken: cancellationToken);
        }
    }
}