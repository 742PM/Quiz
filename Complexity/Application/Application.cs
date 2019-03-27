using System;
using System.Collections.Generic;
using System.Linq;
using Application.Info;
using DataBase;
using DataBase.Entities;
using Domain.Entities.TaskGenerators;
using Domain.Values;
using Ninject;

namespace Application
{
    public class Application : IApplicationApi
    {
        //TODO: build asp.net, database etc.
        static Application()
        {
            var container = new StandardKernel();

            container.Bind<TaskGenerator>()
                .To<ExampleTaskGenerator>()
                .WithConstructorArgument("hint", "Wow"); //Какое-то странное что-то, просто пример
        }

        private readonly ITaskRepository taskRepository;
        private readonly IUserRepository userRepository;
        private readonly ITaskGeneratorSelector generatorSelector;
        private readonly Random random = new Random();

        public Application(
            IUserRepository userRepository,
            ITaskRepository taskRepository,
            ITaskGeneratorSelector generatorSelector)
        {
            this.userRepository = userRepository;
            this.taskRepository = taskRepository;
            this.generatorSelector = generatorSelector;
        }

        public IEnumerable<TopicInfo> GetTopicsInfo()
        {
            return taskRepository.GetTopics().Select(topic => topic.ToInfo());
        }

        public IEnumerable<LevelInfo> GetLevels(Guid topicId)
        {
            return taskRepository.GetLevelsFromTopic(topicId).Select(level => level.ToInfo());
        }

        public IEnumerable<LevelInfo> GetAvailableLevels(Guid userId, Guid topicId)
        {
            return FindOrInsertUser(userId)
                .UserProgressEntity
                .TopicsProgress[topicId]
                .LevelProgressEntities
                .Select(levelProgress =>
                    taskRepository
                        .FindLevel(topicId, levelProgress.Key)
                        .ToInfo());
        }

        public double GetCurrentProgress(Guid userId, Guid topicId, Guid levelId)
        {
            var user = FindOrInsertUser(userId);
            var solved = user
                .UserProgressEntity
                .TopicsProgress[topicId]
                .LevelProgressEntities[levelId]
                .CurrentLevelStreaks
                .Count(pair => pair.Value >= taskRepository.FindGenerator(topicId, levelId, pair.Key).Streak);
            return (double) solved / taskRepository.GetGeneratorsFromLevel(topicId, levelId).Length;
        }

        public TaskInfo GetTask(Guid userId, Guid topicId, Guid levelId)
        {
            var user = FindOrInsertUser(userId);
            if (!GetAvailableLevels(userId, topicId).Select(info => info.Id).Contains(levelId))
                throw new AccessDeniedException(
                    $"User {userId} doesn't have access to level {levelId} in topic {topicId}");
            var task = generatorSelector
                .Select(taskRepository.GetGeneratorsFromLevel(topicId, levelId))
                .GetTask(random);
            UpdateUserCurrentTask(user, topicId, levelId, task);
            return task.ToInfo();
        }

        public TaskInfo GetNextTask(Guid userId)
        {
            var user = FindOrInsertUser(userId);
            CheckCurrentTask(user);
            if (!user.UserProgressEntity.CurrentTask.IsSolved)
                throw new AccessDeniedException($"User {userId} should solve current task first");
            return GetTask(userId, user.UserProgressEntity.CurrentTopicId, user.UserProgressEntity.CurrentLevelId);
        }

        public bool CheckAnswer(Guid userId, string answer)
        {
            var user = FindOrInsertUser(userId);
            CheckCurrentTask(user);
            if (user.UserProgressEntity.CurrentTask.Answer != answer)
            {
                UpdateStreakIfNotSolved(user, streak => 0);
                return false;
            }
            user.UserProgressEntity.CurrentTask.IsSolved = true;
            UpdateStreakIfNotSolved(user, streak => streak + 1);
            //TODO: if everything solved add next level in progress
            userRepository.Update(user);
            return true;
        }

        public string GetHint(Guid userId)
        {
            var user = FindOrInsertUser(userId);
            CheckCurrentTask(user);
            var hints = user.UserProgressEntity.CurrentTask.Hints;
            var currentHintIndex = user.UserProgressEntity.CurrentTask.HintsTaken;
            if (currentHintIndex >= hints.Length)
                return null;
            user.UserProgressEntity.CurrentTask.HintsTaken++;
            userRepository.Update(user);
            return hints[currentHintIndex];
        }

        private void UpdateUserCurrentTask(UserEntity user, Guid topicId, Guid levelId, Task task)
        {
            user.UserProgressEntity.CurrentTopicId = topicId;
            user.UserProgressEntity.CurrentLevelId = levelId;
            user.UserProgressEntity.CurrentTask = new TaskInfoEntity
            {
                Question = task.Question,
                Answer = task.Answer,
                Hints = task.Hints,
                HintsTaken = 0,
                IsSolved = false,
                ParentGeneratorId = task.ParentGeneratorId
            };
            userRepository.Update(user);
        }

        private void UpdateStreakIfNotSolved(UserEntity user, Func<int, int> updateFunc)
        {
            var topicId = user.UserProgressEntity.CurrentTopicId;
            var levelId = user.UserProgressEntity.CurrentLevelId;
            var generatorId = user.UserProgressEntity.CurrentTask.ParentGeneratorId;
            var currentStreak = user
                .UserProgressEntity
                .TopicsProgress[topicId]
                .LevelProgressEntities[levelId]
                .CurrentLevelStreaks[generatorId];
            if (currentStreak < taskRepository.FindGenerator(topicId, levelId, generatorId).Streak)
            {
                user.UserProgressEntity
                    .TopicsProgress[topicId]
                    .LevelProgressEntities[levelId]
                    .CurrentLevelStreaks[generatorId] = updateFunc(currentStreak);
            }
        }

        private static void CheckCurrentTask(UserEntity user)
        {
            if (user.UserProgressEntity.CurrentTask is null)
                throw new AccessDeniedException($"User {user.Id} hadn't started any task");
        }

        private UserEntity FindOrInsertUser(Guid userId)
        {
            var progress = new UserProgressEntity
            {
                TopicsProgress = taskRepository
                    .GetTopics()
                    .ToDictionary(
                        topic => topic.Id,
                        topic => new TopicProgressEntity
                        {
                            TopicId = topic.Id,
                            //TODO: add only available levels
                            LevelProgressEntities = taskRepository
                                .GetLevelsFromTopic(topic.Id)
                                .ToDictionary(
                                    level => level.Id,
                                    level => new LevelProgressEntity
                                    {
                                        LevelId = level.Id,
                                        CurrentLevelStreaks = taskRepository
                                            .GetGeneratorsFromLevel(topic.Id, level.Id)
                                            .ToDictionary(
                                                generator => generator.Id,
                                                generator => 0)
                                    })
                        })
            };
            return userRepository.FindById(userId) ?? userRepository.Insert(new UserEntity(userId, progress));
        }
    }
}