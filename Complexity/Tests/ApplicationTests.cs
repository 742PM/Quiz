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

        #region DoesNotThrowException

        [Test]
        public void GetTopicsInfo_DoesNotThrowException_WhenNoTopics()
        {
            Action action = () => application.GetTopicsInfo();
            action.Should().NotThrow();
        }

        [Test]
        public void GetLevels_DoesNotThrowException_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            Action action = () => application.GetLevels(id);
            action.Should().NotThrow();
        }

        [Test]
        public void GetAvailableLevels_DoesNotThrowException_WhenNoUsers()
        {
            var id = AddEmptyTopic();
            Action action = () => application.GetAvailableLevels(Guid.NewGuid(), id);
            action.Should().NotThrow();
        }

        [Test]
        public void GetAvailableLevels_DoesNotThrowException_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            Action action = () => application.GetAvailableLevels(Guid.NewGuid(), id);
            action.Should().NotThrow();
        }

        [Test]
        public void GetCurrentProgress_DoesNotThrowException_WhenNoUsers()
        {
            var (topicId, levelId) = AddTopicWithLevel();
            Action action = () => application.GetCurrentProgress(Guid.NewGuid(), topicId, levelId);
            action.Should().NotThrow();
        }

        [Test]
        public void GetTask_DoesNotThrowException_WhenNoUsers()
        {
            var (topicId, levelId) = AddTopicWithLevel();
            Action action = () => application.GetTask(Guid.NewGuid(), topicId, levelId);
            action.Should().NotThrow();
        }

        [Test]
        public void GetTask_DoesNotThrowException_WhenNoGenerators()
        {
            var (topicId, levelId) = AddTopicWithLevel();
            Action action = () => application.GetTask(Guid.NewGuid(), topicId, levelId);
            action.Should().NotThrow();
        }

        #endregion

        #region ThrowsException

        [Test]
        public void GetLevels_ThrowsTopicNotFoundException_WhenNoTopics()
        {
            Action action = () => application.GetLevels(Guid.NewGuid());
            action.Should().Throw<TopicNotFoundException>();
        }

        [Test]
        public void GetAvailableLevels_ThrowsTopicNotFoundException_WhenNoTopics()
        {
            Action action = () => application.GetAvailableLevels(Guid.NewGuid(), Guid.NewGuid());
            action.Should().Throw<TopicNotFoundException>();
        }

        [Test]
        public void GetCurrentProgress_ThrowsTopicNotFoundException_WhenNoTopics()
        {
            Action action = () => application.GetCurrentProgress(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            action.Should().Throw<TopicNotFoundException>();
        }

        [Test]
        public void GetCurrentProgress_ThrowsLevelNotFoundException_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            Action action = () => application.GetCurrentProgress(Guid.NewGuid(), id, Guid.NewGuid());
            action.Should().Throw<LevelNotFoundException>();
        }

        [Test]
        public void GetTask_ThrowsTopicNotFoundException_WhenNoTopics()
        {
            Action action = () => application.GetTask(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            action.Should().Throw<TopicNotFoundException>();
        }

        [Test]
        public void GetTask_ThrowsLevelNotFoundException_WhenNoLevels()
        {
            var id = AddEmptyTopic();
            Action action = () => application.GetTask(Guid.NewGuid(), id, Guid.NewGuid());
            action.Should().Throw<LevelNotFoundException>();
        }

        [Test]
        public void GetTask_ThrowsAccessDeniedException_WhenLevelNotAvailable()
        {
            var (topicId, _) = AddTopicWithLevel();
            var levelId = Guid.NewGuid();
            taskRepository.InsertLevel(topicId, new Level(levelId, "", new TaskGenerator[0]));
            var user = userRepository.FindOrInsertUser(Guid.NewGuid(), taskRepository);
            Action action = () => application.GetTask(user.Id, topicId, levelId);
            action.Should().Throw<AccessDeniedException>();
        }

        #endregion

        [Test]
        public void GetTopicsInfo_ReturnsAllTopics()
        {
            var ids = new List<Guid>();
            for (var i = 0; i < 5; i++)
                ids.Add(AddEmptyTopic());
            application.GetTopicsInfo().Select(t => t.Id).Should().BeEquivalentTo(ids);
        }

        [Test]
        public void GetLevels_ReturnsAllLevels()
        {
            var topicId = AddEmptyTopic();
            var ids = AddLevels(topicId);
            application.GetLevels(topicId).Select(l => l.Id).Should().BeEquivalentTo(ids);
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
            application.GetAvailableLevels(Guid.NewGuid(), topicId).First().Id.Should().Be(ids.First());
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
            var level = new Level(levelId, "", new[] { new TestGenerator(42) });
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
            return taskRepository.InsertLevel(topicId, new Level(Guid.NewGuid(), "", new TaskGenerator[0])).Id;
        }
    }
}