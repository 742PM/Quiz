using System;
using System.Collections.Generic;
using Domain;

namespace Tests
{
    public class TestGenerator : ITaskGenerator
    {
        public TestGenerator(int difficulty)
        {
            Difficulty = difficulty;
        }

        public IEnumerable<Task> Tasks => new[]
        {
            new Task(new[] { "a1", "a2", "a3" }, "?", new[] { "h1", "h2", "h3" }, "a1", Id),
        };

        public string Description => "Test";

        public int Difficulty { get; }

        public Guid Id => Guid.NewGuid();
    }
}