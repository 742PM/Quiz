using System;

namespace DataBase.DatabaseEntities
{
    public class Progress
    {
        public Guid CurrentTopicId { get; set; }
        public LevelEntity CurrentLevel { get; set; }
        public TopicEntity[] Topics { get; set; }
    }
}