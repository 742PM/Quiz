using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Domain
{

    public class ExampleTaskGenerator : TaskGenerator
    {
        private readonly Task exampleTask;

        public ExampleTaskGenerator(string hint, Guid id)
        {
            Id = id;
            exampleTask = new Task("Is this an \"a\"?",
                                   new[] {"What a hint!", "And there it goes...", $"{hint}!"}, "a", Id);
        }

        public override Task GetTask(Random randomSeed) => exampleTask.With(randomSeed.NextDouble().ToString(CultureInfo.InvariantCulture));

        /// <inheritdoc />
        public override Guid Id { get; }




    }
}
