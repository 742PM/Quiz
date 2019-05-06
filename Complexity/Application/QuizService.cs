using System;
using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.Extensions;
using Application.Info;
using Application.Repositories;
using Application.Repositories.Entities;
using Application.Selectors;
using Domain.Values;
using Infrastructure.Result;
using Microsoft.Extensions.Logging;

namespace Application
{
    public class QuizService : IQuizService
    {
        private readonly ITaskGeneratorSelector generatorSelector;
        private readonly Random random;

        private readonly ITaskRepository taskRepository;

        private readonly IUserRepository userRepository;

        public QuizService(
            IUserRepository userRepository,
            ITaskRepository taskRepository,
            ITaskGeneratorSelector generatorSelector,
            ILogger<QuizService> logger,
            Random random)
        {
            this.userRepository = userRepository;
            this.taskRepository = taskRepository;
            this.generatorSelector = generatorSelector;
            this.random = random;
            Logger = logger;
            this.random = random;
        }

        private ILogger<QuizService> Logger { get; }

        /// <inheritdoc />
        public Result<IEnumerable<TopicInfo>, Exception> GetTopicsInfo()
        {
            Logger.LogInformation("Showing topics;");
            return taskRepository.GetTopics()
                .Select(topic => topic.ToInfo())
                .LogInfo(ts => $"Found {ts.Count()} topics", Logger)
                .Ok();
        }

        /// <inheritdoc />
        public Result<IEnumerable<LevelInfo>, Exception> GetLevels(Guid topicId)
        {
            Logger.LogInformation($"Getting levels for topic {topicId}");
            if (TopicExists(topicId))
                return taskRepository
                    .GetLevelsFromTopic(topicId)
                    .Select(level => level.ToInfo())
                    .LogInfo(ts => $"Found {ts.Count()} levels", Logger)
                    .Ok();
            Logger.LogError($"Did not find any levels for {topicId}");
            return new ArgumentException(nameof(topicId));
        }

        /// <inheritdoc />
        public Result<IEnumerable<LevelInfo>, Exception> GetAvailableLevels(Guid userId, Guid topicId)
        {
            Logger.LogInformation($"Showing Available levels for User @ {userId} at Topic @ {topicId}");
            return !TopicExists(topicId)
                ? new ArgumentException(nameof(topicId))
                : userRepository
                    .FindOrInsertUser(userId, taskRepository)
                    .UserProgressEntity.TopicsProgress[topicId]
                    .LogInfo(s => $"Found TopicProgressEntity {s}", Logger)
                    .LevelProgressEntities
                    .Select(levelProgress => taskRepository.FindLevel(topicId, levelProgress.Key).ToInfo())
                    .LogInfo(s => $"Found {s.Count()} levels", Logger)
                    .Ok();
        }

        /// <inheritdoc />
        public Result<LevelProgressInfo, Exception> GetCurrentProgress(Guid userId, Guid topicId, Guid levelId)
        {
            if (!TopicExists(topicId))
                return new ArgumentException(nameof(topicId));
            if (!LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));

            var user = userRepository.FindOrInsertUser(userId, taskRepository);
            var streaks = user
                .UserProgressEntity
                .TopicsProgress[topicId]
                .LevelProgressEntities[levelId]
                .CurrentLevelStreaks;

            var solved = streaks.Count(pair => IsGeneratorSolved(user, topicId, levelId, pair.Key));
            var total = streaks.Sum(pair => taskRepository.FindGenerator(topicId, levelId, pair.Key).Streak);
            return new LevelProgressInfo(total, solved);
        }

        /// <inheritdoc />
        public Result<TaskInfo, Exception> GetTask(Guid userId, Guid topicId, Guid levelId)
        {
            if (!TopicExists(topicId))
                return new ArgumentException(nameof(topicId))
                    .LogError(_ =>
                            $"No topics exists for {(nameof(userId), userId, nameof(topicId), topicId, nameof(levelId), levelId)}",
                        Logger);
            if (!LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));

            var user = userRepository.FindOrInsertUser(userId, taskRepository);
            var levels = GetAvailableLevels(userId, topicId).Value;

            if (!levels.Select(info => info.Id).Contains(levelId))
                return new
                    AccessDeniedException($"User {userId} doesn't have access to level {levelId} in topic {topicId}");

            var streaks = user
                .UserProgressEntity
                .TopicsProgress[topicId]
                .LevelProgressEntities[levelId]
                .CurrentLevelStreaks;

            var (_, isFailure, generator, error) = generatorSelector
                .SelectGenerator(taskRepository.GetGeneratorsFromLevel(topicId, levelId), streaks);

