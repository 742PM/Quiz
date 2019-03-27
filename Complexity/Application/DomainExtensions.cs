using Application.Info;
using DataBase;
using Domain;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Domain.Values;

namespace Application
{
    public static class DomainExtensions
    {
        public static TopicInfo ToInfo(this Topic topic) => new TopicInfo(topic.Name, topic.Id);

        public static LevelInfo ToInfo(this Level level) => new LevelInfo(level.Id, level.Description);

        public static TaskInfo ToInfo(this Task task) => new TaskInfo(task.Question, task.PossibleAnswers);
    }
}