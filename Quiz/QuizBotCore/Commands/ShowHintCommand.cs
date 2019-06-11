using System;
using System.Linq;
using System.Threading.Tasks;
using QuizBotCore.Database;
using QuizBotCore.States;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class ShowHintCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.UserRepository.FindByTelegramId(chat.Id);
            var hint = await serviceManager.QuizService.GetHint(user.Id);
            if (hint.HasNoValue)
                await new NoConnectionCommand().ExecuteAsync(chat, client, serviceManager);
            await client.SendTextMessageAsync(chat.Id, hint.Value.HintText, ParseMode.Markdown);
            if (!hint.Value.HasNext)
                await EditReplyButtons(user, chat, client, serviceManager);
        }

        private async Task EditReplyButtons(UserEntity user, Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var messageId = user.MessageId;
            var state = (TaskState)user.CurrentState;
            var reportCallback = user.MessageId.CreateMessageReportCallback(state.TopicDto.Id, state.LevelDto.Id);

            var controlButtons = new[]
            {
                InlineKeyboardButton    
                    .WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Report, reportCallback)
            };
            var answers = user.CurrentTask.Answers.Select((e, index) => (letter: DialogMessages.Alphabet[index], answer: $"{e}"))
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