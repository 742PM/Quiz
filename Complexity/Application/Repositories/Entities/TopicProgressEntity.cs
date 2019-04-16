using System;
using System.Collections.Generic;

namespace Application.Repositories.Entities
{
    public class TopicProgressEntity
    {
        public TopicProgressEntity(Guid topicId, Dictionary<Guid, LevelProgressEntity> levelProgressEntities)
        {
            TopicId = topicId;
            LevelProgressEntities = levelProgressEntities;
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public Dictionary<Guid, LevelProgressEntity> LevelProgressEntities { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public Guid TopicId { get; private set; }
    }
}
