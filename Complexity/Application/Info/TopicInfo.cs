using System;

namespace Application.Info
{
    public class TopicInfo
    {
        public Guid Id { get; }
        public string Name { get; }

        public TopicInfo(string name, Guid id)
        {
            Name = name;
            Id = id;
        }
    }
}