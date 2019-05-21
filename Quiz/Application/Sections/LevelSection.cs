using System;

namespace Application.Sections
{
    public class LevelSection
    {
        public LevelSection(Guid id, string description, Guid[] nextLevels, Guid[] generatorIds)
        {
            Id = id;
            Description = description;
            NextLevels = nextLevels;
            GeneratorIds = generatorIds;
        }

        public Guid Id { get; }
        public string Description { get; }
        public Guid[] NextLevels { get; }
        public Guid[] GeneratorIds { get; }
    }
}