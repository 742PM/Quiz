using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Info;
using Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        private readonly IEnumerable<ITaskGenerator> generators = new[]
        {
            new TestGenerator(1),
            new TestGenerator(2),
            new TestGenerator(3)
        };

        private IEnumerable<Topic> topics;
        private IApplicationApi application;
        private IApplicationApi applicationWithoutTopics;
        private IApplicationApi applicationWithoutGenerators;

        [SetUp]
        public void SetUp()
        {
            applicationWithoutTopics = new Application.Application(new Topic[0], new TestUserRepository());

            applicationWithoutGenerators = new Application.Application(
                new[] { new Topic(new TestGenerator[0], Guid.NewGuid(), "n", "d") },
                new TestUserRepository());

            topics = new[]
            {
                CreateTopic(1),
                CreateTopic(2),
                CreateTopic(3)
            };
            application = new Application.Application(topics, new TestUserRepository());
        }

        private Topic CreateTopic(int number) => new Topic(generators, Guid.NewGuid(), $"t{number}", $"d{number}");

        [Test]
        public void ReturnsNoTopicsInfo_WhenNoTopicsInApplication()
        {
            applicationWithoutTopics.GetTopicsInfo().Should().BeEmpty();
        }

        [Test]
        public void ReturnsAllTopics()
        {
            application
                .GetTopicsInfo()
                .Should()
                .BeEquivalentTo(topics.Select(topic => new TopicInfo(topic.Name)),
                    options => options.Excluding(topic => topic.Id));
        }

        [Test]
        public void ThrowsArgumentException_WhenNoTopics()
        {
            Action action = () => applicationWithoutTopics.GetDifficulties(Guid.NewGuid());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ThrowsArgumentException_WhenNoSuchTopicId()
        {
            Action action = () => application.GetDifficulties(Guid.NewGuid());
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReturnsNoDifficulties_WhenTopicIsEmpty()
        {
            var id = applicationWithoutGenerators.GetTopicsInfo().First().Id;
            applicationWithoutGenerators.GetDifficulties(id).Should().BeEmpty();
        }

        [Test]
        public void ReturnsAllDifficulties()
        {
            application
                .GetDifficulties(topics.First().Id)
                .Should()
                .BeEquivalentTo(generators.Select(g => g.Difficulty));
        }
    }
}