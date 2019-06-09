using System;

namespace Infrastructure.Logger
{
    public struct LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }

        public string ToTelegramFormat() => $"**{Timestamp.LocalDateTime}**\n\n{Message}";
    }
}