using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IQuestionApi
    {
        IEnumerable<Topic> Topics { get; }

        Topic this[Guid topicId] { get; }

        IEnumerable<Topic> this[string topicName] { get; }

        Guid Id { get; }
    }

    internal class QuestionApi : IQuestionApi
    {
        public IEnumerable<Topic> Topics { get; }
        public Topic this[Guid topicId] => throw new NotImplementedException();

        public IEnumerable<Topic> this[string topicName] => throw new NotImplementedException();

        public Guid Id { get; }
    }
}
