using Telegram.Bot;

namespace QuizWebHookBot.Services
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}