using System;
using System.Collections;
using System.Collections.Generic;
using Domain;

namespace Tests
{
    public class TestGenerator : TaskGenerator
    {
        public TestGenerator(int difficulty)
        {
            Difficulty = difficulty;
            Id = Guid.NewGuid();
        }

        public override Task GetTask(Random randomSeed)
        {
            return new Task(
                new[] { "a1", "a2", "a3" }, 
                $"{Id}?", 
                new[] { "h1", "h2", "h3" },
                $"{Id}!", 
                Id);
        }



        public int Difficulty { get; }

        public override Guid Id { get; }
    }
}