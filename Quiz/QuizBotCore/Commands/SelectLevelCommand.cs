using System.Linq;
using System.Threading.Tasks;
using QuizBotCore.Database;
using QuizRequestService.DTO;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class SelectLevelCommand : ICommand
    {
        private readonly TopicDTO topicDto;


        public SelectLevelCommand(TopicDTO topicDto)
        {
            this.topicDto = topicDto;

        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var chatId = chat.Id;
            var user = serviceManager.UserRepository.FindByTelegramId(chatId);
            
            var allLevels = serviceManager.QuizService.GetLevels(topicDto.Id);
            var availableLevels = serviceManager.QuizService.GetAvailableLevels(user.Id, topicDto.Id).ToList();
            var closedLevels = allLevels.Select(x => x.Description).Except(availableLevels.Select(x => x.Description));
            
            var activeLevels = availableLevels.Select((e,index)=> $"/level{index} {e.Description}");
            var nonActiveLevels = closedLevels.Select(x => $"{serviceManager.Dialog.Messages.ClosedLevel} {x}");
            
            var activeLevelsMessage = string.Join("\n", activeLevels);
            var nonActiveLevelsMessage = string.Join('\n', nonActiveLevels);
            
            var message = $"{serviceManager.Dialog.Messages.LevelSelection}\n{activeLevelsMessage}\n{nonActiveLevelsMessage}";
            
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Back, StringCallbacks.Back)
                }
            });

            await client.SendTextMessageAsync(chatId, message, replyMarkup: keyboard);
        }
    }
}