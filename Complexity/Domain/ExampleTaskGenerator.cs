using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ExampleTaskGenerator : ITaskGenerator
    {
        private readonly Task exampleTask;

        public ExampleTaskGenerator(string hint)
        {
            exampleTask = new Task(new[] {"a", "b", "c"}, "Is this an \"a\"?",
                                   new[] {"What a hint!", "And there it goes...", $"{hint}!"}, "a");
        }

        public IEnumerable<Task> Tasks => Enumerable.Repeat(exampleTask, 5);

        public string Description => "This is so example!";
    }
}
