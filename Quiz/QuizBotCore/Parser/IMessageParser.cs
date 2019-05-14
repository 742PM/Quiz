using Microsoft.Extensions.Logging;
using QuizBotCore.States;
using QuizBotCore.Transitions;
using QuizRequestService;
using Telegram.Bot.Types;

namespace QuizBotCore.Parser
{
    public interface IMessageParser
    {
        Transition Parse(State currentState, Update update, IQuizService quizService, ILogger logger);
    }
}
