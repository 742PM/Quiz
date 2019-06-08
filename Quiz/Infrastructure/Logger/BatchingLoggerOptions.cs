using System;

namespace Infrastructure.Logger
{
    public class BatchingLoggerOptions
    {
        public BatchingLoggerOptions(TimeSpan? flushPeriod = default, int batchSize = int.MaxValue,
            bool isEnabled = true, bool includeScopes = false, int backgroundQueueSize = 1000)
        {
            FlushPeriod = flushPeriod ?? TimeSpan.FromSeconds(1);
            BatchSize = batchSize;
            IsEnabled = isEnabled;
            IncludeScopes = includeScopes;
            BackgroundQueueSize = backgroundQueueSize;
        }

        public BatchingLoggerOptions()
        {
        }

        /// <summary>
        ///     Gets or sets the period after which logs will be flushed to the store.
        /// </summary>
        public TimeSpan FlushPeriod { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        ///     Gets or sets the maximum size of the background log message queue or null for no limit.
        ///     After maximum queue size is reached log event sink would start blocking.
        ///     Defaults to <c>1000</c>.
        /// </summary>
        public int BackgroundQueueSize { get; set; } = 1000;

        /// <summary>
        ///     Gets or sets a maximum number of events to include in a single batch or null for no limit.
        /// </summary>
        /// Defaults to
        /// <c>null</c>
        public int BatchSize { get; set; } = int.MaxValue;

        /// <summary>
        ///     Gets or sets value indicating if logger accepts and queues writes.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether scopes should be included in the message.
        ///     Defaults to <c>false</c>.
        /// </summary>
        public bool IncludeScopes { get; set; }
    }
}