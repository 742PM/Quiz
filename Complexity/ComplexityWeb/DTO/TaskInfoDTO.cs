using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class TaskInfoDTO
    {
        public TaskInfoDTO(string question, string[] answers, bool hasHints)
        {
            Question = question;
            Answers = answers;
            HasHints = hasHints;
        }

        [JsonProperty("question")] public string Question { get; }
        [JsonProperty("answers")] public string[] Answers { get; }
        [JsonProperty("hasHints")] public bool HasHints { get; }
    }
}
