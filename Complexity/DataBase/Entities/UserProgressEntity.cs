using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DataBase.Entities
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    public class UserProgressEntity
    {
        public UserProgressEntity(
            Guid currentTopicId,
            Guid currentLevelId,
            Guid userId,
            Dictionary<Guid, TopicProgressEntity> topicsProgress,
            TaskInfoEntity currentTask)
        {
            CurrentTopicId = currentTopicId;
            CurrentLevelId = currentLevelId;
            UserId = userId;
            TopicsProgress = topicsProgress;
            CurrentTask = currentTask;
        }

        public Guid CurrentTopicId { get; private set; }
        public Guid CurrentLevelId { get; private set; }
        public Guid UserId { get; private set; }

        /// <summary>
        ///     Maps Topic Id to progress of the Topic
        /// </summary>
        public Dictionary<Guid, TopicProgressEntity> TopicsProgress { get; private set; }

        public TaskInfoEntity CurrentTask { get; private set; }

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
