using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.TaskGenerators;

namespace Domain.Entities
{
    /// <summary>
    ///     Subtopic for some topic;
    /// </summary>
    [MustBeSaved]
    [Entity]
    public class Level : Entity<Guid>
    {
        public Level(Guid id, string description, ICollection<TaskGenerator> generators, Guid[] nextLevels) : base(id)
        {
            Description = description;
            NextLevels = nextLevels;
            Generators = generators.ToArray();
        }
        public Guid[] NextLevels { get; }
        public string Description { get; }

        public TaskGenerator[] Generators { get; }
    }
}
