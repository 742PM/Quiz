using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.DDD;

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

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Level[] Levels { get; private set; }

        public Topic With(Guid? id = default,
            string name = default,
            string description = default,
            ICollection<Level> levels = default) =>
            new Topic(id ?? Id, name ?? Name, description ?? Description, levels ?? Levels);
    }
}