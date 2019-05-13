using Microsoft.Extensions.Logging;
using QuizBotCore.Database;
using QuizRequestService;

namespace QuizBotCore.Commands
{
    public class ServiceManager
    {
        public ILogger logger;
        public IUserRepository userRepository;
        public IQuizService quizService;

        public ServiceManager(IQuizService quizService, IUserRepository userRepository, ILogger logger)
        {
            this.quizService = quizService;
            this.userRepository = userRepository;
            this.logger = logger;
        }
    }
}