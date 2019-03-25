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
        public static TaskInfo ToInfo(this Task task) => new TaskInfo(task.Question, null); //TODO: remove stub

        public static TopicInfo ToInfo(this Topic topic) => new TopicInfo(topic.Name, topic.Id);

        public static TaskEntity ToEntity(this Task task, TaskGenerator generator)
        {
            var (question, hints, rightAnswer) = task;
            return new TaskEntity
            {
                Difficulty = generator.Difficulty,
                GeneratorId = generator.Id,
                Hints = hints,
                Question = question,
                RightAnswer = rightAnswer
            };
        }
    }
}