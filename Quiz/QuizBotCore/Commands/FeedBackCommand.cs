using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class FeedBackCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var keyboard = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithUrl(
                    serviceManager.Dialog.Messages.FeedbackContact.Item1,
                    serviceManager.Dialog.Messages.FeedbackContact.Item2)
                );
            await client.SendTextMessageAsync(chat.Id, serviceManager.Dialog.Messages.FeedbackMessage, replyMarkup: keyboard);
        }
    }
}