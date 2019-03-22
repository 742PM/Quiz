using System;
using System.Collections.Generic;

namespace Domain
{
    [Entity]
    public class Topic
    {
        public Topic(IEnumerable<ITaskGenerator> generators, Guid id, string name, string description,
                     Level[] levels)
        {
            Generators = generators;
            Id = id;
            Name = name;
            Description = description;
            Levels = levels;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public Level[] Levels { get; }

        public IEnumerable<ITaskGenerator> Generators { get; }

        protected bool Equals(Topic other) => Id.Equals(other.Id);

        public override bool Equals(object obj) => obj is Topic topic && Equals(topic);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
