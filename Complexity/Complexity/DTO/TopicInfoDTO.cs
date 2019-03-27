using System;
using Newtonsoft.Json;

namespace Application
{
    public class TopicInfoDTO
    {
        public TopicInfoDTO(string name, Guid id)
        {
            Name = name;
            Id = id;
        }

        [JsonProperty("id")] public Guid Id { get; }

        [JsonProperty("name")] public string Name { get; }
    }
}