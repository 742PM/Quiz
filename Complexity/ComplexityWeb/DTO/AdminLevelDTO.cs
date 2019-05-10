using System;
using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class AdminLevelDTO
    {
        public AdminLevelDTO(Guid id, string description, Guid[] nextLevels, AdminTaskGeneratorDTO[] generators)
        {
            Id = id;
            Description = description;
            NextLevels = nextLevels;
            Generators = generators;
        }

        [JsonProperty("id")] public Guid Id { get; }

        [JsonProperty("description")] public string Description { get; }

        [JsonProperty("nextLevels")] public Guid[] NextLevels { get; }

        [JsonProperty("generators")] public AdminTaskGeneratorDTO[] Generators { get; }
    }
}