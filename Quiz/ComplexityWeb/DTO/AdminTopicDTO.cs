using System;
using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class AdminTopicDTO
    {
        public AdminTopicDTO(string name, Guid id, AdminLevelDTO[] levels)
        {
            Name = name;
            Id = id;
            Levels = levels;
        }

        [JsonProperty("levels")] public AdminLevelDTO[] Levels { get; }

        [JsonProperty("id")] public Guid Id { get; }

        [JsonProperty("name")] public string Name { get; }
    }
}