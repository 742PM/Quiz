using System;
using System.Collections.Generic;

namespace Application.Repositories.Entities
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
        public UserProgressEntity With(
            Guid? currentTopicId = default,
            Guid? currentLevelId = default,
            Guid? userId = default,
            Dictionary<Guid, TopicProgressEntity> topicsProgress = default,
            TaskInfoEntity currentTask = default) =>
            new UserProgressEntity(currentTopicId ?? CurrentTopicId, currentLevelId ?? CurrentLevelId, userId ?? UserId,
                                   topicsProgress ?? TopicsProgress, currentTask ?? CurrentTask);
    }
}
