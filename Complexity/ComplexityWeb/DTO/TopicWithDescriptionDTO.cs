using System;
using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class TopicWithDescriptionDTO
    {
        public TopicWithDescriptionDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonProperty("description")] public string Description { get; }

        [JsonProperty("name")] public string Name { get; }
    }
}