using System;
using Newtonsoft.Json;

namespace QuizWebApp.TaskService.DTO
{
    public class AdminLevelDTO
    {
        public AdminLevelDTO(Guid id, string description, Guid[] nextLevels, Guid[] generatorIds)
        {
            Id = id;
            Description = description;
            NextLevels = nextLevels;
            GeneratorIds = generatorIds;
        }

        [JsonProperty("id")] public Guid Id { get; }

        [JsonProperty("description")] public string Description { get; }

        [JsonProperty("nextLevels")] public Guid[] NextLevels { get; }

        [JsonProperty("generatorIds")] public Guid[] GeneratorIds { get; }
    }
}