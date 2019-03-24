using System;
using System.Collections.Generic;

namespace Domain
{
    [Entity] //Should it be Value?
    [MustBeSaved]
    public class Topic
    {
        public Topic(Guid id, string name, string description,
                     Level[] levels)
        {
            Id = id;
            Name = name;
            Description = description;
            Levels = levels;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public Level[] Levels { get; }

        protected bool Equals(Topic other) => Id.Equals(other.Id);

        public override bool Equals(object obj) => obj is Topic topic && Equals(topic);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
