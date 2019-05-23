using QuizRequestService.DTO;

namespace QuizBotCore.Transitions
{
    public class ReportTransition : Transition
    {
        public readonly TopicDTO TopicDto;
        public readonly LevelDTO LevelDto;

        public ReportTransition(TopicDTO topicDto, LevelDTO levelDto)
        {
            TopicDto = topicDto;
            LevelDto = levelDto;
        }
    }
}