using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Database;
using QuizBotCore.ProgressBar;
using QuizRequestService;
using QuizRequestService.DTO;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    public class ShowTaskCommand : ICommand
    {
        private readonly TopicDTO topicDto;
        private readonly LevelDTO levelDto;
        private readonly bool isNext;

        public ShowTaskCommand(TopicDTO topicDto, LevelDTO levelDto, bool isNext = false)
        {
            this.topicDto = topicDto;
            this.levelDto = levelDto;
            this.isNext = isNext;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.UserRepository.FindByTelegramId(chat.Id);
            var task = await GetTask(user, chat, client, serviceManager);
            if (task != null)
            {
                var progress = serviceManager.QuizService.GetProgress(user.Id, topicDto.Id, levelDto.Id);
                var isLevelSolved = IsLevelSolved(progress);
                if (isLevelSolved)
                    await client.SendTextMessageAsync(chat.Id, serviceManager.Dialog.Messages.LevelCompleted);
                var message = await SendTask(task, progress, chat, user, client, serviceManager);
                var newUser = new UserEntity(user.CurrentState, user.TelegramId, user.Id, message.MessageId);
                serviceManager.UserRepository.Update(newUser);
            }
        }

        private async Task<TaskDTO> GetTask(UserEntity user, Chat chat, TelegramBotClient client,
            ServiceManager serviceManager)
        {
            TaskDTO task;
            if (!isNext)
            {
                task = serviceManager.QuizService.GetTaskInfo(user.Id, topicDto.Id, levelDto.Id);
            }
            else
            {
                task = serviceManager.QuizService.GetNextTaskInfo(user.Id);
                if (task == null)
                    await client.SendTextMessageAsync(chat.Id, serviceManager.Dialog.Messages.NextTaskNotAvailable);
            }

            return task;
        }

        private async Task<Message> SendTask(TaskDTO task, ProgressDTO userProgress, Chat chat, UserEntity user, TelegramBotClient client,
            ServiceManager serviceManager)
        {
            var progress = PrepareProgress(serviceManager.Logger, userProgress);
            var isLevelSolved = IsLevelSolved(userProgress);

            var answers = task.Answers.Select((e, index) => (letter: DialogMessages.Alphabet[index], answer: $"{e}"))
                .ToList();
            var answerBlock = PrepareAnswers(answers, serviceManager.Logger);

            var message = FormatMessage(task, progress, answerBlock, isLevelSolved, serviceManager);
            serviceManager.Logger.LogInformation($"messageToSend : {message}");

            var keyboard = PrepareButtons(user, task, serviceManager.Logger, answers);

            var taskMessage = await client.SendTextMessageAsync(chat.Id, message,
                ParseMode.Markdown);
            var reportCallback = taskMessage.MessageId.CreateMessageReportCallback(topicDto.Id, levelDto.Id);
            var reportButton = new[]
            {
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Report, reportCallback)
            };
            var keyboardWithReport = new InlineKeyboardMarkup(keyboard.InlineKeyboard.Append(reportButton));
            await client.EditMessageReplyMarkupAsync(chat.Id, taskMessage.MessageId, keyboardWithReport);
            return taskMessage;
        }

        private bool IsLevelSolved(ProgressDTO progress)
        {
            return progress.TasksCount == progress.TasksSolved;
        }

        private static string PrepareProgress(ILogger logger, ProgressDTO userProgress)
        {
            logger.LogInformation($"Progress: {userProgress.TasksSolved}:{userProgress.TasksCount}");
            var progressBar = new CircleProgressBar();
            var progress = progressBar.GenerateProgressBar(userProgress.TasksSolved, userProgress.TasksCount);
            return progress;
        }

        private static string PrepareAnswers(IEnumerable<(char letter, string answer)> answers, ILogger logger)
        {
            var answerBlock = string.Join('\n', answers.Select(x => $"*{x.letter}.* `{x.answer}`"));
            logger.LogInformation($"Answers: {answerBlock}");
            return answerBlock;
        }

        private InlineKeyboardMarkup PrepareButtons(UserEntity user, TaskDTO task, ILogger logger,
            IEnumerable<(char letter, string answer)> answers)
        {
            var controlButtons = new[]
            {
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
            };
            logger.LogInformation($"HasHints: {task.HasHints}");
            if (task.HasHints)
                controlButtons =
                    new[]
                    {
                        InlineKeyboardButton
                            .WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
                        InlineKeyboardButton
                            .WithCallbackData(ButtonNames.Hint, StringCallbacks.Hint)
                    };
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                answers.Select(x => InlineKeyboardButton
                    .WithCallbackData(x.answer, x.answer)),
                controlButtons
            });
            return keyboard;
        }

        private string FormatMessage(TaskDTO task, string progressBar, string answers, bool isSolved, ServiceManager serviceManager)
        {
            var topicName = $"{serviceManager.Dialog.Messages.TopicName} {topicDto.Name} \n";
            var levelName = $"{serviceManager.Dialog.Messages.LevelName} {levelDto.Description} \n";
            var progress = $"{serviceManager.Dialog.Messages.Progress} {progressBar}\n";
            var question = $"{task.Question}\n";

            if (isSolved)
                progress = $"{progress}\n{serviceManager.Dialog.Messages.LevelSolved}\n";

            var questionFormatted = "```csharp\n" +
                                    $"{task.Text}\n" +
                                    "```";

            return $"{topicName}" +
                   $"{levelName}" +
                   $"{progress}" +
                   $"{question}\n" +
                   $"{questionFormatted}\n" +
                   $"{answers}\n\n";
        }
    }
}