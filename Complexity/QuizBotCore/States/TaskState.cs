using MongoDB.Bson.Serialization.Attributes;
using QuizRequestService.DTO;

namespace QuizBotCore.States
{
    public class TaskState : State
    {
        [BsonConstructor]
        public TaskState(TopicDTO topicDto, LevelDTO levelDto)
        {
            TopicDto = topicDto;
            LevelDto = levelDto;
        }

        [BsonElement] public TopicDTO TopicDto { get; }

        [BsonElement] public LevelDTO LevelDto { get; }

    }
}
