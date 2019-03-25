using System;
using System.Globalization;
using Domain.Values;

namespace Domain.Entities.TaskGenerators
{
    public class ExampleTaskGenerator : TaskGenerator
    {
        private readonly Task exampleTask;

        public ExampleTaskGenerator(string hint, Guid id) : base(id)
        {
            exampleTask = new Task("Is this an \"a\"?", new[] {"What a hint!", "And there it goes...", $"{hint}!"}, "a",
                                   Id, new string[0]);
        }


        public override Task GetTask(Random randomSeed) =>
            exampleTask.With(randomSeed.NextDouble()
                                       .ToString(CultureInfo.InvariantCulture));
    }
}
