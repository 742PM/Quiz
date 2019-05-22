using System;
using Newtonsoft.Json;

namespace QuizRequestExtendedService.DTO
{
    public class EmptyLevelDTO
    {
        [JsonProperty("description")] public string Description { get; }
        [JsonProperty("nextLevels")] public Guid[] NextLevels { get; }
        [JsonProperty("previousLevels")] public Guid[] PreviousLevels { get; }

        public EmptyLevelDTO(string description, Guid[] nextLevels, Guid[] previousLevels)
        {
            Description = description;
            NextLevels = nextLevels;
            PreviousLevels = previousLevels;
        }
    }
}