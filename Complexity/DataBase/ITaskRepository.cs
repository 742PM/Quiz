using System;
using DataBase.DatabaseEntities;
using MongoDB.Driver;

namespace DataBase
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Inserts user;
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        TopicEntity Insert(TopicEntity topic);
        TopicEntity Find(Guid id);

        LevelEntity InsertAt(Guid topicId, LevelEntity level);
        LevelEntity UpdateAt(Guid topicId, LevelEntity level);
        LevelEntity FindLevel(Guid topicId, Guid levelId);

        GeneratorEntity InsertAt(Guid topicId, Guid levelId, GeneratorEntity entity);
        GeneratorEntity UpdateAt(Guid topicId, Guid levelId, GeneratorEntity entity);
        GeneratorEntity FindGenerator(Guid topicId, Guid levelId, Guid generatorId);
    }

    class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TopicEntity> topicCollection;
        private const string CollectionName = "topics";
        public TaskRepository(IMongoDatabase database)
        {
            topicCollection = database.GetCollection<TopicEntity>(CollectionName);
        }
        /// <inheritdoc />
        /// <summary>
        /// Gently;
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
        public GeneratorEntity InsertAt(Guid topicId, Guid levelId, GeneratorEntity entity) => throw new NotImplementedException();

        /// <inheritdoc />
        public GeneratorEntity UpdateAt(Guid topicId, Guid levelId, GeneratorEntity entity) => throw new NotImplementedException();

        /// <inheritdoc />
        public GeneratorEntity FindGenerator(Guid topicId, Guid levelId, Guid generatorId) => throw new NotImplementedException();
    }
}