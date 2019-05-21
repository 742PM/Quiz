using Microsoft.Extensions.Options;
using Telegram.Bot;
using System;

namespace QuizWebHookBot.Services
{
    public class BotService : IBotService
    {
        private const string TelegramTokenVariableName = "TELEGRAM_TOKEN";

        public BotService(IOptions<BotConfiguration> config)
        {
            Client = Environment.GetEnvironmentVariable(TelegramTokenVariableName)??new TelegramBotClient("854957716:AAHjU_wFLwX5Sz23CV-9qs-kSYAcVqewERU");
        }
        
        public TelegramBotClient Client { get; }
    }
}
