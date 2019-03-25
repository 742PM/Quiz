using System;

namespace DataBase.DatabaseEntities
{
    public class TopicEntity
    {
        public Guid TopicId { get; set; }

        public string Name { get; set; }
        public LevelEntity[] Tasks { get; set; }
    }
}
