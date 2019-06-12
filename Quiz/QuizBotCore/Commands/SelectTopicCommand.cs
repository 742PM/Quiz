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
            var topics = await serviceManager.QuizService.GetTopics();
            if (topics.HasNoValue)
                await new NoConnectionCommand().ExecuteAsync(chat, client, serviceManager);
            var keyboard = new InlineKeyboardMarkup(
                topics
                    .Value.Select(x =>
                        new[]
                        {
                            InlineKeyboardButton
                                .WithCallbackData(x.Name, x.Id.ToString())
                        })
            );
            await client.SendTextMessageAsync(chatId, serviceManager.Dialog.Messages.Welcome, replyMarkup: keyboard);
        }
    }
}