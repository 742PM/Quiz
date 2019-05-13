using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    public class ShowHintCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);
            var hint = serviceManager.quizService.GetHint(user.Id);
            if (hint == null)
                await client.SendTextMessageAsync(chat.Id, DialogMessages.NoHints);
            else await client.SendTextMessageAsync(chat.Id, hint.HintText);
        }
    }
}