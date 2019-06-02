using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class ReportTaskCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.UserRepository.FindByTelegramId(chat.Id);
            var cancelKey = new InlineKeyboardMarkup(
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Cancel, StringCallbacks.Cancel)
            );
            await client.SendTextMessageAsync(chat.Id, DialogMessages.ReportRequesting, replyMarkup: cancelKey);
        }
    }
}
