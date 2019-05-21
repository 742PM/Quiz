using System;
using Newtonsoft.Json;

namespace QuizWebApp.TaskService.DTO
{
    public class AdminTopicDTO
    {
        public AdminTopicDTO(string name, Guid id, Guid[] levelIds, string description)
        {
            Name = name;
            Id = id;
            LevelIds = levelIds;
            Description = description;
        }

        [JsonProperty("levelIds")] public Guid[] LevelIds { get; }

        [JsonProperty("id")] public Guid Id { get; }

        [JsonProperty("name")] public string Name { get; }
        
        [JsonProperty("description")] public string Description { get; }
    }
}