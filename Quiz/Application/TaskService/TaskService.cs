using System;
using System.Collections.Generic;
using System.Linq;
using Application.Extensions;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Domain.Values;
using Infrastructure.Extensions;
using Infrastructure.Result;
using static Infrastructure.Result.None;

namespace Application.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly Random random;

        public TaskService(ITaskRepository taskRepository, Random random)
        {
            this.taskRepository = taskRepository;
            this.random = random;
        }

        /// <inheritdoc />
        public Result<IEnumerable<TemplateTaskGenerator>, Exception> GetTemplateGenerators(Guid topicId, Guid levelId)
        {
            if (!taskRepository.TopicExists(topicId))
                return new ArgumentException(nameof(topicId));
            if (!taskRepository.LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));

            return taskRepository
                .FindLevel(topicId, levelId)
                .Generators
                .Cast<TemplateTaskGenerator>()
                .Ok();
        }

        /// <inheritdoc />
        public Guid AddEmptyTopic(string name, string description)
        {
            return taskRepository
                .InsertTopic(new Topic(Guid.NewGuid(), name, description, new Level[0]))
                .Id;
        }

        /// <inheritdoc />
        public Result<None, Exception> DeleteTopic(Guid topicId)
        {
            if (!taskRepository.TopicExists(topicId))
                return new ArgumentException(nameof(topicId));

            taskRepository.DeleteTopic(topicId);
            return Nothing;
        }

        /// <inheritdoc />
        public Result<Guid, Exception> AddEmptyLevel(
            Guid topicId,
            string description,
            IEnumerable<Guid> previousLevels,
            IEnumerable<Guid> nextLevels)
        {
            if (!taskRepository.TopicExists(topicId))
                return new ArgumentException(nameof(topicId));

            var nextLevelIds = nextLevels.ToArray();
            var levelId = Guid.NewGuid();

            foreach (var id in nextLevelIds)
            {
                if (!taskRepository.LevelExists(topicId, levelId))
                    return new ArgumentException($"previous level with {id} doesn't exists");

                var level = taskRepository.FindLevel(topicId, levelId);
                var newNextLevels = level.NextLevels.Concat(new[] { id }).ToArray();

                taskRepository.UpdateLevel(topicId, level.With(nextLevels: newNextLevels));
            }

            return taskRepository
                .InsertLevel(
                    topicId,
                    new Level(levelId, description, new TaskGenerator[0], nextLevelIds))
                .Id
                .Ok();
        }

        /// <inheritdoc />
        public Result<None, Exception> DeleteLevel(Guid topicId, Guid levelId)
        {
            if (!taskRepository.TopicExists(topicId))
                return new ArgumentException(nameof(topicId));
            if (!taskRepository.LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));

            taskRepository.DeleteLevel(topicId, levelId);
            return Nothing;
        }

        /// <inheritdoc />
        public Result<Guid, Exception> AddTemplateGenerator(
            Guid topicId,
            Guid levelId,
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints,
            int streak,
            string question)
        {
            if (!taskRepository.TopicExists(topicId))
                return new ArgumentException(nameof(topicId));
            if (!taskRepository.LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));

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
                        streak, question))
                .Id;
        }

        /// <inheritdoc />
        public Result<None, Exception> DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            if (!taskRepository.TopicExists(topicId))
                return new ArgumentException(nameof(topicId));
            if (!taskRepository.LevelExists(topicId, levelId))
                return new ArgumentException(nameof(levelId));
            if (!taskRepository.GeneratorExists(topicId, levelId, generatorId))
                return new ArgumentException(nameof(generatorId));

            taskRepository.DeleteGenerator(topicId, levelId, generatorId);
            return Nothing;
        }

        /// <inheritdoc />
        public Task RenderTask(
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints,
            string question)
        {
            var possibleAnswerArray = possibleAnswers as string[] ?? possibleAnswers.ToArray();
            var hintsArray = hints as string[] ?? hints.ToArray();

            return new TemplateTaskGenerator(Guid.Empty, possibleAnswerArray, template, hintsArray, rightAnswer, 1,
                    question)
                .GetTask(random);
        }
    }
}