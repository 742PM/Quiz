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
                    DialogMessages.FeedbackContact.Item1, 
                    DialogMessages.FeedbackContact.Item2)
                );
            await client.SendTextMessageAsync(chat.Id, DialogMessages.FeedbackMessage, replyMarkup: keyboard);
        }
    }
}