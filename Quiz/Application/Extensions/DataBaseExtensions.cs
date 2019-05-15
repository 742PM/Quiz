using System;
using System.Collections.Generic;
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
                        topic => topic.ToProgressEntity()));

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

        public static bool GeneratorExists(
            this ITaskRepository taskRepository,
            Guid topicId,
            Guid levelId,
            Guid generatorId)
        {
            return taskRepository.FindGenerator(topicId, levelId, generatorId) != null;
        }

        public static TopicProgressEntity GetOrInsertTopicProgress(
            this UserEntity user,
            Guid topicId,
            ITaskRepository taskRepository)
        {
            var topicProgress = taskRepository.FindTopic(topicId).ToProgressEntity();

            user.UserProgressEntity.TopicsProgress.TryAdd(topicId, topicProgress);

            return user.UserProgressEntity.TopicsProgress[topicId];
        }

        public static UserEntity UpdateUserProgress(this UserEntity user, ITaskRepository taskRepository)
        {
            var topics = taskRepository.GetTopics();
            foreach (var topic in topics)
                user.UserProgressEntity.TopicsProgress.TryAdd(topic.Id, topic.ToProgressEntity());

            var ids = topics.Select(topic => topic.Id).ToHashSet();
            var progress = user
                .UserProgressEntity
                .TopicsProgress
                .TakeIfIn(ids);

            return user.With(user.UserProgressEntity.With(topicsProgress: progress));
        }

        public static TopicProgressEntity UpdateTopicProgress(
            this TopicProgressEntity topicProgress,
            ITaskRepository taskRepository)
        {
            var levels = taskRepository.GetLevelsFromTopic(topicProgress.TopicId);
            if (levels.Length == 0)
                return topicProgress.With(new Dictionary<Guid, LevelProgressEntity>());

            var firstLevel = levels[0];

            topicProgress
                .LevelProgressEntities
                .TryAdd(firstLevel.Id, firstLevel.ToProgressEntity());

            var ids = levels.Select(level => level.Id).ToHashSet();
            var progress = topicProgress
                .LevelProgressEntities
                .TakeIfIn(ids);

            return topicProgress.With(progress);
        }

        public static LevelProgressEntity UpdateLevelProgress(
            this LevelProgressEntity levelProgress,
            Guid topicId,
            ITaskRepository taskRepository)
        {
            var level = taskRepository.FindLevel(topicId, levelProgress.LevelId);
            if (level is null)
                return levelProgress;

            foreach (var generator in level.Generators)
                levelProgress.CurrentLevelStreaks.TryAdd(generator.Id, 0);

            var ids = level.Generators.Select(generator => generator.Id).ToHashSet();
            var streaks = levelProgress
                .CurrentLevelStreaks
                .TakeIfIn(ids);

            return levelProgress.With(streaks);
        }
    }
}