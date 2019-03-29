using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Domain.Entities
{
    [Entity]
    [MustBeSaved]
    public class Topic : Entity<Guid>
    {
        public Topic(Guid id, string name, string description, ICollection<Level> levels) : base(id)
        {
            Name = name;
            Description = description;
            Levels = levels.ToArray();
        }

        public string Name { get; }

        public string Description { get; }

        public Level[] Levels { get; }
    }
}
