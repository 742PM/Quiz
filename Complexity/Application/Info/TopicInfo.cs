using System;

namespace Application
{
    public class TopicInfo
    {
        public string Name { get; }
        public Guid Id { get; }

        public TopicInfo(string name, Guid id)
        {
            Name = name;
            Id = id;
        }

        public TopicInfo(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}