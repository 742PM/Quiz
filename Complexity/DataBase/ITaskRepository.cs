using System;
using DataBase.DatabaseEntities;
using DataBase.DatabaseEntities.GeneratorEntities;

namespace DataBase
{
    public interface ITaskRepository
    {
        /// <summary>
        ///     Inserts user;
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
}
