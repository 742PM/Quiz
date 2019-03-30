using System;
using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.Info;
using DataBase;
using DataBase.Entities;
using Domain.Values;
using Infrastructure.Result;

namespace Application
{
    public class Application : IApplicationApi
    {
        private readonly ITaskGeneratorSelector generatorSelector;
        private readonly Random random = new Random();

        private readonly ITaskRepository taskRepository;

        private readonly IUserRepository userRepository;

        //TODO: build asp.net, database etc.

        public Application(
            IUserRepository userRepository,
            ITaskRepository taskRepository,
            ITaskGeneratorSelector generatorSelector)
        {
            this.userRepository = userRepository;
            this.taskRepository = taskRepository;
            this.generatorSelector = generatorSelector;
        }

        /// <inheritdoc />
        public Result<IEnumerable<TopicInfo>, Exception> GetTopicsInfo()
        {
            return taskRepository.GetTopics()
                                 .Select(topic => topic.ToInfo())
                                 .Ok();
        }

        /// <inheritdoc />
        public Result<IEnumerable<LevelInfo>, Exception> GetLevels(Guid topicId)
        {
            return TopicExists(topicId)
                       ? taskRepository.GetLevelsFromTopic(topicId)
                                       .Select(level => level.ToInfo())
                                       .Ok()
                       : new ArgumentException(nameof(topicId));
        }

        /// <inheritdoc />
        public Result<IEnumerable<LevelInfo>, Exception> GetAvailableLevels(Guid userId, Guid topicId)
        {
            return !TopicExists(topicId)
                       ? new ArgumentException(nameof(topicId))
                       : userRepository.FindOrInsertUser(userId, taskRepository)
                                       .UserProgressEntity.TopicsProgress[topicId]
                                       .LevelProgressEntities.Select(levelProgress => taskRepository
                                                                                      .FindLevel(topicId,
                                                                                                 levelProgress.Key)
                                                                                      .ToInfo())
                                       .Ok();
        }

        /// <inheritdoc />
        public Result<double, Exception> GetCurrentProgress(Guid userId, Guid topicId, Guid levelId)
        {
            if (!TopicExists(topicId))
                return new ArgumentException(nameof(topicId));
            if (!LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));

            var user = userRepository.FindOrInsertUser(userId, taskRepository);
            var solved = user.UserProgressEntity.TopicsProgress[topicId]
                             .LevelProgressEntities[levelId]
                             .CurrentLevelStreaks.Count(pair => IsGeneratorSolved(user, topicId, levelId, pair.Key));
            return (double) solved /
                   taskRepository.GetGeneratorsFromLevel(topicId, levelId)
                                 .Length;
        }

        /// <inheritdoc />
        public Result<TaskInfo, Exception> GetTask(Guid userId, Guid topicId, Guid levelId)
        {
            if (!TopicExists(topicId))
                return new ArgumentException(nameof(topicId));
            if (!LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));

            var user = userRepository.FindOrInsertUser(userId, taskRepository);

