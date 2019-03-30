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

        public static TaskInfo ToInfo(this Task task) =>
            new TaskInfo(task.Question, task.PossibleAnswers.HasNoValue ? new string[0] : task.PossibleAnswers.Value);

        public static TaskInfoEntity AsInfoEntity(this Task task) =>
            new TaskInfoEntity(task.Question, task.Answer, task.Hints.HasValue ? task.Hints.Value : new string[0], 0,
                               task.ParentGeneratorId, false);

        public static LevelProgressEntity ToProgressEntity(this Level level)
        {
            return new LevelProgressEntity(level.Id,
                                           level.Generators.ToDictionary(generator => generator.Id, generator => 0));
        }
    }
}
