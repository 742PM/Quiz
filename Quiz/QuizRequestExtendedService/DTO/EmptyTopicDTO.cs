using Newtonsoft.Json;

namespace QuizRequestExtendedService.DTO
{
    public class EmptyTopicDTO
    {
        public EmptyTopicDTO(string description, string name)
        {
            Description = description;
            Name = name;
        }

        [JsonProperty("description")] public string Description { get; }

        [JsonProperty("name")] public string Name { get; }
    }
}