using System;
using System.Collections.Generic;

namespace DataBase.Entities
{
    public class TopicProgressEntity
    {
        public TopicProgressEntity(Dictionary<Guid, LevelProgressEntity> levelProgressEntities, Guid topicId)
        {
            LevelProgressEntities = levelProgressEntities;
            TopicId = topicId;
        }

        public Dictionary<Guid, LevelProgressEntity> LevelProgressEntities { get;  }

        public Guid TopicId { get;  }
    }
}
