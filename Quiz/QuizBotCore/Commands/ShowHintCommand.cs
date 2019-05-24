using System;
using System.Linq;
using System.Threading.Tasks;
using QuizBotCore.Database;
using QuizBotCore.States;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class ShowHintCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);
            var hint = serviceManager.quizService.GetHint(user.Id);
            await client.SendTextMessageAsync(chat.Id, hint.HintText);
            if (!hint.HasNext)
                await EditReplyButtons(user, chat, client, serviceManager);
        }

        private async Task EditReplyButtons(UserEntity user, Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var messageId = user.MessageId;
            var state = user.CurrentState as TaskState;
            var task = serviceManager.quizService.GetTaskInfo(user.Id, state.TopicDto.Id, state.LevelDto.Id);
            var reportCallback = user.MessageId.CreateMessageReportCallback(state.TopicDto.Id, state.LevelDto.Id);
            var controlButtons = new[]
            {
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Report, reportCallback)

                
            };
            var answers = task.Answers.Select((e, index) => (letter: DialogMessages.Alphabet[index], answer: $"{e}"))
                .ToList();
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                answers.Select(x => InlineKeyboardButton
                    .WithCallbackData(x.letter.ToString(), x.answer)),
                controlButtons
            });
            await client.EditMessageReplyMarkupAsync(chat.Id, messageId, keyboard);
        }
    }
}