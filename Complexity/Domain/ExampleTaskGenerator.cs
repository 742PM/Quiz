using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Domain
{
    public class ExampleTaskGenerator : ITaskGenerator
    {
        private readonly Task exampleTask;

        public ExampleTaskGenerator(string hint)
        {
            exampleTask = new Task("Is this an \"a\"?",
                                   new[] {"What a hint!", "And there it goes...", $"{hint}!"}, "a", Id);
        }

        public IEnumerable<Task> Tasks => Enumerable.Repeat(exampleTask, 10000);

        public Task GetTask(Random randomSeed) => exampleTask.With(randomSeed.NextDouble().ToString(CultureInfo.InvariantCulture));
        public Task GetTask() => GetTask(new Random(42));

        public string Description => "This is so example!";
        public int Difficulty => 0;

        public Guid Id => new Guid(1,2,3,4,5,6,7,8,9,10,11);

        /// <inheritdoc />
        public IEnumerator<Task> GetEnumerator() => throw new NotImplementedException();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
