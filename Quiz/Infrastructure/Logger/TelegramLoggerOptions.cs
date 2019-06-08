using System;

namespace Infrastructure.Logger
{
    public class TelegramLoggerOptions : BatchingLoggerOptions
    {
        public TelegramLoggerOptions(long chatId = -1001298429946,
            TimeSpan? flushPeriod = default,
            int batchSize = int.MaxValue,
            PeriodicityOptions periodicity = PeriodicityOptions.Daily,
            bool isEnabled = true,
            bool includeScopes = false,
            int backgroundQueueSize = 1000)
            : base(flushPeriod, batchSize, isEnabled,
                includeScopes, backgroundQueueSize)
        {
            ChatId = chatId;
            Periodicity = periodicity;
        }

        public TelegramLoggerOptions()
        {
        }

        public long ChatId { get; set; } = -1001298429946;

        /// <summary>
        ///     Gets or sets the periodicity for rolling over log files.
        /// </summary>
        public PeriodicityOptions Periodicity { get; } = PeriodicityOptions.Daily;
    }
}