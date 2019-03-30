using System;
using System.Linq;
using DataBase;
using DataBase.Entities;

namespace Application
{
    public static class DataBaseExtensions
    {
        public static UserEntity FindOrInsertUser(
            this IUserRepository userRepository,
            Guid userId,
            ITaskRepository taskRepository)
        {
            var progress = new UserProgressEntity(
                Guid.Empty,
                Guid.Empty,
                currentTask: null,
                userId: userId,
                topicsProgress: taskRepository
                    .GetTopics()
                    .ToDictionary(
                        topic => topic.Id,
                        topic => new TopicProgressEntity(
                            topic.Id,
                            topic.Levels
                                .Take(1)
                                .ToDictionary(
                                    level => level.Id,
                                    level => level.ToProgressEntity()))));

            return userRepository.FindById(userId) ?? userRepository.Insert(new UserEntity(userId, progress));
        }

        public static int GetCurrentStreak(
            this UserEntity user,
            Guid? topicId = null,
            Guid? levelId = null,
            Guid? generatorId = null)
        {
            var progress = user.UserProgressEntity;
            return progress
                .TopicsProgress[topicId ?? progress.CurrentTopicId]
                .LevelProgressEntities[levelId ?? progress.CurrentLevelId]
                .CurrentLevelStreaks[generatorId ?? progress.CurrentTask.ParentGeneratorId];
        }
    }
}