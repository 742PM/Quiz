using System;
using System.Linq;
using Application.Repositories;
using Application.Repositories.Entities;

namespace Application.Extensions
{
    public static class DataBaseExtensions
    {
        public static UserEntity FindOrInsertUser(
            this IUserRepository userRepository,
            Guid userId,
            ITaskRepository taskRepository)
        {
            var progress = new UserProgressEntity(
                Guid.NewGuid(),
                Guid.NewGuid(),
                currentTask: null,
                userId: userId,
                topicsProgress: taskRepository
                    .GetTopics()
                    .ToDictionary(
                        topic => topic.Id,
                        topic => new TopicProgressEntity(
                            topic.Levels
                                .Take(1)
                                .ToDictionary(
                                    level => level.Id,
                                    level => level.ToProgressEntity()), topic.Id, Guid.NewGuid())), id: Guid.NewGuid());

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

        public static bool TopicExists(this ITaskRepository taskRepository, Guid topicId) =>
            taskRepository.FindTopic(topicId) != null;

        public static bool LevelExists(this ITaskRepository taskRepository, Guid topicId, Guid levelId) =>
            taskRepository.FindLevel(topicId, levelId) != null;
    }
}