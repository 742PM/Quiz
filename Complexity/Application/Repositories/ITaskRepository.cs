using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Entities.TaskGenerators;

namespace Application.Repositories
{
    public interface ITaskRepository
    {
        Topic[] GetTopics();

        Level[] GetNextLevels(Guid topicId, Guid levelId);

        Level[] GetLevelsFromTopic(Guid topicId);

        TaskGenerator[] GetGeneratorsFromLevel(Guid topicId, Guid levelId);

        Topic InsertTopic(Topic topic);

        void UpdateTopic(Topic topic);

        Topic FindTopic(Guid topicId);

        Level InsertLevel(Guid topicId, Level level);

        Level UpdateLevel(Guid topicId, Level level);

        Level FindLevel(Guid topicId, Guid levelId);

        TaskGenerator InsertGenerator(Guid topicId, Guid levelId, TaskGenerator entity);
        
        ICollection<TaskGenerator> InsertGenerators(Guid topicId, Guid levelId, ICollection<TaskGenerator> entity);

        TaskGenerator UpdateGenerator(Guid topicId, Guid levelId, TaskGenerator entity);

        TaskGenerator FindGenerator(Guid topicId, Guid levelId, Guid generatorId);

        void DeleteTopic(Guid topicId);

        void DeleteLevel(Guid topicId, Guid levelId);

        void DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId);
    }
}