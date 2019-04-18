using System;
using System.Collections.Generic;

namespace DataBase.Entities
{
    public class UserProgressEntity
    {
        public UserProgressEntity(Guid currentTopicId, Guid currentLevelId, Guid userId, Dictionary<Guid, TopicProgressEntity> topicsProgress, TaskInfoEntity currentTask)
        {
            CurrentTopicId = currentTopicId;
            CurrentLevelId = currentLevelId;
            UserId = userId;
            TopicsProgress = topicsProgress;
            CurrentTask = currentTask;
        }

        public Guid CurrentTopicId { get;  }
        public Guid CurrentLevelId { get;  }

        public Guid UserId { get;  }

        /// <summary>
        ///     Maps Topic Id to progress of the Topic
        /// </summary>
        public Dictionary<Guid, TopicProgressEntity> TopicsProgress { get;  }

        public TaskInfoEntity CurrentTask
        {
            get;

        }
    }
}
