using System;
using System.Linq;
using Application.Info;
using Application.Repositories.Entities;
using Application.Sections;
using Domain.Entities;
using Domain.Values;
using Infrastructure.Extensions;

namespace Application.Extensions
{
    public static class DomainExtensions
    {
        public static TopicInfo ToInfo(this Topic topic) => new TopicInfo(topic.Name, topic.Id);

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

        public static TopicSection ToSection(this Topic topic)
        {
            return new TopicSection(
                topic.Id,
                topic.Name,
                topic.Description,
                topic
                    .Levels
                    .Select(level => level.Id)
                    .ToArray());
        }

        public static LevelSection ToSection(this Level level)
        {
            return new LevelSection(
                level.Id, 
                level.Description,
                level.NextLevels, 
                level
                    .Generators
                    .Select(generator => generator.Id)
                    .ToArray());
        }
    }
}