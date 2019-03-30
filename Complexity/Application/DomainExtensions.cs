using System.Linq;
using Application.Info;
using DataBase.Entities;
using Domain.Entities;
using Domain.Values;

namespace Application
{
    public static class DomainExtensions
    {
        public static TopicInfo ToInfo(this Topic topic) => new TopicInfo(topic.Name, topic.Id);

        public static LevelInfo ToInfo(this Level level) => new LevelInfo(level.Id, level.Description);

        public static TaskInfo ToInfo(this Task task) => new TaskInfo(task.Question, task.PossibleAnswers.HasNoValue?new string[0] :task.PossibleAnswers.Value);

        public static LevelProgressEntity ToProgressEntity(this Level level)
        {
            return new LevelProgressEntity
            {
                LevelId = level.Id,
                CurrentLevelStreaks = level
                    .Generators
                    .ToDictionary(
                        generator => generator.Id,
                        generator => 0)
            };
        }
    }
}