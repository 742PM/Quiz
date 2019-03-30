using System;
using Domain.Entities.TaskGenerators;
using Domain.Values;

namespace Tests.Mocks
{
    public class TestGenerator : TaskGenerator
    {
        public TestGenerator(int difficulty) : base(Guid.NewGuid(), 1)
        {
            Difficulty = difficulty;
        }

        public int Difficulty { get; }

        public override Task GetTask(Random randomSeed) =>
            new Task(
                $"{Id}?",
                new[] { $"h1{Id}", $"h2{Id}", $"h3{Id}" },
                $"{Id}!", Id,
                new[] { $"a1{Id}", $"a2{Id}", $"a3{Id}" });
    }
}