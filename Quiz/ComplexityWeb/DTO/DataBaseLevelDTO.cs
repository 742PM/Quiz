using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class DataBaseLevelDTO
    {
        [JsonProperty("description")] public string Description { get; }
        [JsonProperty("next_levels")] public IEnumerable<Guid> NextLevels { get; }
        [JsonProperty("previous_levels")] public IEnumerable<Guid> PreviousLevels { get; }

        public DataBaseLevelDTO(string description, IEnumerable<Guid> nextLevels, IEnumerable<Guid> previousLevels)
        {
            Description = description;
            NextLevels = nextLevels;
            PreviousLevels = previousLevels;
        }
    }
}