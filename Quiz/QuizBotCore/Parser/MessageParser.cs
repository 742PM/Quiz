using System;
using System.Linq;
using Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using QuizBotCore.States;
using QuizBotCore.Transitions;
using QuizRequestService;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizBotCore.Parser
{
    public class MessageParser : IMessageParser
    {
        public Transition Parse(State currentState, Update update, IQuizService quizService, ILogger logger)
        {
            if (update.Type == UpdateType.Message)
                switch (update.Message.Text)
                {
                    case UserCommands.Help:
                        return new HelpTransition();
                    case UserCommands.Feedback:
                        return new FeedbackTransition();
                }
            
            switch (currentState)
            {
                case UnknownUserState _:
                    return UnknownUserStateParser(update);
                case TopicSelectionState _:
                    return TopicSelectionStateParser(update);
                case LevelSelectionState state:
                    return LevelSelectionStateParser(state, update, quizService, logger);
                case TaskState _:
                    return TaskStateParser(update, quizService, logger);
                case ReportState _:
                    return ReportStateParser(update);
            }
            return new InvalidTransition();
        }

        private Transition ReportStateParser(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    return new ReplyReportTransition(update.Message.MessageId);
                }
                case UpdateType.CallbackQuery:
                {
                    var callbackData = update.CallbackQuery.Data;
                    if (callbackData == StringCallbacks.Cancel)
                        return new CancelTransition();
                    break;
                }
            }
            
            return new InvalidTransition();
        }

        private Transition TaskStateParser(Update update, IQuizService quizService, ILogger logger)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                {
                    var callbackData = update.CallbackQuery.Data;
                    switch (callbackData)
                    {
                        case StringCallbacks.Back:
                            return new BackTransition();
                        case StringCallbacks.Hint:
                            return new ShowHintTransition();
                        case var t when t.StartsWith(StringCallbacks.Report):
                            return HandleReportCallback(t, quizService, logger);
                        default:
                            return new CorrectTransition(callbackData);
                    }
                }

            }

            return new InvalidTransition();
        }

        private Transition HandleReportCallback(string callback, IQuizService quizService, ILogger logger)
        {
            var (_, message, topicId, levelId) = callback.Split('\n').ToArray();
            var messageId = int.Parse(message);
            logger.LogInformation($"topicId: {topicId}");
            logger.LogInformation($"levelId: {levelId}");
            var topidGuid = new Guid(Convert.FromBase64String(topicId));
            var levelGuid = new Guid(Convert.FromBase64String(levelId));
            logger.LogInformation($"topicId: {topidGuid.ToString()}");
            logger.LogInformation($"levelId: {levelGuid.ToString()}");
            var topicRequest = quizService.GetTopics().Result;
            if (topicRequest.HasValue)
            {
                var topicDto = topicRequest.Value.FirstOrDefault(x => x.Id == topidGuid);
                var levelRequest = quizService.GetLevels(topicDto.Id).Result;
                if (levelRequest.HasValue)
                {
                    var levelDto = levelRequest.Value.FirstOrDefault(x => x.Id == levelGuid);
                    return new ReportTransition(messageId, topicDto, levelDto);
                }
            }
            return new InvalidTransition();
        }

        private Transition LevelSelectionStateParser(LevelSelectionState state, Update update, 
            IQuizService quizService, ILogger logger)
        {
            logger.LogInformation($"Update Type: {update.Type}");
            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    logger.LogInformation($"Parsed message: {update.Message.Text}");
                    return ParseLevel(state, update, quizService, logger);
                }
                case UpdateType.CallbackQuery:
                {
                    var callbackData = update.CallbackQuery.Data;
                    logger.LogInformation($"Parsed callback: {callbackData}");
                    if (callbackData == StringCallbacks.Back)
                        return new BackTransition();
                    return new InvalidTransition();
                }
            }
            return new InvalidTransition();
        }

        private Transition ParseLevel(LevelSelectionState state, Update update, 
            IQuizService quizService, ILogger logger)
        {
            var message = update.Message.Text;
            if (message.Contains(UserCommands.Level))
            {
                var levelId = message.Replace(UserCommands.Level, "");
                if (int.TryParse(levelId, out var index))
                {
                    logger.LogInformation($"levelId: {index}");
                    var levelRequest = quizService.GetLevels(state.TopicDto.Id).Result;
                    if (levelRequest.HasValue)
                    {
                        var level = levelRequest.Value.ElementAt(index);
                        logger.LogInformation($"level: {level.Id}");
                        return new CorrectTransition(level.Id.ToString());
                    }
                }
                return new InvalidTransition();
            }
            return new InvalidTransition();
        }

        private Transition TopicSelectionStateParser(Update update)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackData = update.CallbackQuery.Data;
                if (callbackData == StringCallbacks.Back)
                    return new BackTransition();
                return new CorrectTransition(callbackData);
            }
            
            return new InvalidTransition();
        }

        private Transition UnknownUserStateParser(Update update)
        {
            return new BackTransition();
        }
    }
}
