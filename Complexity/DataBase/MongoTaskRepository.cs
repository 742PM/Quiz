using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using MongoDB.Driver;

namespace DataBase
{
    public class MongoTaskRepository : ITaskRepository
    {
        private const string CollectionName = "Topics";
        private readonly IMongoCollection<Topic> topicCollection;

        public MongoTaskRepository(IMongoDatabase database)
        {
            topicCollection = database.GetCollection<Topic>(CollectionName);
        }
        
        public Topic[] GetTopics()
        {
            return topicCollection.Find(t => true).ToList().ToArray();
        }

        /// <inheritdoc />
        public Level[] GetNextLevels(Guid topicId, Guid levelId)
        {
            var topic = topicCollection.Find(t => t.Id == topicId)
                                       .FirstOrDefault();
            var levels = topic?.Levels?.FirstOrDefault(l => l.Id == levelId)
                              ?.NextLevels ??
                         new Guid[0];
            return topic?.Levels?.Where(l => levels.Contains(l.Id))
                        .ToArray();
        }

        /// <inheritdoc />
        public Level[] GetLevelsFromTopic(Guid topicId)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            return topic?.Levels;

        }

        /// <inheritdoc />
        public TaskGenerator[] GetGeneratorsFromLevel(Guid topicId, Guid levelId)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            var level = topic?.Levels?.Where(l => l.Id == levelId).FirstOrDefault();
            return level?.Generators;
        }

        /// <inheritdoc />
        public Topic InsertTopic(Topic topic)
        { 
            topicCollection.InsertOne(topic);
            return topic;
        }

        /// <inheritdoc />
        public void UpdateTopic(Topic topic)
        {
            topicCollection.ReplaceOne(t => t.Id == topic.Id, topic);
        }

        /// <inheritdoc />
        public Topic FindTopic(Guid topicId)
        {
            return topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
        }

        /// <inheritdoc />
        public Level InsertLevel(Guid topicId, Level level)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            Console.WriteLine(topic);
            var levels = topic?.Levels?.Append(level);
            Console.WriteLine(levels.ToList().First());
            var newTopic = topic.With(levels: levels.ToArray());
            Console.WriteLine(newTopic);
            UpdateTopic(newTopic);
            return level;
        }

        /// <inheritdoc />
        public Level UpdateLevel(Guid topicId, Level level)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            var levels = topic?.Levels?.ToList();
            var index = levels?.FindIndex(l => l.Id == level.Id) ?? -1;
            if (index != -1 && levels != null)
            {
                levels[index] = level;
                var newTopic = topic.With(levels: levels.ToArray());
                UpdateTopic(newTopic);
            }
            return level;
        }

        /// <inheritdoc />
        public Level FindLevel(Guid topicId, Guid levelId)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            return topic?.Levels?.Where(l => l.Id == levelId).FirstOrDefault();
        }

        /// <inheritdoc />
        public TaskGenerator InsertGenerator(Guid topicId, Guid levelId, TaskGenerator generator)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            var levels = topic?.Levels?.ToList();
            var index = levels?.FindIndex(l => l.Id == levelId) ?? -1;
            if (index != -1 && levels != null)
            {
                var level = levels[index];
                var generators = level.Generators.Append(generator);
                var newLevel = level.With(generators: generators.ToArray());
                levels[index] = newLevel;
                var newTopic = topic.With(levels: levels.ToArray());
                UpdateTopic(newTopic);
            }
            return generator;
        }
        
        public ICollection<TaskGenerator> InsertGenerators(Guid topicId, Guid levelId, ICollection<TaskGenerator> generators)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            var levels = topic?.Levels?.ToList();
            var index = levels?.FindIndex(l => l.Id == levelId) ?? -1;
            if (index != -1 && levels != null)
            {
                var level = levels[index];
                var levelGenerators = level.Generators.ToList();
                levelGenerators.AddRange(generators);
                var newLevel = level.With(generators: levelGenerators.ToArray());
                levels[index] = newLevel;
                var newTopic = topic.With(levels: levels.ToArray());
                UpdateTopic(newTopic);
            }
            return generators;
        }

        /// <inheritdoc />
        public TaskGenerator UpdateGenerator(Guid topicId, Guid levelId, TaskGenerator generator)
        { 
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            var levels = topic?.Levels?.ToList();
            var index = levels?.FindIndex(l => l.Id == levelId) ?? -1;
            if (index != -1 && levels != null)
            {
                var level = levels[index];
                var generators = level.Generators;
                var genIndex = level.Generators.ToList().FindIndex(l => l.Id == generator.Id);
                generators[genIndex] = generator;
                var newLevel = level.With(generators: generators.ToArray());
                levels[index] = newLevel;
                var newTopic = topic.With(levels: levels.ToArray());
                UpdateTopic(newTopic);
            }
            return generator;
        }

        /// <inheritdoc />
        public TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            var topic = topicCollection.Find(t => t.Id == topicId).FirstOrDefault();
            var levels = topic?.Levels?.ToList();
            var level = levels?.FirstOrDefault(l => l.Id == levelId);
            var generator = level?.Generators.FirstOrDefault(l => l.Id == generatorId);
            return generator;
        }

    }
}
