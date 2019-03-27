using System;
using System.Collections.Generic;

namespace DataBase.Entities
{
    public class TopicProgressEntity
    {
        public Dictionary<Guid, LevelProgressEntity> LevelProgressEntities { get; set; }

        public Guid TopicId { get; set; }
    }
}
