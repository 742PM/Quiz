//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Domain;
//using Domain.Entities.TaskGenerators;
//using Domain.Values;

//namespace Tests
//{
//    public class TestGenerator : TaskGenerator
//    {
//        public TestGenerator(int difficulty) : base(Guid.NewGuid(), 1)
//        {
//            Difficulty = difficulty;
//        }

//        public new Task GetTask(Random randomSeed)
//        {
//            return new Task(
//                $"{Id}?", 
//                new[] { "h1", "h2", "h3" },
//                $"{Id}!", 
//                Id,
//                new[] { "a1", "a2", "a3" });
//        }

//        public int Difficulty { get; }
//    }
//}