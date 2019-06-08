using Microsoft.Extensions.Logging;
using QuizBotCore.Database;
using QuizRequestService;

namespace QuizBotCore.Commands
{
    public class ServiceManager
    {
        public ILogger Logger { get; }
        public  MessageTextRepository Dialog { get; }
        public  IUserRepository UserRepository { get; }
        public IQuizService QuizService { get; }

        public ServiceManager(IQuizService quizService, IUserRepository userRepository, ILogger<ServiceManager> logger, MessageTextRepository dialog)
        {
            this.QuizService = quizService;
            this.UserRepository = userRepository;
            this.Logger = logger;
            this.Dialog = dialog;
        }
    }
}