            var levels = GetAvailableLevels(userId, topicId)
                .Value;
            if (!levels.Select(info => info.Id)
                       .Contains(levelId))
                return new
                    AccessDeniedException($"User {userId} doesn't have access to level {levelId} in topic {topicId}");
            var task = generatorSelector.Select(taskRepository.GetGeneratorsFromLevel(topicId, levelId))
                                        .GetTask(random);
            UpdateUserCurrentTask(user, topicId, levelId, task);
            return task.ToInfo();
        }

        /// <inheritdoc />
        public Result<TaskInfo, Exception> GetNextTask(Guid userId)
        {
            var user = userRepository.FindOrInsertUser(userId, taskRepository);

            if (!CheckCurrentTaskExists(user))
                return new AccessDeniedException($"User {userId} hadn't started any task");
            if (!user.UserProgressEntity.CurrentTask.IsSolved)
                return new AccessDeniedException($"User {userId} should solve current task first");

            return GetTask(userId, user.UserProgressEntity.CurrentTopicId, user.UserProgressEntity.CurrentLevelId);
        }

        /// <inheritdoc />
        public Result<bool, Exception> CheckAnswer(Guid userId, string answer)
        {
            var user = userRepository.FindOrInsertUser(userId, taskRepository);

            if (!CheckCurrentTaskExists(user))
                return new AccessDeniedException($"User {userId} hadn't started any task");

            var userUserProgress = user.UserProgressEntity;
            var currentTask = userUserProgress.CurrentTask;

            if (currentTask.Answer != answer)
            {
                UpdateStreakIfNotSolved(user, streak => 0);
                return false;
            }

            user = user.With(userUserProgress.With(currentTask: currentTask.With(isSolved: true)));

            user = UpdateStreakIfNotSolved(user, streak => streak + 1);
            user = UpdateProgressIfLevelSolved(user);
            userRepository.Update(user);
            return true;
        }

        /// <inheritdoc />
        public Result<string, Exception> GetHint(Guid userId)
        {
            var user = userRepository.FindOrInsertUser(userId, taskRepository);

            if (!CheckCurrentTaskExists(user))
                return new AccessDeniedException($"User {userId} had not started any task");

            var userProgress = user.UserProgressEntity;
            var currentTask = userProgress.CurrentTask;
            var hints = currentTask.Hints;

            var currentHintIndex = currentTask.HintsTaken;
            if (currentHintIndex >= hints.Length)
                return new Exception("Out of hints");

            user = user.With(userProgress.With(currentTask: currentTask.With(hintsTaken: currentTask.HintsTaken + 1)));
            userRepository.Update(user);
            return hints[currentHintIndex];
        }

        private UserEntity UpdateProgressIfLevelSolved(UserEntity user)
        {
            var topicId = user.UserProgressEntity.CurrentTopicId;
            var levelId = user.UserProgressEntity.CurrentLevelId;
            var allSolved = taskRepository.GetGeneratorsFromLevel(topicId, levelId)
                                          .All(generator => IsGeneratorSolved(user, topicId, levelId, generator.Id));
            if (!allSolved)
                return user;
            var level = taskRepository.GetLevelsFromTopic(topicId)
                                      .SkipWhile(l => l.Id != levelId)
                                      .Skip(1)
                                      .FirstOrDefault();
            if (level is null)
                return user;
            user.UserProgressEntity.TopicsProgress[topicId]
                .LevelProgressEntities[level.Id] = level.ToProgressEntity();
            return user;
        }

        private bool IsGeneratorSolved(UserEntity user, Guid topicId, Guid levelId, Guid generatorId)
        {
            var currentStreak = user.UserProgressEntity.TopicsProgress[topicId]
                                    .LevelProgressEntities[levelId]
                                    .CurrentLevelStreaks[generatorId];
            return currentStreak >=
                   taskRepository.FindGenerator(topicId, levelId, generatorId)
                                 .Streak;
        }

        private void UpdateUserCurrentTask(UserEntity user, Guid topicId, Guid levelId, Task task)
        {
            var taskInfoEntity = task.AsInfoEntity();
            var pge = user.UserProgressEntity.With(topicId, levelId, currentTask: taskInfoEntity);
            user = user.With(pge);
            userRepository.Update(user);
        }

        private UserEntity UpdateStreakIfNotSolved(UserEntity user, Func<int, int> updateFunc)
        {
            var topicId = user.UserProgressEntity.CurrentTopicId;
            var levelId = user.UserProgressEntity.CurrentLevelId;
            var generatorId = user.UserProgressEntity.CurrentTask.ParentGeneratorId;
            var currentStreak = user.UserProgressEntity.TopicsProgress[topicId]
                                    .LevelProgressEntities[levelId]
                                    .CurrentLevelStreaks[generatorId];
            if (!IsGeneratorSolved(user, topicId, levelId, generatorId))
                user.UserProgressEntity.TopicsProgress[topicId]
                    .LevelProgressEntities[levelId]
                    .CurrentLevelStreaks[generatorId] = updateFunc(currentStreak);
            return user;
        }

        private bool TopicExists(Guid topicId) => taskRepository.FindTopic(topicId) != null;

        private bool LevelExists(Guid topicId, Guid levelId) => taskRepository.FindLevel(topicId, levelId) != null;

        private static bool CheckCurrentTaskExists(UserEntity user) => user.UserProgressEntity.CurrentTask != null;
    }
}
