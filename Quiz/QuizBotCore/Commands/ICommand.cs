using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager);
    }
}