            if (isFailure)
                return error;

            var task = generator.GetTask(random);
            UpdateUserCurrentTask(user, topicId, levelId, task);
            return task.ToInfo();
        }

        /// <inheritdoc />
        public Result<TaskInfo, Exception> GetNextTask(Guid userId)
        {
            var user = userRepository.FindOrInsertUser(userId, taskRepository);

            if (!CurrentTaskExists(user))
                return new AccessDeniedException($"User {userId} hadn't started any task");
            if (!user.UserProgressEntity.CurrentTask.IsSolved)
                return new AccessDeniedException($"User {userId} should solve current task first");

            return GetTask(userId, user.UserProgressEntity.CurrentTopicId, user.UserProgressEntity.CurrentLevelId);
        }

        /// <inheritdoc />
        public Result<bool, Exception> CheckAnswer(Guid userId, string answer)
        {
            var user = userRepository.FindOrInsertUser(userId, taskRepository);
            Logger.LogInformation($"Checking answer for User {user}: his answer is {answer}");
            if (!CurrentTaskExists(user))
                return new AccessDeniedException($"User {userId} hadn't started any task");

            var userUserProgress = user.UserProgressEntity;
            var currentTask = userUserProgress.CurrentTask;
            Logger.LogInformation($"User's current task is {currentTask}");
            if (currentTask.Answer != answer)
            {
                user = GetUserWithNewStreakIfNotSolved(user, _ => 0);
                userRepository.Update(user);
                return false;
            }

            user = user.With(
                userUserProgress.With(
                    currentTask: currentTask.With(isSolved: true)));
            user = GetUserWithNewStreakIfNotSolved(user, streak => streak + 1);
            user = GetUserWithNewProgressIfLevelSolved(user);
            userRepository.Update(user);
            return true;
        }

        /// <inheritdoc />
        public Result<HintInfo, Exception> GetHint(Guid userId)
        {
            var user = userRepository.FindOrInsertUser(userId, taskRepository);

            if (!CurrentTaskExists(user))
                return new AccessDeniedException($"User {userId} had not started any task");

            var userProgress = user.UserProgressEntity;
            var currentTask = userProgress.CurrentTask;
            var hints = currentTask.Hints;
            var currentHintIndex = currentTask.HintsTaken;

            if (currentHintIndex >= hints.Length)
                return new OutOfHintsException("Out of hints");

            user = user.With(userProgress.With(currentTask: currentTask.With(hintsTaken: currentTask.HintsTaken + 1)));
            userRepository.Update(user);
            return new HintInfo(hints[currentHintIndex], currentHintIndex < hints.Length - 1);
        }

        private UserEntity GetUserWithNewProgressIfLevelSolved(UserEntity user)
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
            user.UserProgressEntity.TopicsProgress[topicId].LevelProgressEntities[level.Id] = level.ToProgressEntity();
            return user;
        }

        private bool IsGeneratorSolved(UserEntity user, Guid topicId, Guid levelId, Guid generatorId)
        {
            var currentStreak = user.GetCurrentStreak(topicId, levelId, generatorId);
            var streakToSolve = taskRepository.FindGenerator(topicId, levelId, generatorId).Streak;
            return currentStreak >= streakToSolve;
        }

        private void UpdateUserCurrentTask(UserEntity user, Guid topicId, Guid levelId, Task task)
        {
            var taskInfoEntity = task.AsInfoEntity();
            var progress = user.UserProgressEntity.With(topicId, levelId, currentTask: taskInfoEntity);
            user = user.With(progress);
            userRepository.Update(user);
        }

        private UserEntity GetUserWithNewStreakIfNotSolved(UserEntity user, Func<int, int> updateFunc)
        {
            var topicId = user.UserProgressEntity.CurrentTopicId;
            var levelId = user.UserProgressEntity.CurrentLevelId;
            var generatorId = user.UserProgressEntity.CurrentTask.ParentGeneratorId;
            var currentStreak = user.GetCurrentStreak();
            if (!IsGeneratorSolved(user, topicId, levelId, generatorId))
                user.UserProgressEntity.TopicsProgress[topicId]
                    .LevelProgressEntities[levelId]
                    .CurrentLevelStreaks[generatorId] = updateFunc(currentStreak);
            return user;
        }

        private bool TopicExists(Guid topicId) => taskRepository.FindTopic(topicId) != null;

        private bool LevelExists(Guid topicId, Guid levelId) => taskRepository.FindLevel(topicId, levelId) != null;

        private static bool CurrentTaskExists(UserEntity user) => user.UserProgressEntity.CurrentTask != null;
    }
}