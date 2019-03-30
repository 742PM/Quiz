using System;

namespace Application.Info
{
    public class LevelInfo
    {
        public LevelInfo(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public Guid Id { get; }
        public string Description { get; }
    }
}
