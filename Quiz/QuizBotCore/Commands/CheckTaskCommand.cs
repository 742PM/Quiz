using System.Threading.Tasks;
using QuizBotCore.Database;
using QuizRequestService.DTO;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class CheckTaskCommand : ICommand
    {
        private readonly TopicDTO topicDto;
        private readonly LevelDTO levelDto;
        private readonly string answer;

        public CheckTaskCommand(TopicDTO topicDto, LevelDTO levelDto, string answer)
        {
            this.topicDto = topicDto;
            this.levelDto = levelDto;
            this.answer = answer;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);
            var isCorrect = serviceManager.quizService.SendAnswer(user.Id, answer);
            if (isCorrect.HasValue)
            {
                if (isCorrect.Value)
                {
                    await client.SendTextMessageAsync(chat.Id, DialogMessages.CorrectAnswer);
                    await RemoveButtonsForPreviousTask(user, chat, client);
                    await new ShowTaskCommand(topicDto, levelDto, true).ExecuteAsync(chat, client, serviceManager);
                }
                else await client.SendTextMessageAsync(chat.Id, DialogMessages.WrongAnswer);
            }
        }

        private async Task RemoveButtonsForPreviousTask(UserEntity user, Chat chat, TelegramBotClient client)
        {
            var reportCallback = $"{StringCallbacks.Report}\n{topicDto.Id}\n{levelDto.Id}";
            var reportButton = new[]
            {
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Report, StringCallbacks.Report)
            };
            await client.EditMessageReplyMarkupAsync(chat.Id, user.MessageId, reportButton);
        }
    }
}