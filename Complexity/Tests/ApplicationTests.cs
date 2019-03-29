using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Exceptions;
using DataBase;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using FluentAssertions;
using NUnit.Framework;
using Tests.Mocks;

namespace Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        private IApplicationApi application;
        private IUserRepository userRepository;
        private ITaskRepository taskRepository;
        private ITaskGeneratorSelector selector;

        [SetUp]
        public void SetUp()
        {
            userRepository = new TestUserRepository();
            taskRepository = new TestTaskRepository();
            selector = new TestTaskGeneratorSelector();
            application = new Application.Application(userRepository, taskRepository, selector);
        }

        //TODO: change to result

        [Test]
        public void GetTopicsInfo_ReturnsSuccess_WhenNoTopics()
        {
            application.GetTopicsInfo().IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GetLevels_ReturnsSuccess_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            application.GetLevels(id).IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GetAvailableLevels_ReturnsSuccess_WhenNoUsers()
        {
            var id = AddEmptyTopic();
            application.GetAvailableLevels(Guid.NewGuid(), id).IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GetAvailableLevels_ReturnsSuccess_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            application.GetAvailableLevels(Guid.NewGuid(), id).IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GetCurrentProgress_ReturnsSuccess_WhenNoUsers()
        {
            var (topicId, levelId) = AddTopicWithLevel();
            application.GetCurrentProgress(Guid.NewGuid(), topicId, levelId).IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GetTask_ReturnsSuccess_WhenNoUsers()
        {
            var (topicId, levelId) = AddTopicWithLevel();
            application.GetTask(Guid.NewGuid(), topicId, levelId).IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GetTask_ReturnsSuccess_WhenNoGenerators()
        {
            var (topicId, levelId) = AddTopicWithLevel();
            application.GetTask(Guid.NewGuid(), topicId, levelId).IsSuccess.Should().BeTrue();
        }

        [Test]
        public void GetLevels_ReturnsFailure_WhenNoTopics()
        {
            application.GetLevels(Guid.NewGuid()).IsFailure.Should().BeTrue();
        }

        [Test]
        public void GetAvailableLevels_ReturnsFailure_WhenNoTopics()
        {
            application.GetAvailableLevels(Guid.NewGuid(), Guid.NewGuid()).IsFailure.Should().BeTrue();
        }

        [Test]
        public void GetCurrentProgress_ReturnsFailure_WhenNoTopics()
        {
            application.GetCurrentProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()).IsFailure.Should().BeTrue();
        }

        [Test]
        public void GetCurrentProgress_ReturnsFailure_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            application.GetCurrentProgress(Guid.NewGuid(), id, Guid.NewGuid()).IsFailure.Should().BeTrue();
        }

        [Test]
        public void GetTask_ReturnsFailure_WhenNoTopics()
        {
            application.GetTask(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()).IsFailure.Should().BeTrue();
        }

        [Test]
        public void GetTask_ReturnsFailure_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            application.GetTask(Guid.NewGuid(), id, Guid.NewGuid()).IsFailure.Should().BeTrue();
        }

        [Test]
        public void GetTask_ReturnsFailure_WhenLevelNotAvailable()
        {
            var (topicId, _) = AddTopicWithLevel();
            var levelId = Guid.NewGuid();
            taskRepository.InsertLevel(topicId, CreateLevel(levelId));
            var user = userRepository.FindOrInsertUser(Guid.NewGuid(), taskRepository);
            application.GetTask(user.Id, topicId, levelId).IsFailure.Should().BeTrue();
        }

        [Test]
        public void GetTopicsInfo_ReturnsAllTopics()
        {
            var ids = new List<Guid>();
            for (var i = 0; i < 5; i++)
                ids.Add(AddEmptyTopic());
            application.GetTopicsInfo().Value.Select(t => t.Id).Should().BeEquivalentTo(ids);
        }

        [Test]
        public void GetLevels_ReturnsAllLevels()
        {
            var topicId = AddEmptyTopic();
            var ids = AddLevels(topicId);
            application.GetLevels(topicId).Value.Select(l => l.Id).Should().BeEquivalentTo(ids);
        }

        [Test]
        public void GetAvailableLevels_AddsNewUser_WhenNoUser()
        {
            var userId = Guid.NewGuid();
            var topicId = AddEmptyTopic();
            Assert.IsNull(userRepository.FindById(userId));
            application.GetAvailableLevels(userId, topicId);
            userRepository.FindById(userId).Should().NotBeNull();
        }

        [Test]
        public void GetAvailableLevels_ReturnsFirstLevel_WhenNoProgress()
        {
            var topicId = AddEmptyTopic();
            var ids = AddLevels(topicId);
            application.GetAvailableLevels(Guid.NewGuid(), topicId).Value.First().Id.Should().Be(ids.First());
        }

        private IEnumerable<Guid> AddLevels(Guid topicId)
        {
            var ids = new List<Guid>();
            for (var i = 0; i < 5; i++)
                ids.Add(AddEmptyLevel(topicId));
            return ids;
        }

        private (Guid topicId, Guid levelId) AddTopicWithLevel()
        {
            var topicId = Guid.NewGuid();
            var levelId = Guid.NewGuid();
            var level = CreateLevel(levelId, new[] { new TestGenerator(42) });
            taskRepository.InsertTopic(new Topic(topicId, "", "", new[] { level }));
            return (topicId, levelId);
        }

        private Guid AddEmptyTopic()
        {
            var id = Guid.NewGuid();
            taskRepository.InsertTopic(new Topic(id, "", "", new Level[0]));
            return id;
        }

        private Guid AddEmptyLevel(Guid topicId)
        {
            return taskRepository.InsertLevel(topicId, CreateLevel()).Id;
        }

        private static Level CreateLevel(Guid id, IEnumerable<TaskGenerator> generators, IEnumerable<Guid> nextLevels) => 
            new Level(id, $"{id}", generators.ToArray(), nextLevels.ToArray());

        private static Level CreateLevel(Guid id, IEnumerable<TaskGenerator> generators) =>
            CreateLevel(id, generators, new Guid[0]);

        private static Level CreateLevel(Guid id) => CreateLevel(id, new TaskGenerator[0]);

        private static Level CreateLevel() => CreateLevel(Guid.NewGuid());
    }
}