using System;
using System.Collections.Generic;

namespace DataBase.Entities
{
    public class UserProgressEntity
    {
        public Guid CurrentTopicId { get; set; }
        public Guid CurrentLevelId { get; set; }

        public Guid UserId { get; set; }

        /// <summary>
        ///     Maps Topic Id to progress of the Topic
        /// </summary>
        public Dictionary<Guid, TopicProgressEntity> TopicsProgress { get; set; }

        public TaskInfoEntity CurrentTask { get; set; }
    }
}
