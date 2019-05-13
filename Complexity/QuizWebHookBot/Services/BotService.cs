using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace QuizWebHookBot.Services
{
    public class BotService : IBotService
    {
        private const string TelegramTokenVariableName = "TELEGRAM_TOKEN";

        public BotService(IOptions<BotConfiguration> config)
        {
            Client = new TelegramBotClient("854957716:AAHjU_wFLwX5Sz23CV-9qs-kSYAcVqewERU");
        }
        
        public TelegramBotClient Client { get; }
    }
}
