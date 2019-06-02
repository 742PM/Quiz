using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.States;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuizBotCore.Commands
{
    public class SendReportTaskCommand : ICommand
    {
        private readonly ReportState reportState;
        private readonly int messageToReportId;
        private readonly int reportMessageId;
        private const string ReportContact = "@quibblereport";

        public SendReportTaskCommand(ReportState reportState, int messageToReportId, int reportMessageId)
        {
            this.reportState = reportState;
            this.messageToReportId = messageToReportId;
            this.reportMessageId = reportMessageId;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            serviceManager.Logger.LogInformation($"Sending report from {chat.Id}");
            var user = serviceManager.UserRepository.FindByTelegramId(chat.Id);
            var reportUserInfo = $"Report message from: {chat.Id};\n" +
                                 $"UserId: {user.Id};\n" +
                                 $"TopicId: {reportState.TopicDto.Id}\n" +
                                 $"LevelId: {reportState.LevelDto.Id}";
            
            await client.SendTextMessageAsync(ReportContact, reportUserInfo);
            await client.ForwardMessageAsync(ReportContact, chat.Id, reportMessageId);
            await client.ForwardMessageAsync(ReportContact, chat.Id, messageToReportId);
            await client.SendTextMessageAsync(chat.Id, serviceManager.Dialog.Messages.ReportThanks);
            await new ShowTaskCommand(reportState.TopicDto, reportState.LevelDto).ExecuteAsync(chat, client, serviceManager);
        }
    }
}