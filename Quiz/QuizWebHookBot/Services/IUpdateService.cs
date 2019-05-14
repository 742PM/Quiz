using QuizBotCore.Commands;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public interface IUpdateService
    {
        ICommand ProcessMessage(Update update);
    }
}
