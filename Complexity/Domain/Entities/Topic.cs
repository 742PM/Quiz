using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.DDD;

namespace Domain.Entities
{
    [Entity]
    [MustBeSaved]
    public class Topic : Entity
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

        public Topic With(Guid? id = default,
            string name = default,
            string description = default,
            ICollection<Level> levels = default)
        {
            return new Topic(id ?? Id, name ?? Name, description ?? Description, levels ?? Levels);
        }
    }
}