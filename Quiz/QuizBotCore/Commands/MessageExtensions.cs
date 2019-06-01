using System;

namespace QuizBotCore.Commands
{
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