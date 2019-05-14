namespace QuizBotCore.Transitions
{
    public class ReplyReportTransition : Transition
    {
        public readonly int MessageId;

        public ReplyReportTransition(int messageId)
        {
            MessageId = messageId;
        }
    }
}