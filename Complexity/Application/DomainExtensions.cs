using Application.Info;
using DataBase;
using Domain;

namespace Application
{
    public static class DomainExtensions
    {
        public static TaskInfo ToInfo(this Task task) => new TaskInfo(task.Question, task.Answers);

        public static TopicInfo ToInfo(this Topic topic) => new TopicInfo(topic.Name, topic.Id);

        public static TaskEntity ToEntity(this Task task, ITaskGenerator generator)
        {
            var (question, answers, hints, rightAnswer) = task;
            return new TaskEntity
            {
                Answers = answers,
                Difficulty = generator.Difficulty,
                GeneratorId = generator.Id,
                Hints = hints,
                Question = question,
                RightAnswer = rightAnswer
            };
        }
    }
}