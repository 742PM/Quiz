using Telegram.Bot;
using System;
using Microsoft.Extensions.Logging;

namespace QuizWebHookBot.Services
{
    public class BotService : IBotService
    {
        private const string TelegramTokenVariableName = "TELEGRAM_TOKEN";

        public BotService(ILogger<BotService> logger)
        {
            var environmentVariable = Environment.GetEnvironmentVariable(TelegramTokenVariableName);
            logger.LogInformation($"Got token {environmentVariable}");
            Client = new TelegramBotClient(environmentVariable ?? "854957716:AAHjU_wFLwX5Sz23CV-9qs-kSYAcVqewERU");
        }
        
        public TelegramBotClient Client { get; }
    }
}
