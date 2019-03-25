using System;
using DataBase.DatabaseEntities;
using DataBase.DatabaseEntities.GeneratorEntities;
using MongoDB.Driver;

namespace DataBase
{
    internal class TaskRepository : ITaskRepository
    {
        private const string CollectionName = "topics";
        private readonly IMongoCollection<TopicEntity> topicCollection;

        public TaskRepository(IMongoDatabase database)
        {
            topicCollection = database.GetCollection<TopicEntity>(CollectionName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gently;
        /// </summary>
        public TopicEntity Insert(TopicEntity topic)
        {
            topicCollection.InsertOne(topic);
            return topic;
        }

        /// <inheritdoc />
        public TopicEntity Find(Guid id) => throw new NotImplementedException();

        /// <inheritdoc />
        public LevelEntity InsertAt(Guid topicId, LevelEntity level) => throw new NotImplementedException();

        /// <inheritdoc />
        public LevelEntity UpdateAt(Guid topicId, LevelEntity level) => throw new NotImplementedException();

        /// <inheritdoc />
        public LevelEntity FindLevel(Guid topicId, Guid levelId) => throw new NotImplementedException();

        /// <inheritdoc />
        public GeneratorEntity InsertAt(Guid topicId, Guid levelId, GeneratorEntity entity) =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public GeneratorEntity UpdateAt(Guid topicId, Guid levelId, GeneratorEntity entity) =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public GeneratorEntity FindGenerator(Guid topicId, Guid levelId, Guid generatorId) =>
            throw new NotImplementedException();
    }
}
