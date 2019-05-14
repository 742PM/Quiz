using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.TaskGenerators;

namespace Tests.Mocks
{
    public class TestTaskRepository : ITaskRepository
    {
        private readonly Dictionary<Guid, Topic> topics = new Dictionary<Guid, Topic>();

        public Topic[] GetTopics() => topics.Values.ToArray();

        public Level[] GetNextLevels(Guid topicId, Guid levelId)
        {
            return FindLevel(topicId, levelId)
                .NextLevels.Select(id => FindLevel(topicId, id))
                .ToArray();
        }

        public Level[] GetLevelsFromTopic(Guid topicId) =>
            topics.TryGetValue(topicId, out var topic) ? topic.Levels : null;

        public TaskGenerator[] GetGeneratorsFromLevel(Guid topicId, Guid levelId)
        {
            return GetLevelsFromTopic(topicId)
                ?.FirstOrDefault(l => l.Id == levelId)
                ?.Generators;
        }

        public Topic InsertTopic(Topic topic) => UpdateTopic(topic);

        [Obsolete]
        void ITaskRepository.UpdateTopic(Topic topic)
        {
            throw new NotImplementedException();
        }

        public Topic UpdateTopic(Topic topic)
        {
            topics[topic.Id] = topic;
            return topic;
        }

        public Topic FindTopic(Guid topicId) => topics.TryGetValue(topicId, out var topic) ? topic : null;

        public Level InsertLevel(Guid topicId, Level level) => UpdateLevel(topicId, level);

        public Level UpdateLevel(Guid topicId, Level level)
        {
            var topic = topics[topicId];
            UpdateTopic(new Topic(
                topic.Id,
                topic.Name,
                topic.Description,
                topic.Levels
                    .Where(l => l.Id != level.Id)
                    .Concat(new[] { level })
                    .ToArray()));
            return level;
        }

        public Level FindLevel(Guid topicId, Guid levelId)
        {
            return GetLevelsFromTopic(topicId)?.FirstOrDefault(l => l.Id == levelId);
        }

        public TaskGenerator InsertGenerator(Guid topicId, Guid levelId, TaskGenerator entity) =>
            UpdateGenerator(topicId, levelId, entity);

        [Obsolete]
        public ICollection<TaskGenerator> InsertGenerators(Guid topicId, Guid levelId, ICollection<TaskGenerator> entity)
        {
            throw new NotImplementedException();
        }

        public TaskGenerator UpdateGenerator(Guid topicId, Guid levelId, TaskGenerator entity)
        {
            var topic = topics[topicId];
            var level = FindLevel(topicId, levelId);
            UpdateTopic(new Topic(
                topic.Id,
                topic.Name,
                topic.Description,
                topic.Levels
                    .Where(l => l.Id != levelId)
                    .Concat(new[]
                    {
                        new Level(level.Id,
                            level.Description,
                            level.Generators
                                .Where(g => g.Id != entity.Id)
                                .Concat(new[] { entity })
                                .ToArray(),
                            level.NextLevels)
                    })
                    .ToArray()));
            return entity;
        }

        public TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            return FindLevel(topicId, levelId)?.Generators.FirstOrDefault(g => g.Id == generatorId);
        }

        [Obsolete]
        public void DeleteTopic(Guid topicId)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public void DeleteLevel(Guid topicId, Guid levelId)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public void DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            throw new NotImplementedException();
        }
    }
}