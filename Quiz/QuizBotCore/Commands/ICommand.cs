using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager);
    }

    public static class MessageExtensions
    {
        public static string CreateMessageReportCallback(this int messageId, Guid topicId, Guid levelId)
        {
            var topicGuidString = Convert.ToBase64String(topicId.ToByteArray());
            var levelGuidString = Convert.ToBase64String(levelId.ToByteArray());
            return $"{StringCallbacks.Report}\n{messageId}\n{topicGuidString}\n{levelGuidString}";
        }

    }
}
