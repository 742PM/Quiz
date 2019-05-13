using System.Threading.Tasks;
using QuizRequestService.DTO;
using Telegram.Bot;
using Telegram.Bot.Types;

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
                    await new ShowTaskCommand(topicDto,levelDto, true).ExecuteAsync(chat, client, serviceManager);
                }
                else await client.SendTextMessageAsync(chat.Id, DialogMessages.WrongAnswer);
            }
        }
    }
}