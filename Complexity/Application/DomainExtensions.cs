using Domain;

namespace Application
{
    public static class DomainExtensions
    {
        public static TaskInfo ToInfo(this Task task) => new TaskInfo(task.Question, task.Answers);

        public static TopicInfo ToInfo(this Topic topic) => new TopicInfo(topic.Name, topic.Id);
    }
}