using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Domain.Values;
using Infrastructure.Result;

namespace Application
{
    class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly Random random;

        public TaskService(ITaskRepository taskRepository, Random random)
        {
            this.taskRepository = taskRepository;
            this.random = random;
        }

        public IEnumerable<Topic> GetAllTopics() => taskRepository.GetTopics();

        public Guid AddEmptyTopic(string name, string description)
        {
            return taskRepository
                .InsertTopic(new Topic(Guid.NewGuid(), name, description, new Level[0]))
                .Id;
        }

        public Result<Guid, Exception> AddEmptyLevel(
            Guid topicId,
            string description,
            IEnumerable<Guid> previousLevels,
            IEnumerable<Guid> nextLevels)
        {
            return taskRepository
                .InsertLevel(
                    topicId,
                    new Level(Guid.NewGuid(), description, new TaskGenerator[0], new Guid[0]))
                .Id;
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
            var possibleAnswerArray = possibleAnswers as string[] ?? possibleAnswers.ToArray();
            var hintsArray = hints as string[] ?? hints.ToArray();

            return taskRepository
                .InsertGenerator(
                    topicId,
                    levelId,
                    new TemplateTaskGenerator(
                        Guid.NewGuid(),
                        possibleAnswerArray,
                        template,
                        hintsArray,
                        rightAnswer,
                        streak))
                .Id;
        }

        public Task RenderTask(
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints)
        {
            var possibleAnswerArray = possibleAnswers as string[] ?? possibleAnswers.ToArray();
            var hintsArray = hints as string[] ?? hints.ToArray();

            return new TemplateTaskGenerator(Guid.Empty, possibleAnswerArray, template, hintsArray, rightAnswer, 1)
                .GetTask(random);
        }
    }
}