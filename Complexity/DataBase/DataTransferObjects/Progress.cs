using System;

namespace DataBase
{
    public class Progress
    {
        public Guid CurrentTopicId { get; set; }
        public TaskEntity CurrentTask { get; set; }
        public TopicEntity[] Topics { get; set; }
    }
}