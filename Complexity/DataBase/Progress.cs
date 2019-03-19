namespace DataBase
{
    public class Progress
    {
        public int AccessLevel { get; set; }
        
        public int CurrentTopicId { get; set; }
        public TopicEntity[] TopicsEntityInfo { get; set; }
    }
}