using System;
using System.Collections.Generic;

namespace Domain
{
    [Entity]
    public class Topic
    {
        public Topic(IEnumerable<Level> levels, Guid topicId, string name, string description)
        {
            Levels = levels;
            TopicId = topicId;
            Name = name;
            Description = description;
        }

        public Guid TopicId { get; }

        public string Name { get; }

        public string Description { get; }

        public IEnumerable<Level> Levels { get; }

        protected bool Equals(Topic other) => TopicId.Equals(other.TopicId);

        public override bool Equals(object obj) => obj is Topic topic && Equals(topic);

        public override int GetHashCode() => TopicId.GetHashCode();
    }
}
