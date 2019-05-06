using System;
using System.Collections.Generic;
using Domain.Values;
using Infrastructure.Result;

namespace Application {
    class TaskService : ITaskService {
        public IEnumerable<Task> GetAllTopics()
        {
            throw new NotImplementedException();
        }

        public Guid AddEmptyTopic(string name, string description)
        {
            throw new NotImplementedException();
        }

        public Result<Guid, Exception> AddEmptyLevel(Guid topicId, string description, IEnumerable<Guid> previousLevels, IEnumerable<Guid> nextLevels)
        {
            throw new NotImplementedException();
        }

        public Guid AddTemplateGenerator(
            Guid topicId,
            Guid levelId,
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints,
            int streak)
        {
            throw new NotImplementedException();
        }

        public Task RenderTask(string template, IEnumerable<string> possibleAnswers, string rightAnswer, IEnumerable<string> hints)
        {
            throw new NotImplementedException();
        }
    }
}