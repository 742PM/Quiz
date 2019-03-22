using System;
using System.Collections;
using System.Collections.Generic;
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

        /// <inheritdoc />
        public Task GetTask() => throw new NotImplementedException();

        public string Description => $"Test {Difficulty}";

        public int Difficulty { get; }

        public Guid Id { get; }

        /// <inheritdoc />
        public IEnumerator<Task> GetEnumerator() => throw new NotImplementedException();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}