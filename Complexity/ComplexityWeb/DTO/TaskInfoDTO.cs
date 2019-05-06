using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class TaskInfoDTO
    {
        public TaskInfoDTO(string question, string[] answers)
        {
            Question = question;
            Answers = answers;
        }

        [JsonProperty("question")] public string Question { get; }
        [JsonProperty("answers")] public string[] Answers { get; }
        [JsonProperty("has_hints")] public bool HasHints { get; }
    }
}
