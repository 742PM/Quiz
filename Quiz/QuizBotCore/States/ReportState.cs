using MongoDB.Bson.Serialization.Attributes;
using QuizRequestService.DTO;

namespace QuizBotCore.States
{
    public class ReportState : State
    {
        [BsonElement]
        public readonly int MessageId;
        
        [BsonElement]
        public readonly TopicDTO TopicDto;
        
        [BsonElement]
        public readonly LevelDTO LevelDto;
        
        [BsonConstructor]
        public ReportState(int messageId, TopicDTO topicDto, LevelDTO levelDto)
        {
            MessageId = messageId;
            TopicDto = topicDto;
            LevelDto = levelDto;
        }
    }
}