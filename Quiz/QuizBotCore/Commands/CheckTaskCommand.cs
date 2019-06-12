using System;
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
            var user = serviceManager.UserRepository.FindByTelegramId(chat.Id);
            var isCorrect = await serviceManager.QuizService.SendAnswer(user.Id, answer);
            if (isCorrect.HasValue)
            {
                await RemoveButtonsForPreviousTask(user, chat, client);
                if (isCorrect.Value)
                {
                    await client.SendTextMessageAsync(chat.Id, serviceManager.Dialog.Messages.CorrectAnswer);
                    await new ShowTaskCommand(topicDto, levelDto, true).ExecuteAsync(chat, client, serviceManager);
                }
                else
                {
                    await client.SendTextMessageAsync(chat.Id, serviceManager.Dialog.Messages.WrongAnswer);
                    await new ShowTaskCommand(topicDto, levelDto).ExecuteAsync(chat, client, serviceManager);
                }
            }
        }

        private async Task RemoveButtonsForPreviousTask(UserEntity user, Chat chat, TelegramBotClient client)
        {
            var topicId = Convert.ToBase64String(topicDto.Id.ToByteArray());
            var levelId = Convert.ToBase64String(levelDto.Id.ToByteArray());
            var reportCallback = $"{StringCallbacks.Report}\n{user.MessageId}\n{topicId}\n{levelId}";
            var reportButton = new[]
            {
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Report, reportCallback)
            };
            await client.EditMessageReplyMarkupAsync(chat.Id, user.MessageId, reportButton);
        }
    }
}