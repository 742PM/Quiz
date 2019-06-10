using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    class NoConnectionCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            await client.SendTextMessageAsync(chat.Id, serviceManager.Dialog.Messages.NoServiceConnection);
        }
    }
}