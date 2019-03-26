using System;
using System.Collections.Generic;

namespace DataBase.DatabaseEntities
{
    public class TopicProgressEntity
    {
        public Dictionary<Guid, LevelProgressEntity> LevelProgressEntities { get; set; }

        public Guid TopicId { get; set; }

    }
}