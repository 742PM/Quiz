using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class QuizApi : IQuizApi
    {
        public QuizApi(IEnumerable<Topic> topics, Guid id)
        {
            Topics = topics;
            Id = id;
        }

        public IEnumerable<Topic> Topics { get; }
        public Topic this[Guid topicId] => Topics.FirstOrDefault(t => t.Id == topicId);

        public Topic this[string topicName] => Topics.FirstOrDefault(t => t.Name == topicName);

        public Guid Id { get; }
    }
}