using System;
using System.Linq;
using Application.DTO;
using Application.Info;
using Application.Repositories.Entities;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Domain.Values;
using Infrastructure.Extensions;

namespace Application.Extensions
{
    public static class DomainExtensions
    {
        public static TopicInfo ToInfo(this Topic topic) => new TopicInfo(topic.Name, topic.Id);

        public static TopicDto ToDTO(this Topic topic)
        {
            var ids = topic.Levels.Zip(Enumerable.Range(0, topic.Levels.Length), (l, i) => (level: l, i))
                           .ToDictionary(tup => tup.level.Id, tup => tup.i);

            return new TopicDto
                   {
                       Description = topic.Description,
                       Name = topic.Name,
                       Levels = topic.Levels.Select(level =>
                                                        new LevelDto
                                                        {
                                                            Description = level.Description,
                                                            Generators = level
                                                                         .Generators
                                                                         .Select(g => (TemplateTaskGeneratorDTO) g
                                                                                     .Cast<TemplateTaskGenerator>())
                                                                         .ToArray(),
                                                            NextLevels = level
                                                                         .NextLevels.Select(id => ids[id])
                                                                         .ToArray(),
                                                            Number = ids[level.Id]
                                                        }
                                                   )
                                     .ToArray()
                   };
        }

        public static LevelInfo ToInfo(this Level level) => new LevelInfo(level.Id, level.Description);

        public static TaskInfo ToInfo(this Task task) =>
            new TaskInfo(task.Question, task.PossibleAnswers, task.Hints.Length > 0, task.Text);

        public static TaskInfoEntity AsInfoEntity(this Task task) =>
            new TaskInfoEntity(
                               task.Text,
                               task.Answer,
                               task.Hints,
                               0,
                               task.ParentGeneratorId,
                               false,
                               Guid.NewGuid());

        public static LevelProgressEntity ToProgressEntity(this Level level)
        {
            return new LevelProgressEntity(
                                           level.Id,
                                           level
                                               .Generators
                                               .SafeToDictionary(generator => generator.Id, generator => 0),
                                           Guid.NewGuid());
        }

        public static TopicProgressEntity ToProgressEntity(this Topic topic)
        {
            return new TopicProgressEntity(
                                           topic
                                               .Levels
                                               .Take(1)
                                               .SafeToDictionary(level => level.Id, level => level.ToProgressEntity()),
                                           topic.Id,
                                           Guid.NewGuid());
        }
    }
}