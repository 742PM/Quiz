using System;
using Newtonsoft.Json;

namespace QuizWebApp.Services.QuizService.DTO
{
    public class LevelInfoDTO
    {
        public LevelInfoDTO(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        [JsonProperty("id")] public Guid Id { get; }

        [JsonProperty("description")] public string Description { get; }
    }
}
