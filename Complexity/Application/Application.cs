using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using Domain;
using Ninject;

namespace Application
{
    public class Application : IApplicationApi
    {
        //TODO: build asp.net, database etc.
        static Application()
        {
            var container = new StandardKernel();

            container.Bind<ITaskGenerator>()
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

        public IEnumerable<int> GetDifficulties(Guid topicId)
        {
            var topic = topics
                .FirstOrDefault(t => t.Id == topicId) ?? 
                throw new ArgumentException($"No topic with with id {topicId}");
            return topic
                .Generators
                .Select(generator => generator.Difficulty)
                .Distinct();
        }

        public IEnumerable<int> GetAvailableDifficulties(Guid userId, Guid topicId)
        {
            var user = FindOrInsertUser(userId);
            var difficulties = GetDifficulties(topicId);
            var maxStartedDifficulty = user
                .Progress
                .Topics
                .First(topic => topic.TopicId == topicId)
                .Tasks
                .Select(task => task.Difficulty)
                .Distinct()
                .Max();
            var difficultyStep = GetTopicProgress(userId, topicId, maxStartedDifficulty) == 100 ? 1 : 0;
            return difficulties.TakeWhile(difficulty => difficulty <= maxStartedDifficulty + difficultyStep);
        }

        public IEnumerable<string> GetDifficultyDescription(Guid topicId, int difficulty)
        {
            return topics
                .First(topic => topic.Id == topicId)
                .Generators
                .Select(generator => generator.Description);
        }

        public int GetCurrentProgress(Guid userId)
        {
            var user = FindOrInsertUser(userId);
            return GetTopicProgress(userId, user.Progress.CurrentTopicId, user.Progress.CurrentTask.Difficulty);
        }

        public TaskInfo GetTask(Guid userId, Guid topicId, int difficulty)
        {
            return topics
                .First(topic => topic.Id == topicId)
                .Generators
                .First(generator => generator.Difficulty == difficulty)
                .GetTask(random)
                .ToInfo();
        }

        public TaskInfo GetNextTask(Guid userId)
        {
            var user = FindOrInsertUser(userId);
            return topics
                .First(topic => topic.Id == user.Progress.CurrentTopicId)
                .Generators
                .SkipWhile(generator => generator.Id != user.Progress.CurrentTask.GeneratorId)
                .Skip(1)
                .First()
                .GetTask(random)
                .ToInfo();
        }

        public TaskInfo GetSimilarTask(Guid userId)
        {
            var user = FindOrInsertUser(userId);
            return topics
                .First(topic => topic.Id == user.Progress.CurrentTopicId)
                .Generators
                .First(generator => generator.Id == user.Progress.CurrentTask.GeneratorId)
                .GetTask(random)
                .ToInfo();
        }

        public bool CheckAnswer(Guid userId, string answer)
        {
            var user = FindOrInsertUser(userId);
            var expected = user.Progress.CurrentTask.RightAnswer;
            return expected == answer;
        }

        public string GetHint(Guid userId)
        {
            var user = FindOrInsertUser(userId);
            return user.Progress.CurrentTask.Hints.First();
        }

        private int GetTopicProgress(Guid userId, Guid topicId, int difficulty)
        {
            var user = FindOrInsertUser(userId);
            var solvedTasksCount = user
                .Progress
                .Topics
                .First(topic => topic.TopicId == topicId)
                .Tasks
                .Distinct(new TaskGeneratorIdEqualityComparer())
                .Count(task => task.Difficulty == difficulty);
            var allTasksCount = topics
                .First(topic => topic.Id == topicId)
                .Generators
                .Count(generator => generator.Difficulty == difficulty);
            return solvedTasksCount / allTasksCount * 100;
        }

        private UserEntity FindOrInsertUser(Guid userId)
        {
            return userRepository.FindById(userId) ?? userRepository.Insert(new UserEntity(userId));
        }

        private class TaskGeneratorIdEqualityComparer : IEqualityComparer<TaskEntity>
        {
            public bool Equals(TaskEntity x, TaskEntity y)
            {
                return y != null && x != null && x.GeneratorId == y.GeneratorId;
            }

            public int GetHashCode(TaskEntity obj)
            {
                return obj.GeneratorId.GetHashCode();
            }
        }
    }
}