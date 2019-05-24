using QuizRequestService.DTO;

namespace QuizBotCore.Transitions
{
    public class ReportTransition : Transition
    {
        public readonly TopicDTO TopicDto;
        public readonly LevelDTO LevelDto;
        public int MessageId;

        public ReportTransition(int messageId, TopicDTO topicDto, LevelDTO levelDto)
        {
            MessageId = messageId;
            TopicDto = topicDto;
            LevelDto = levelDto;
        }
    }
}