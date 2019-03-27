using System;
using Domain.Entities;
using Domain.Entities.TaskGenerators;

namespace DataBase
{
    public interface ITaskRepository
    {
        Topic[] Topics { get; }

        Topic Insert(Topic topic);

        Topic Find(Guid id);

        Level InsertAt(Guid topicId, Level level);

        Level UpdateAt(Guid topicId, Level level);

        Level FindLevel(Guid topicId, Guid levelId);

        TaskGenerator InsertAt(Guid topicId, Guid levelId, TaskGenerator entity);

        TaskGenerator UpdateAt(Guid topicId, Guid levelId, TaskGenerator entity);

        TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId);
    }
}
