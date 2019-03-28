using System;
using System.Linq;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Linq;

namespace DataBase
{
    public class MongoTaskRepository : ITaskRepository
    {
        private const string CollectionName = "topics";
        private readonly IMongoCollection<Topic> topicCollection;

        public MongoTaskRepository(IMongoDatabase database)
        {
            topicCollection = database.GetCollection<Topic>(CollectionName);
        }

        Topic[] ITaskRepository.GetTopics() => throw new NotImplementedException();

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
        public Level[] GetLevelsFromTopic(Guid topicId) => throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator[] GetGeneratorsFromLevel(Guid topicId, Guid levelId) =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public Topic InsertTopic(Topic topic) => throw new NotImplementedException();

        /// <inheritdoc />
        public Topic UpdateTopic(Topic topic) => throw new NotImplementedException();

        /// <inheritdoc />
        public Topic FindTopic(Guid topicId) => throw new NotImplementedException();

        /// <inheritdoc />
        public Level InsertLevel(Guid topicId, Level level) => throw new NotImplementedException();

        /// <inheritdoc />
        public Level UpdateLevel(Guid topicId, Level level) => throw new NotImplementedException();

        /// <inheritdoc />
        public Level FindLevel(Guid topicId, Guid levelId) => throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator InsertGenerator(Guid topicId, Guid levelId, TaskGenerator entity) =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator UpdateGenerator(Guid topicId, Guid levelId, TaskGenerator entity) =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId) =>
            throw new NotImplementedException();

        public Topic[] GetTopics() => throw new NotImplementedException();
    }
}
