using System;
using Domain;

namespace Tests
{
    public class TestGenerator : ITaskGenerator
    {
        public TestGenerator(int difficulty)
        {
            Difficulty = difficulty;
            Id = Guid.NewGuid();
        }

        public Task GetTask(Random randomSeed)
        {
            return new Task(
                new[] { "a1", "a2", "a3" }, 
                $"{Id}?", 
                new[] { "h1", "h2", "h3" },
                $"{Id}!", 
                Id);
        }

        public string Description => $"Test {Difficulty}";

        public int Difficulty { get; }

        public Guid Id { get; }
    }
}