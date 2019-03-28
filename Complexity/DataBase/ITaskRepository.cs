using System;
using Domain.Entities;
using Domain.Entities.TaskGenerators;

namespace DataBase
{
    public interface ITaskRepository
    {
        Topic[] GetTopics();

        Level[] GetLevelsFromTopic(Guid topicId);

        Level[] GetNextLevels(Guid topicId, Guid levelId);

        TaskGenerator[] GetGeneratorsFromLevel(Guid topicId, Guid levelId);

        Topic InsertTopic(Topic topic);

        Topic UpdateTopic(Topic topic);

        Topic FindTopic(Guid topicId);

        Level InsertLevel(Guid topicId, Level level);

        Level UpdateLevel(Guid topicId, Level level);

        Level FindLevel(Guid topicId, Guid levelId);

        TaskGenerator InsertGenerator(Guid topicId, Guid levelId, TaskGenerator entity);

        TaskGenerator UpdateGenerator(Guid topicId, Guid levelId, TaskGenerator entity);

        TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId);
    }
}
