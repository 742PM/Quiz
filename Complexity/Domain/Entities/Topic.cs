using System;

namespace Domain.Entities
{
    [Entity]
    [MustBeSaved]
    public class Topic : Entity<Guid>
    {
        public Topic(Guid id, string name, string description,
                     Level[] levels) : base(id)
        {
            Name = name;
            Description = description;
            Levels = levels;
        }

        public string Name { get; }

        public string Description { get; }

        public Level[] Levels { get; }
    }
}
