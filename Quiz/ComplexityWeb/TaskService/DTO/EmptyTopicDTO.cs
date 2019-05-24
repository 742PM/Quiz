using Newtonsoft.Json;

namespace QuizWebApp.TaskService.DTO
{
    public class EmptyTopicDTO
    {
        public EmptyTopicDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonProperty("description")] public string Description { get; }

        [JsonProperty("name")] public string Name { get; }
    }
}