using System;
using System.Linq;
using Application.Repositories;
using Application.Repositories.Entities;
using Infrastructure;

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
                userId,
                id: Guid.NewGuid(),
                currentTask: null,
                topicsProgress: taskRepository
                    .GetTopics()
                    .SafeToDictionary(
                        topic => topic.Id,
                        topic => new TopicProgressEntity(
                            topicId: topic.Id,
                            id: Guid.NewGuid(),
                            levelProgressEntities: topic.Levels
                                .Take(1)
                                .SafeToDictionary(
                                    level => level.Id,
                                    level => level.ToProgressEntity()))));

            return userRepository.FindById(userId) ?? 
                   userRepository.Insert(
                       new UserEntity(
                           userId, 
                           progress, 
                           UserRightsEntity.CreatePlayerRights()));
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

        public static bool GeneratorExists(
            this ITaskRepository taskRepository,
            Guid topicId,
            Guid levelId,
            Guid generatorId)
        {
            return taskRepository.FindGenerator(topicId, levelId, generatorId) != null;
        }
    }
}