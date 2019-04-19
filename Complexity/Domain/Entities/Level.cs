using System;
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
            if (nextLevels == null)
                throw new ArgumentNullException(nameof(nextLevels));
            Description = description;
            if (nextLevels.Contains(id))
                throw new ArgumentException($"Cyclic reference: one of predecessors equals to this {nameof(Level)}");
            NextLevels = nextLevels;
            Generators = generators.ToArray();
        }
        
        public Guid[] NextLevels { get; private set;}
        public string Description { get; private set;}

        public TaskGenerator[] Generators { get; private set;}
        
        public Level With(
            Guid? id = default, 
            string description = default, 
            ICollection<TaskGenerator> generators = default, 
            Guid[] nextLevels = default) =>
            new Level(id ?? Id, description ?? Description, generators ?? Generators,
                nextLevels ?? NextLevels);
    }
}
