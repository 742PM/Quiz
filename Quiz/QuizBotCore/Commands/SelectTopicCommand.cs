using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class SelectTopicCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var chatId = chat.Id;
            var topics = serviceManager.QuizService.GetTopics();
            var keyboard = new InlineKeyboardMarkup(
                topics
                    .Select(x =>
                        new[]
                        {
                            InlineKeyboardButton
                                .WithCallbackData(x.Name, x.Id.ToString())
                        })
            );
            await client.SendPhotoAsync(chat.Id, serviceManager.Dialog.Messages.RequestForRotateDeviceGif, serviceManager.Dialog.Messages.RequestForRotateDevice);
            await client.SendTextMessageAsync(chatId, serviceManager.Dialog.Messages.Welcome, replyMarkup: keyboard);
        }
    }
}