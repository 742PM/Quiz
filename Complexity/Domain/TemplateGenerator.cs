using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class TemplateGenerator : ITaskGenerator
    {
        private readonly Random random;

        public TemplateGenerator(GeneratorTemplate template, string description, Guid id)
        {
            Description = description;
            (TemplateCode, Hints, Answer) = template;
            random = new Random(42);
            Id = id;
        }

        public string TemplateCode { get; set; }

        public string[] Hints { get; set; }

        public string Answer { get; set; }

        /// <inheritdoc />
        public IEnumerator<Task> GetEnumerator()
        {
            while (true)
            {
                yield return GetTask(random);
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public Task GetTask(Random randomSeed) => new Task(Randomize(randomSeed), Hints, Answer, Id);

        /// <inheritdoc />
        public Task GetTask() => GetTask(random);

        /// <inheritdoc />
        public int Difficulty { get; }

        /// <inheritdoc />
        public string Description { get; }


        /// <inheritdoc />
        public Guid Id { get; }

        private string Randomize(Random randomSeed) =>
            TemplateCode.Replace("$i$", ((char) randomSeed.Next('a', 'z')).ToString());
    }
}
