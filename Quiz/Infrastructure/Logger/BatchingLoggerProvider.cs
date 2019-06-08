using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Logger
{
    public abstract class BatchingLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly int? batchSize;
        private readonly List<LogMessage> currentBatch = new List<LogMessage>();
        private readonly TimeSpan interval;
        private readonly IDisposable optionsChangeToken;
        private readonly int? queueSize;
        private CancellationTokenSource cancellationTokenSource;

        private bool includeScopes;

        private BlockingCollection<LogMessage> messageQueue;
        private Task outputTask;
        private IExternalScopeProvider scopeProvider;

        protected BatchingLoggerProvider(IOptionsMonitor<BatchingLoggerOptions> options)
        {
            var loggerOptions = options.CurrentValue;
            if (loggerOptions.BatchSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(loggerOptions.BatchSize),
                    $"{nameof(loggerOptions.BatchSize)} must be a positive number.");
            if (loggerOptions.FlushPeriod <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(loggerOptions.FlushPeriod),
                    $"{nameof(loggerOptions.FlushPeriod)} must be longer than zero.");

            interval = loggerOptions.FlushPeriod;
            batchSize = loggerOptions.BatchSize;
            queueSize = loggerOptions.BackgroundQueueSize;

            optionsChangeToken = options.OnChange(UpdateOptions);
            UpdateOptions(options.CurrentValue);
        }

        internal IExternalScopeProvider ScopeProvider => includeScopes ? scopeProvider : null;

        public bool IsEnabled { get; private set; }

        public void Dispose()
        {
            optionsChangeToken?.Dispose();
            if (IsEnabled) Stop();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new BatchingLogger(this, categoryName);
        }

        void ISupportExternalScope.SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            this.scopeProvider = scopeProvider;
        }

        private void UpdateOptions(BatchingLoggerOptions options)
        {
            var oldIsEnabled = IsEnabled;
            IsEnabled = options.IsEnabled;
            includeScopes = options.IncludeScopes;

            if (oldIsEnabled != IsEnabled)
            {
                if (IsEnabled)
                    Start();
                else
                    Stop();
            }
        }

        protected abstract Task WriteMessagesAsync(IEnumerable<LogMessage> messages, CancellationToken token);

        private async Task ProcessLogQueue()
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                var limit = batchSize ?? int.MaxValue;

                while (limit > 0 && messageQueue.TryTake(out var message))
                {
                    currentBatch.Add(message);
                    limit--;
                }

                if (currentBatch.Count > 0)
                {
                    try
                    {
                        await WriteMessagesAsync(currentBatch, cancellationTokenSource.Token);
                    }
                    catch
                    {
                        // ignored
                    }

                    currentBatch.Clear();
                }

                await IntervalAsync(interval, cancellationTokenSource.Token);
            }
        }

        protected virtual Task IntervalAsync(TimeSpan interval, CancellationToken cancellationToken)
        {
            return Task.Delay(interval, cancellationToken);
        }

        internal void AddMessage(DateTimeOffset timestamp, string message)
        {
            if (!messageQueue.IsAddingCompleted)
                try
                {
                    messageQueue.Add(new LogMessage {Message = message, Timestamp = timestamp},
                        cancellationTokenSource.Token);
                }
                catch
                {
                    //cancellation token canceled or CompleteAdding called
                }
        }

        private void Start()
        {
            messageQueue = queueSize == null
                ? new BlockingCollection<LogMessage>(new ConcurrentQueue<LogMessage>())
                : new BlockingCollection<LogMessage>(new ConcurrentQueue<LogMessage>(), queueSize.Value);

            cancellationTokenSource = new CancellationTokenSource();
            outputTask = Task.Run(ProcessLogQueue);
        }

        private void Stop()
        {
            cancellationTokenSource.Cancel();
            messageQueue.CompleteAdding();

            try
            {
                outputTask.Wait(interval);
            }
            catch (TaskCanceledException)
            {
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 &&
                                                ex.InnerExceptions[0] is TaskCanceledException)
            {
            }
        }
    }
}