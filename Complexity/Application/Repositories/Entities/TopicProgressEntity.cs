using System;
using System.Collections.Generic;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class TopicProgressEntity : Entity
    {
        public TopicProgressEntity(Dictionary<Guid, LevelProgressEntity> levelProgressEntities, Guid topicId, Guid id):base(id)
        {
            LevelProgressEntities = levelProgressEntities;
            TopicId = topicId;
        }

        public Dictionary<Guid, LevelProgressEntity> LevelProgressEntities { get;  }

        public Guid TopicId { get;  }
    }
}
