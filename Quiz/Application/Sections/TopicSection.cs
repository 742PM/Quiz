using System;

namespace Application.Sections
{
    public class TopicSection
    {
        public TopicSection(Guid id, string name, string description, Guid[] levelIds)
        {
            Id = id;
            Name = name;
            Description = description;
            LevelIds = levelIds;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Guid[] LevelIds { get; }
    }
}