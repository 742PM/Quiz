using System;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using MongoDB.Driver;

namespace DataBase
{
    internal class MongoTaskRepository : ITaskRepository
    {
        private const string CollectionName = "topics";
        private readonly IMongoCollection<Topic> topicCollection;

        public MongoTaskRepository(IMongoDatabase database)
        {
            topicCollection = database.GetCollection<TopicEntity>(CollectionName);
        }

        /// <inheritdoc />
        public Topic[] Topics { get; }

        /// <inheritdoc />
        public Level[] Levels(Guid topicId) => throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator[] Generators(Guid topicId, Guid levelId) => throw new NotImplementedException();

        /// <inheritdoc />
        /// <summary>
        ///     Gently;
        /// </summary>
        public Topic Insert(Topic topic)
        {
            topicCollection.InsertOne(topic);
            return topic;
        }

        /// <inheritdoc />
        public Topic Find(Guid id) => throw new NotImplementedException();

        /// <inheritdoc />
        public Level InsertAt(Guid topicId, Level level) => throw new NotImplementedException();

        /// <inheritdoc />
        public Level UpdateAt(Guid topicId, Level level) => throw new NotImplementedException();

        /// <inheritdoc />
        public Level FindLevel(Guid topicId, Guid levelId) => throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator InsertAt(Guid topicId, Guid levelId, TaskGenerator entity) => throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator UpdateAt(Guid topicId, Guid levelId, TaskGenerator entity) => throw new NotImplementedException();

        /// <inheritdoc />
        public TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId) => throw new NotImplementedException();
    }
}
