using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using Domain.Entities;
using Domain.Entities.TaskGenerators;

namespace Tests.Mocks
{
    public class TestTaskRepository : ITaskRepository
    {
        private readonly Dictionary<Guid, Topic> topics = new Dictionary<Guid, Topic>();

        public Topic[] GetTopics()
        {
            return topics.Values.ToArray();
        }

        public Level[] GetLevelsFromTopic(Guid topicId)
        {
            return topics[topicId].Levels;
        }

        public TaskGenerator[] GetGeneratorsFromLevel(Guid topicId, Guid levelId)
        {
            return topics[topicId].Levels.FirstOrDefault(l => l.Id == levelId)?.Generators ?? new TaskGenerator[0];
        }

        public Topic InsertTopic(Topic topic)
        {
            return UpdateTopic(topic);
        }

        public Topic UpdateTopic(Topic topic)
        {
            topics[topic.Id] = topic;
            return topic;
        }

        public Topic FindTopic(Guid topicId)
        {
            return topics[topicId];
        }

        public Level InsertLevel(Guid topicId, Level level)
        {
            return UpdateLevel(topicId, level);
        }

        public Level UpdateLevel(Guid topicId, Level level)
        {
            var topic = topics[topicId];
            UpdateTopic(new Topic(
                topic.Id,
                topic.Name,
                topic.Description,
                topic
                    .Levels
                    .Where(l => l.Id != level.Id)
                    .Concat(new[] { level })
                    .ToArray()));
            return level;
        }

        public Level FindLevel(Guid topicId, Guid levelId)
        {
            return topics[topicId].Levels.First(l => l.Id == levelId);
        }

        public TaskGenerator InsertGenerator(Guid topicId, Guid levelId, TaskGenerator entity)
        {
            return UpdateGenerator(topicId, levelId, entity);
        }

        public TaskGenerator UpdateGenerator(Guid topicId, Guid levelId, TaskGenerator entity)
        {
            var topic = topics[topicId];
            var level = FindLevel(topicId, levelId);
            UpdateTopic(new Topic(
                topic.Id,
                topic.Name,
                topic.Description,
                topic
                    .Levels
                    .Where(l => l.Id != levelId)
                    .Concat(new[]
                    {
                        new Level(level.Id,
                            level.Description,
                            level
                                .Generators
                                .Where(g => g.Id != entity.Id)
                                .Concat(new[] { entity })
                                .ToArray())
                    })
                    .ToArray()));
            return entity;
        }

        public TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            return FindLevel(topicId, levelId).Generators.First(g => g.Id == generatorId);
        }
    }
}