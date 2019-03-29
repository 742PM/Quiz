using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.TaskGenerators;
using Infrastructure;
using Infrastructure.DDD;

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
            if (nextLevels.Contains(id))
                throw new ArgumentException($"Cyclic reference: one of predecessors equals to this {nameof(Level)}");
            NextLevels = nextLevels;
            Generators = generators.ToArray();
        }
        public Guid[] NextLevels { get; }
        public string Description { get; }

        public TaskGenerator[] Generators { get; }
    }
}
