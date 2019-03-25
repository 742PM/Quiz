using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Info;
using DataBase;
using DataBase.DatabaseEntities;
using Domain;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        private static readonly Random Random = new Random();

        private readonly TaskGenerator[] generators =
        {
            new TestGenerator(1),
            new TestGenerator(2),
            new TestGenerator(2),
            new TestGenerator(3)
        };

        private Topic[] topics;
        private IApplicationApi application;
        private IApplicationApi applicationWithoutTopics;
        private IApplicationApi applicationWithoutGenerators;
        private IUserRepository userRepository;
        private Guid userId;

        [SetUp]
        public void SetUp()
        {
            topics = new[]
            {
                CreateTopic(1),
                CreateTopic(2),
                CreateTopic(3),
                CreateTopic(4)
            };

            userRepository = new TestUserRepository();
            userId = AddUserWithProgress();

            applicationWithoutTopics = new Application.Application(new Topic[0], userRepository);

            applicationWithoutGenerators = new Application.Application(
                new[] { new Topic(Guid.NewGuid(), "n", "d", new Level[0]) }, // change
                userRepository);

            application = new Application.Application(topics, userRepository);
        }

        [Test]
        public void GetTopicsInfo_ReturnsNoTopicsInfo_WhenNoTopicsInApplication()
        {
            applicationWithoutTopics.GetTopicsInfo().Should().BeEmpty();
        }

        [Test]
        public void GetTopicsInfo_ReturnsAllTopics()
        {
            //application
            //    .GetTopicsInfo()
            //    .Should()
            //    .BeEquivalentTo(topics.Select(topic => new TopicInfo(topic.Name)),
            //        options => options.Excluding(topic => topic.Id));
        }

        [Test]
        public void GetDifficulties_ThrowsArgumentException_WhenNoTopics()
        {
            Action action = () => applicationWithoutTopics.GetLevels(Guid.NewGuid());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetDifficulties_ThrowsArgumentException_WhenNoSuchTopicId()
        {
            Action action = () => application.GetLevels(Guid.NewGuid());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetDifficulties_ReturnsNoDifficulties_WhenTopicIsEmpty()
        {
            var id = applicationWithoutGenerators.GetTopicsInfo().First().Id;
            applicationWithoutGenerators.GetLevels(id).Should().BeEmpty();
        }

        [Test]
        public void GetDifficulties_ReturnsAllDifficulties()
        {
            //application
            //    .GetLevels(topics.First().Id)
            //    .Should()
            //    .BeEquivalentTo(generators.Select(g => g.Difficulty).Distinct());
        }

        [Test]
        public void GetDifficulties_ReturnsDifficultiesInAscendingOrder()
        {
            application
                .GetLevels(topics.First().Id)
                .Should()
                .BeInAscendingOrder();
        }

        [Test]
        public void GetAvailableDifficulties_ThrowsArgumentException_WhenNoTopics()
        {
            Action action = () => applicationWithoutTopics.GetAvailableLevels(Guid.NewGuid(), Guid.NewGuid());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetAvailableDifficulties_ThrowsArgumentException_WhenNoSuchTopicId()
        {
            Action action = () => application.GetAvailableLevels(Guid.NewGuid(), Guid.NewGuid());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsNoDifficulties_WhenTopicIsEmpty()
        {
            var id = applicationWithoutGenerators.GetTopicsInfo().First().Id;
            applicationWithoutGenerators.GetAvailableLevels(Guid.NewGuid(), id).Should().BeEmpty();
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsOne_WhenUserIsNew()
        {
            var id = application.GetTopicsInfo().First().Id;
            application.GetAvailableLevels(Guid.NewGuid(), id).Count().Should().Be(1);
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsMin_WhenUserIsNew()
        {
            var id = application.GetTopicsInfo().First().Id;
            var minDifficulty = application.GetLevels(id).Min();
            application.GetAvailableLevels(Guid.NewGuid(), id).First().Should().Be(minDifficulty);
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsOne_WhenTopicIsNew()
        {
            var topicId = application.GetTopicsInfo().Last().Id;
            application.GetAvailableLevels(userId, topicId).Count().Should().Be(1);
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsMin_WhenTopicIsNew()
        {
            var topicId = application.GetTopicsInfo().Last().Id;

            var minDifficulty = application.GetLevels(topicId).Min();
            application.GetAvailableLevels(userId, topicId).Last().Should().Be(minDifficulty);
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsAllStarted_WhenSomeDifficultiesAreStarted()
        {
            var topicId = application.GetTopicsInfo().Skip(2).First().Id;

            application.GetAvailableLevels(userId, topicId).Count().Should().Be(2);
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsStarted_WhenDifficultyIsStarted()
        {
            var topicId = application.GetTopicsInfo().Skip(2).First().Id;
            application.GetAvailableLevels(userId, topicId).Last().Should().Be(2);
        }

        [Test]
        public void GetAvailableDifficulties_ReturnsNew_WhenDifficultyIsFinished()
        {
            var topicId = application.GetTopicsInfo().Skip(1).First().Id;
            application.GetAvailableLevels(userId, topicId).Last().Should().Be(3);
        }

        [Test]
        public void GetDifficultyDescription_ThrowsArgumentException_WhenNoTopics()
        {
            //Action action = () => applicationWithoutTopics.GetDifficultyDescription(Guid.NewGuid(), int.MaxValue);
            //action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetDifficultyDescription_ThrowsArgumentException_WhenNoSuchTopicId()
        {
            //Action action = () => application.GetDifficultyDescription(Guid.NewGuid(), int.MaxValue);
            //action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetDifficultyDescription_ReturnsAllDescriptions()
        {
            //var topicId = application.GetTopicsInfo().First().Id;
            //application
            //    .GetDifficultyDescription(topicId, 2)
            //    .Should()
            //    .BeEquivalentTo(generators
            //        .Where(g => g.Difficulty == 2)
            //        .Select(g => g.Description));
        }

        [Test]
        public void GetCurrentProgress_ThrowsAccessDeniedException_WhenNewUser()
        {
            //Action action = () => applicationWithoutTopics.GetCurrentProgress(Guid.NewGuid());
            //action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetCurrentProgress_ThrowsAccessDeniedException_WhenNoCurrentTask()
        {
            //Action action = () => applicationWithoutTopics.GetCurrentProgress(userId);
            //action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetCurrentProgress_Returns100_WhenAllAreSolved()
        {
            //var user = userRepository.FindById(userId);
            //var task = GetTaskEntity(generators[0]);
            //user.Progress.CurrentTask = task;
            //user.Progress.CurrentTopicId = topics[0].Id;
            //userRepository.Update(user);
            //application.GetCurrentProgress(userId).Should().Be(100);
        }

        [Test]
        public void GetCurrentProgress_Returns0_WhenNoSolvedTasks()
        {
            //var user = userRepository.FindById(userId);
            //var task = GetTaskEntity(generators[0]);
            //user.Progress.CurrentTask = task;
            //user.Progress.CurrentTopicId = topics.Last().Id;
            //userRepository.Update(user);
            //application.GetCurrentProgress(userId).Should().Be(0);
        }

        [Test]
        public void GetCurrentProgress_Returns50_WhenHalfIsSolved()
        {
            //var user = userRepository.FindById(userId);
            //var task = GetTaskEntity(generators[1]);
            //user.Progress.CurrentTask = task;
            //user.Progress.CurrentTopicId = topics[2].Id;
            //userRepository.Update(user);
            //application.GetCurrentProgress(userId).Should().Be(50);
        }

        [Test]
        public void GetTask_ThrowsArgumentException_WhenNoTopics()
        {
            Action action = () => applicationWithoutTopics.GetTask(Guid.NewGuid(), Guid.NewGuid(), int.MaxValue);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetTask_ThrowsArgumentException_WhenNoSuchTopicId()
        {
            Action action = () => application.GetTask(Guid.NewGuid(), Guid.NewGuid(), int.MaxValue);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetTask_ThrowsAccessDeniedException_WhenDifficultyIsNotAvailable()
        {
            Action action = () => application.GetTask(Guid.NewGuid(), topics.First().Id, 2);
            action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetTask_ReturnsTaskWithMinDifficulty_WhenUserIsNew()
        {
            //var topic = topics.First();
            //application
            //    .GetTask(Guid.NewGuid(), topic.Id, 1)
            //    .Should()
            //    .BeEquivalentTo(topic
            //        .Generators
            //        .First()
            //        .GetTask(Random)
            //        .ToInfo());
        }

        [Test]
        public void GetTask_ReturnsTaskWithMinDifficulty_WhenTopicIsNew()
        {
            //var topic = topics.Last();
            //application
            //    .GetTask(userId, topic.Id, 1)
            //    .Should()
            //    .BeEquivalentTo(topic
            //        .Generators
            //        .First()
            //        .GetTask(Random)
            //        .ToInfo());
        }

        [Test]
        public void GetTask_ReturnsTask_WhenDifficultyIsAvailable()
        {
            //var topic = topics.First();
            //application
            //    .GetTask(userId, topic.Id, 1)
            //    .Should()
            //    .BeEquivalentTo(topic
            //        .Generators
            //        .First()
            //        .GetTask(Random)
            //        .ToInfo());
        }

        [Test]
        public void GetTask_SetsCurrentTask()
        {
            //var topic = topics.Last();
            //application.GetTask(userId, topic.Id, 1);
            //userRepository
            //    .FindById(userId)
            //    .Progress
            //    .CurrentTask
            //    .Should()
            //    .BeEquivalentTo(topic
            //        .Generators
            //        .First()
            //        .GetTask(Random)
            //        .ToInfo());
        }

        [Test]
        public void GetTask_SetsCurrentTopic()
        {
            var topic = topics.Last();
            application.GetTask(userId, topic.Id, 1);
            userRepository
                .FindById(userId)
                .ProgressEntity
                .CurrentTopicId
                .Should()
                .Be(topic.Id);
        }

        [Test]
        public void GetNextTask_ThrowsAccessDeniedException_WhenUserIsNew()
        {
            Action action = () => application.GetNextTask(Guid.NewGuid());
            action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetNextTask_ThrowsAccessDeniedException_WhenNoCurrentTask()
        {
            Action action = () => application.GetNextTask(userId);
            action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetNextTask_ReturnsTaskFromSameDifficulty()
        {
            var topic = topics[2];
            application.GetTask(userId, topic.Id, 2);
            application.GetNextTask(userId).Should().BeEquivalentTo(generators[2].GetTask(Random).ToInfo());
        }

        [Test]
        public void GetNextTask_ReturnsOtherTaskFromSameDifficulty()
        {
            var topic = topics[2];
            application.GetTask(userId, topic.Id, 2);
            application.GetNextTask(userId).Should().NotBe(generators[2].GetTask(Random).ToInfo());
        }

        [Test]
        public void GetNextTask_CyclicallyReturnsTaskFromSameDifficulty_WhenCurrentIsLast()
        {
            var topic = topics[2];
            var first = application.GetTask(userId, topic.Id, 2);
            application.GetNextTask(userId);
            application.GetNextTask(userId).Should().BeEquivalentTo(first);
        }

        [Test]
        public void GetSimilarTask_ThrowsAccessDeniedException_WhenUserIsNew()
        {
            //Action action = () => application.GetSimilarTask(Guid.NewGuid());
            //action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetSimilarTask_ThrowsAccessDeniedException_WhenNoCurrentTask()
        {
            //Action action = () => application.GetSimilarTask(userId);
            //action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetSimilarTask_ReturnsTaskFromSameGenerator()
        {
            //var topic = topics.First();
            //application.GetTask(userId, topic.Id, 2);
            //application.GetSimilarTask(userId).Should().BeEquivalentTo(generators[1].GetTask(Random).ToInfo());
        }

        [Test]
        public void CheckAnswer_ThrowsAccessDeniedException_WhenUserIsNew()
        {
            Action action = () => application.CheckAnswer(Guid.NewGuid(), "");
            action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void CheckAnswer_ThrowsAccessDeniedException_WhenNoCurrentTask()
        {
            Action action = () => application.CheckAnswer(userId, "");
            action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void CheckAnswer_ReturnsFalse_WhenAnswerIsNull()
        {
            var topic = topics.First();
            application.GetTask(userId, topic.Id, 1);
            application.CheckAnswer(userId, null).Should().BeFalse();
        }

        [Test]
        public void CheckAnswer_ReturnsFalse_WhenIncorrectAnswer()
        {
            var topic = topics.First();
            application.GetTask(userId, topic.Id, 1);
            application.CheckAnswer(userId, "").Should().BeFalse();
        }

        [Test]
        public void CheckAnswer_ReturnsTrue_WhenCorrectAnswer()
        {
            var topic = topics.First();
            application.GetTask(userId, topic.Id, 1);
            application.CheckAnswer(userId, generators.First().GetTask(Random).RightAnswer).Should().BeTrue();
        }

        [Test]
        public void GetHint_ThrowsAccessDeniedException_WhenUserIsNew()
        {
            Action action = () => application.GetHint(Guid.NewGuid());
            action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetHint_ThrowsAccessDeniedException_WhenNoCurrentTask()
        {
            Action action = () => application.GetHint(userId);
            action.Should().Throw<AccessDeniedException>();
        }

        [Test]
        public void GetHint_ReturnsFirstTask_WhenCalledFirstTime()
        {
            var topic = topics.First();
            application.GetTask(userId, topic.Id, 1);
            application.GetHint(userId).Should().Be(generators.First().GetTask(Random).Hints.First());
        }

        private Guid AddUserWithProgress()
        {
            var id = Guid.NewGuid();
            var startedTopics = new[]
            {
                GetTopicEntity(topics[0], generators.Select(GetTaskEntity).ToArray()),
                GetTopicEntity(topics[1], generators.Take(3).Select(GetTaskEntity).ToArray()),
                GetTopicEntity(topics[2], generators.Take(2).Select(GetTaskEntity).ToArray()),
                GetTopicEntity(topics[3], new TaskEntity[0])
            };
            var progress = new ProgressEntity { Topics = startedTopics };
            userRepository.Insert(new UserEntity(id, progress));
            return id;
        }

        private Topic CreateTopic(int number) => new Topic(Guid.NewGuid(), $"t{number}", $"d{number}", new Level[0]); // change

        private static TopicEntity GetTopicEntity(Topic topic, TaskEntity[] tasks)
        {
            return new TopicEntity { Name = topic.Name, Tasks = tasks, TopicId = topic.Id };
        }

        private static TaskEntity GetTaskEntity(TaskGenerator generator)
        {
            return generator.GetTask(Random).ToEntity(generator);
        }
    }
}  }
}