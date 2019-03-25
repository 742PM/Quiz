using System;
using System.Collections.Generic;
using System.Linq;
using Application.Info;
using DataBase;
using Domain.Entities;
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

        private readonly IEnumerable<Topic> topics;
        private readonly IUserRepository userRepository;
        private readonly Random random = new Random();

        public Application(IEnumerable<Topic> topics, IUserRepository userRepository)
        {
            this.topics = topics;
            this.userRepository = userRepository;
        }

        public IEnumerable<TopicInfo> GetTopicsInfo()
        {
            return topics.Select(topic => topic.ToInfo());
        }

        public IEnumerable<LevelInfo> GetLevels(Guid topicId)
        {
            return new LevelInfo[0];
            //return GetTopic(topicId)
            //    .Generators
            //    .Select(generator => generator.Difficulty)
            //    .Distinct()
            //    .OrderBy(difficulty => difficulty);
        }

        public IEnumerable<LevelInfo> GetAvailableLevels(Guid userId, Guid topicId)
        {
            return new LevelInfo[0];
            //var user = FindOrInsertUser(userId);
            //var difficulties = GetLevels(topicId);
            //var startedDifficulties = user
            //    .Progress
            //    .Topics
            //    .First(topic => topic.TopicId == topicId)
            //    .Tasks
            //    .Select(task => task.Difficulty)
            //    .ToList();
            //if (startedDifficulties.Count == 0)
            //    return GetLevels(topicId).Take(1);
            //var maxStartedDifficulty = startedDifficulties.Max();
            //var difficultyStep = GetTopicProgress(userId, topicId, maxStartedDifficulty) == 100 ? 1 : 0;
            //return difficulties.TakeWhile(difficulty => difficulty <= maxStartedDifficulty + difficultyStep);
        }

        //public IEnumerable<string> GetDifficultyDescription(Guid topicId, int difficulty)
        //{
        //    return GetTopic(topicId)
        //        .Generators
        //        .Where(generator => generator.Difficulty == difficulty)
        //        .Select(generator => generator.Description);
        //}

        //public int GetCurrentProgress(Guid userId)
        //{
        //    var user = FindOrInsertUser(userId);
        //    CheckCurrentTask(user);
        //    return GetTopicProgress(userId, user.Progress.CurrentTopicId, user.Progress.CurrentTask.Difficulty);
        //}

        public TaskInfo GetTask(Guid userId, Guid topicId, Guid levelId)
        {
            return new TaskInfo("?", new[] { "a", "b", "c" });
            //var user = FindOrInsertUser(userId);
            //if (!GetAvailableLevels(userId, topicId).Contains(difficulty))
            //    throw new AccessDeniedException(
            //        $"User {userId} doesn't have access to difficulty {difficulty} in topic {topicId}");
            //var taskGenerator = GetTopic(topicId)
            //    .Generators
            //    .First(generator => generator.Difficulty == difficulty);
            //var task = taskGenerator.GetTask(random);
            //UpdateUserCurrentTask(user, topicId, task, taskGenerator);
            //return task.ToInfo();
        }

        public TaskInfo GetNextTask(Guid userId)
        {
            return new TaskInfo("?", new[] { "a", "b", "c" });
            //var user = FindOrInsertUser(userId);
            //CheckCurrentTask(user);
            //var generators = GetTopic(user.Progress.CurrentTopicId)
            //    .Generators
            //    .Where(generator => generator.Difficulty == user.Progress.CurrentTask.Difficulty)
            //    .ToArray();
            //var nextGenerators = generators
            //    .SkipWhile(generator => generator.Id != user.Progress.CurrentTask.GeneratorId)
            //    .ToList();
            //var nextGenerator = nextGenerators.Count > 1
            //    ? nextGenerators.Skip(1).First()
            //    : generators.First();
            //var task = nextGenerator.GetTask(random);
            //UpdateUserCurrentTask(user, user.Progress.CurrentTopicId, task, nextGenerator);
            //return task.ToInfo();
        }

        public bool CheckAnswer(Guid userId, string answer)
        {
            return false;
            //var user = FindOrInsertUser(userId);
            //CheckCurrentTask(user);
            //var expected = user.Progress.CurrentTask.RightAnswer;
            //return expected == answer;
        }

        public string GetHint(Guid userId)
        {
            return "";
            //var user = FindOrInsertUser(userId);
            //CheckCurrentTask(user);
            //return user.Progress.CurrentTask.Hints.First();
        }

        //private void UpdateUserCurrentTask(UserEntity user, Guid topicId, Task task, TaskGenerator generator)
        //{
        //    user.Progress.CurrentTask = task.ToEntity(generator);
        //    user.Progress.CurrentTopicId = topicId;
        //    userRepository.Update(user);
        //}

        //private static void CheckCurrentTask(UserEntity user)
        //{
        //    if (user.Progress.CurrentTask is null)
        //        throw new AccessDeniedException($"User {user.Id} hadn't started any task");
        //}

        //private Topic GetTopic(Guid topicId)
        //{
        //    return topics.FirstOrDefault(t => t.Id == topicId) ??
        //           throw new ArgumentException($"No topic with with id {topicId}");
        //}

        //private int GetTopicProgress(Guid userId, Guid topicId, int difficulty)
        //{
        //    var user = FindOrInsertUser(userId);
        //    var solvedTasksCount = user
        //        .Progress
        //        .Topics
        //        .First(topic => topic.TopicId == topicId)
        //        .Tasks
        //        .Distinct(new TaskGeneratorIdEqualityComparer())
        //        .Count(task => task.Difficulty == difficulty);
        //    var allTasksCount = GetTopic(topicId)
        //        .Generators
        //        .Count(generator => generator.Difficulty == difficulty);
        //    var progress = (double) solvedTasksCount / allTasksCount * 100;
        //    return (int) progress;
        //}

        //private UserEntity FindOrInsertUser(Guid userId)
        //{
        //    var progress = new Progress
        //    {
        //        Topics = topics.Select(topic => new TopicEntity
        //            {
        //                Name = topic.Name,
        //                TopicId = topic.Id,
        //                Tasks = new TaskEntity[0]
        //            })
        //            .ToArray()
        //    };
        //    return userRepository.FindById(userId) ?? userRepository.Insert(new UserEntity(userId, progress));
        //}

        //private class TaskGeneratorIdEqualityComparer : IEqualityComparer<TaskEntity>
        //{
        //    public bool Equals(TaskEntity x, TaskEntity y)
        //    {
        //        return y != null && x != null && x.GeneratorId == y.GeneratorId;
        //    }

        //    public int GetHashCode(TaskEntity obj)
        //    {
        //        return obj.GeneratorId.GetHashCode();
        //    }
        //}
    }
}  