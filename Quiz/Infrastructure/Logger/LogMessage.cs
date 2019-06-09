using System;

namespace Infrastructure.Logger
{
    public struct LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }

        public string ToTelegramFormat() => $"__{Timestamp.LocalDateTime}__\n\n```{Message}```";
    }
}